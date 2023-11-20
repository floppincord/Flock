using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Flock.Client.Params;
using Flock.Job;

namespace Flock.Client.Client
{
    public class CrowdApiClient : ApiClient
    {
        private static readonly string ENDPOINT = "https://crowdworks.jp";
        private static readonly string JOBPATH = "/public/jobs/search";

        private readonly CookieStorage _cookies = new CookieStorage();

        public JobData[] FetchJobList(SearchParams @params)
        {
            var request = Utils.createRequest(_cookies);

            var reqParams = new RequestParams();
            reqParams["search[keywords]"] = @params.Keywords;
            reqParams["order"] = @params.Order.Name;
            reqParams["hide_expired"] = false;

            var response = request.Get(ENDPOINT + JOBPATH, reqParams);

            return ParseJobList(response.ToString());
        }

        private JobData[] ParseJobList(String html)
        {
            var document = new HtmlParser().ParseDocument(html);

            var jobLists = document.QuerySelector("div[class='search_results']");
            if (jobLists == null)
                return new JobData[] { };

            List<JobData> jobs = new List<JobData>();
            foreach (var node in jobLists.Children[0].Children)
            {
                JobData result = ParseJob(node);
                if (result == null)
                    continue;
                jobs.Add(result);
            }

            return jobs.ToArray();
        }

        private JobData ParseJob(IElement node)
        {
            var itemElements = node.GetElementsByClassName("item_title");
            var descriptionElement = node.GetElementsByClassName("item_description");
            var usernameElement = node.GetElementsByClassName("user-name");
            var tagsElement = node.GetElementsByClassName("user-tags");
            var paymentElement = node.GetElementsByClassName("entry_data payment");
            if (itemElements.Length != 1 || descriptionElement.Length != 1 || usernameElement.Length != 1
                || paymentElement.Length != 1)
                return null;

            var jobId = node.GetAttribute("data-job_offer_id");
            if (jobId == null)
                return null;

            var itemTitle = itemElements[0].Children[0];
            var title = itemTitle.InnerHtml;
            var url = itemTitle.GetAttribute("href");

            var description = descriptionElement[0].Children[0].InnerHtml;

            var itemUsername = usernameElement[0].Children[0];
            var username = itemUsername.InnerHtml;
            var userUrl = itemUsername.GetAttribute("href");

            List<String> userTagList = new List<string>();
            if (tagsElement.Length > 0)
            {
                var itemTags = tagsElement[0].Children;
                foreach (var tagElement in itemTags)
                {
                    if (tagElement.Children.Length < 1)
                        continue;

                    var itemTag = tagElement.Children[0].Children[0];
                    userTagList.Add(itemTag.InnerHtml);
                }
            }

            var itemPayment = paymentElement[0].Children[0].GetElementsByClassName("amount")[0];
            String paymentAmount = "要相談";
            if (itemPayment.Children.Length > 0)
            {
                paymentAmount = "";
                foreach (var element in itemPayment.Children)
                {
                    paymentAmount += element.InnerHtml;
                    if (element.NextSibling != null)
                    {
                        paymentAmount += new Regex("\\s+")
                            .Replace(element.NextSibling.TextContent, "");
                    }
                }
            }

            var itemPaymentHourly = paymentElement[0].Children[0]
                .GetElementsByClassName("cw-label payment_label")[0];
            var paymentHourlyMethod = itemPaymentHourly.InnerHtml;

            return new JobData(int.Parse(jobId),
                new JobDetails(title, description, userTagList.ToArray(), ENDPOINT + url),
                new UserProfile(username, ENDPOINT + userUrl), paymentAmount, paymentHourlyMethod);
        }
    }
}