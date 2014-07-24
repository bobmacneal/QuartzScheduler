using Quartz;
using RWS.Jobs;
using RWS.Models;

namespace RWS.Services
{
    public class OrderService
    {
        private readonly IScheduler _scheduler;

        public OrderService(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public bool CreateOrder(OrderModel orderModel)
        {
            IJobDetail job = JobBuilder.Create<CreateOrderJob>()
                .WithIdentity("job1", "group1")
                .UsingJobData("Company", orderModel.Company)
                .UsingJobData("Source", orderModel.Source)
                .UsingJobData("CustomerNumber", orderModel.CustomerNumber)
                .UsingJobData("OrderType", orderModel.OrderType)
                .UsingJobData("CustomersOrderNumber", orderModel.CustomersOrderNumber)
                .UsingJobData("ReferenceAtCustomer", orderModel.ReferenceAtCustomer)
                .UsingJobData("OurReferenceNumber", orderModel.OurReferenceNumber)
                .UsingJobData("ReferenceNumber", orderModel.ReferenceNumber)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .Build();

            _scheduler.ScheduleJob(job, trigger);

            return true;
        }
    }
}