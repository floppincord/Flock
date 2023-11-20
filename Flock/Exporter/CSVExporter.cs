using System.IO;
using System.Text;
using Flock.Job;

namespace Flock.Exporter
{
    public class CSVExporter : Exporter
    {
        public void ExportToFile(string query, JobData[] datum)
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
    }
}