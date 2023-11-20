using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Flock.Client;
using Flock.Client.Client;
using Flock.Client.Params;
using Flock.Job;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Flock
{
    public class Flock
    {
        private readonly Configuration _configuration;
        private readonly ApiClient _client;

        private readonly Dictionary<string, List<int>> _visited = new Dictionary<string, List<int>>();

        public Flock(Configuration configuration)
        {
            _configuration = configuration;
            _client = new CrowdApiClient();
        }

        public void Run()
        {
            while (true)
            {
                foreach (string query in _configuration.Query)
                {
                    var shouldIgnore = !_visited.ContainsKey(query);

                    var jobList = _client.FetchJobList(new SearchParams(query, Order.OrderNew));
                    Console.WriteLine("Searched: {0} ({1} Founds)", query, jobList.Length);
                    if (jobList.Length < 1)
                        continue;

                    if (shouldIgnore)
                        _visited.Add(query, new List<int>());

                    exportToCSV(query, jobList);
                    foreach (var job in jobList)
                    {
                        if (_visited[query].Contains(job.JobId))
                            continue;

                        _visited[query].Add(job.JobId);
                        if (!shouldIgnore)
                        {
                            Console.WriteLine("New Job Found: " + job.Details.Url);
                            SendNotification(job);
                        }
                    }

                    Thread.Sleep(1000);
                }

                Console.WriteLine("Next search will be in 30 seconds");
                Thread.Sleep(30 * 1000);
            }
        }

        private void exportToCSV(String query, JobData[] datum)
        {
            var csvBuilder = new StringBuilder();

            csvBuilder.Append("タイトル,タグ,Url,価格,支払方法,ユーザー名,ユーザーUrl\n");
            foreach (var data in datum)
            {
                var details = data.Details;
                var user = data.User;

                var formattedDetails = string.Format("{0},{1},{2}",
                    details.Title.Replace(",", ""), string.Join(" ", details.Tags), details.Url);
                var formattedPayment = string.Format("{0},{1}", data.PaymentAmount.Replace(",", ""),
                    data.PaymentHourlyMethod);
                var formattedUser = string.Format("{0},{1}", user.Username.Replace(",", ""), user.Url);

                csvBuilder.Append(string.Format("{0},{1},{2}\n", formattedDetails, formattedPayment, formattedUser));
            }

            File.WriteAllText(string.Format("jobs_{0}.csv", query), csvBuilder.ToString());
        }

        private void SendNotification(JobData job)
        {
            new ToastContentBuilder()
                .AddText("新しい求人が投稿されました！")
                .AddText(job.Details.Title)
                .Show();
        }
    }
}