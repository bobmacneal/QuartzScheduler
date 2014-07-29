using System;
using System.Configuration;
using Common.Logging;
using Common.Logging.Simple;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using RWS.Jobs;

namespace QuartzSpike
{
    public class QuartzScheduler
    {
        public static void InitializeQuartzJobs()
        {
            LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter {Level = LogLevel.Debug};
            IScheduler scheduler = GetScheduler();
            InitializeIncomingOrderJobs(scheduler, "IncomingOrder");
        }

        private static void InitializeIncomingOrderJobs(IScheduler scheduler, string groupIdentifier)
        {
            IJobDetail job = JobBuilder.Create<IncomingOrderJob>()
                .WithIdentity("job1", groupIdentifier)
                .Build();

            int intervalInSeconds =
                Convert.ToInt32(ConfigurationManager.AppSettings["IncomingOrderJobIntervalSeconds"]);

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", groupIdentifier)
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(intervalInSeconds)
                    .RepeatForever())
                .ForJob(job.Key)
                .Build();

            scheduler.ScheduleJob(job, trigger);

            scheduler.ListenerManager.AddJobListener(new IncomingOrderJobListener(),
                GroupMatcher<JobKey>.GroupEquals(groupIdentifier));
        }

        private static IScheduler GetScheduler()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            return scheduler;
        }
    }
}