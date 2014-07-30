using System;
using System.Configuration;
using Common.Logging;
using Common.Logging.Simple;
using Microsoft.Practices.Unity;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using RWS.Jobs;

namespace QuartzSpike
{
    public class QuartzScheduler
    {
        public static void InitializeQuartzJobs(UnityContainer container)
        {
            LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter {Level = LogLevel.Debug};
            IScheduler scheduler = GetScheduler(container);
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
                .Build();

            scheduler.ScheduleJob(job, trigger);

            scheduler.ListenerManager.AddJobListener(new IncomingOrderJobListener(),
                GroupMatcher<JobKey>.GroupEquals(groupIdentifier));
        }

        private static IScheduler GetScheduler(IUnityContainer container)
        {
            IJobFactory jobFactory = new UnityJobFactory(container);
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = schedulerFactory.GetScheduler();
            scheduler.JobFactory = jobFactory;
            scheduler.Start();
            return scheduler;
        }
    }
}