using System;
using Quartz;

namespace Jobs
{
    public class IncomingOrderJobListener : IJobListener
    {
        public readonly Guid Id = Guid.NewGuid();
        //TODO Determine if this is useful

        public void JobToBeExecuted(IJobExecutionContext context)
        {
            JobKey jobKey = context.JobDetail.Key;
        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            JobKey jobKey = context.JobDetail.Key;
        }

        public void JobExecutionVetoed(IJobExecutionContext context)
        {
            JobKey jobKey = context.JobDetail.Key;
        }

        public string Name
        {
            get { return "IncomingOrderJobListener" + Id; }
        }
    }
}