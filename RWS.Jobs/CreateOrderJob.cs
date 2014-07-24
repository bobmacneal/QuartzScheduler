using System;
using System.Diagnostics;
using Quartz;
using Quartz.Impl.Triggers;
using RWS.Models;
using RWS.Repositories;

namespace RWS.Jobs
{
    public class CreateOrderJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                OrderModel orderModel = GetOrderFromJobDataMap(context.JobDetail.JobDataMap);
                var m3OrderRespository = new M3OrderRespository();
                bool orderCreation = m3OrderRespository.CreateOrder(orderModel);
                if (orderCreation == false)
                {
                    //Do something on orderCreation false
                    Debug.Print("Unable to create order!");
                }
            }
            catch (Exception ex)
            {
                var retryTrigger = new SimpleTriggerImpl(Guid.NewGuid().ToString())
                {
                    Description = "RetryTrigger",
                    RepeatCount = 0,
                    JobKey = context.JobDetail.Key
                };
                context.Scheduler.ScheduleJob(retryTrigger); // schedule the trigger

                var jobExecutionException = new JobExecutionException(ex, false);
                throw jobExecutionException;
            }
        }

        private static OrderModel GetOrderFromJobDataMap(JobDataMap jobDataMap)
        {
            var order = new OrderModel
            {
                Company = jobDataMap.GetIntValue("Company"),
                Source = jobDataMap.GetString("Source"),
                CustomerNumber = jobDataMap.GetString("CustomerNumber"),
                OrderType = jobDataMap.GetString("OrderType"),
                CustomersOrderNumber = jobDataMap.GetString("CustomersOrderNumber"),
                ReferenceAtCustomer = jobDataMap.GetString("ReferenceAtCustomer"),
                OurReferenceNumber = jobDataMap.GetString("OurReferenceNumber"),
                ReferenceNumber = jobDataMap.GetString("ReferenceNumber")
            };
            return order;
        }
    }
}