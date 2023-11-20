using Flock.Job;

namespace Flock.Exporter
{
    public interface Exporter
    {
        void ExportToFile(string query, JobData[] data);
    }
}