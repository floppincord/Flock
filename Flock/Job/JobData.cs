using System;

namespace Flock.Job
{
    public class JobData
    {
        public int JobId { get; private set; }

        public JobDetails Details { get; private set; }

        public UserProfile User { get; private set; }

        public string PaymentAmount { get; private set; }

        public string PaymentHourlyMethod { get; private set; }

        public JobData(int jobId, JobDetails details, UserProfile user, string paymentAmount,
            string paymentHourlyMethod)
        {
            this.JobId = jobId;
            this.Details = details;
            this.User = user;
            this.PaymentAmount = paymentAmount;
            this.PaymentHourlyMethod = paymentHourlyMethod;
        }
    }
}