using Flock.Client.Params;
using Flock.Job;

namespace Flock.Client
{
    public interface ApiClient
    {
        JobData[] FetchJobList(SearchParams @params);
    }
}