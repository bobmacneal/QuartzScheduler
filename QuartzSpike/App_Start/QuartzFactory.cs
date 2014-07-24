using Quartz;
using Quartz.Impl;

namespace QuartzSpike
{
    public class QuartzFactory
    {
        private IScheduler _scheduler;

        public IScheduler Scheduler
        {
            get { return _scheduler ?? (_scheduler = GetScheduler()); }
            set { _scheduler = value; }
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