namespace Flock.Job
{
    public class JobDetails
    {
        public string Title { get; private set; }

        public string Description { get; private set; }

        public string[] Tags { get; private set; }

        public string Url { get; private set; }

        public JobDetails(string title, string description, string[] tags, string url)
        {
            this.Title = title;
            this.Description = description;
            this.Tags = tags;
            this.Url = url;
        }
    }
}