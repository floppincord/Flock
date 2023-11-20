namespace Flock.Job
{
    public class UserProfile
    {
        public string Username { get; private set; }
        
        public string Url { get; private set; }
        

        public UserProfile(string username, string url)
        {
            this.Username = username;
            this.Url = url;
        }
    }
}