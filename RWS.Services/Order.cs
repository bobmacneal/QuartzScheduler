using Quartz;
using RWS.Jobs;

namespace RWS.Services
{
    public class OrderService
    {
        private readonly IScheduler _scheduler;

        public OrderService(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public bool CreateOrder(Models.Order order)
        {
            IJobDetail job = JobBuilder.Create<CreateOrder>()
                .WithIdentity("job1", "group1")
                .Build();


            // Trigger the job to run and repeat every 10 seconds for 5 times
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .WithRepeatCount(5))
                .Build();

            _scheduler.ScheduleJob(job, trigger);

            //_scheduler.Shutdown();

            return true;
        }
    }
}