using Quartz;
using Quartz.Impl;
using RWS.Jobs;

namespace QuartzSpike
{
    public class QuartzScheduler
    {
        public static void InitializeIncomingOrderJobs()
        {
            IScheduler scheduler = GetScheduler();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<IncomingOrderJob>()
                .WithIdentity("IncomingOrderJob", "group1") // name "incomingOrderJob", group "group1"
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("IncomingOrderJobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(2)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            scheduler.ScheduleJob(job, trigger);
        }

        private static IScheduler GetScheduler()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = schedulerFactory.GetScheduler();

            scheduler.Start();
            return scheduler;
        }
    }
}