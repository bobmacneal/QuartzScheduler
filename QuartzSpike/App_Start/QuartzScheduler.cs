using System;
using System.Configuration;
using Jobs;
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
        public static void InitializeQuartzJobs(IUnityContainer container)
        {
            IScheduler scheduler = GetSchedulerFromUnityJobFactory(container);
            IncomingOrderJob(scheduler, "IncomingOrder");
        }

        private static void IncomingOrderJob(IScheduler scheduler, string groupIdentifier)
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

        private static IScheduler GetSchedulerFromUnityJobFactory(IUnityContainer container)
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = schedulerFactory.GetScheduler();
            IJobFactory unityJobFactory = new UnityJobFactory(container);
            scheduler.JobFactory = unityJobFactory;
            scheduler.Start();
            return scheduler;
        }
    }
}