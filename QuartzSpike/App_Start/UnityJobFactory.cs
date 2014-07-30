using System;
using Microsoft.Practices.Unity;
using Quartz;
using Quartz.Spi;

namespace QuartzSpike
{
    public class UnityJobFactory : IJobFactory
    {
        private readonly IUnityContainer _container;

        public UnityJobFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return (IJob) _container.Resolve(bundle.JobDetail.JobType);
            }
            catch (Exception e)
            {
                var schedulerException = new SchedulerException("Problem instantiating class", e);
                throw schedulerException;
            }
        }

        public void ReturnJob(IJob job)
        {
            //http://quartz.10975.n7.nabble.com/quartznet-3591-IJobFactory-ReturnJob-query-td10671.html
            //If your framework has the notion of destroying instances you can 
            //delegate to your framework here. Otherwise you can just skip 
            //implementing this or consider calling job's dispose if job is 
            //IDisposable. Currently SimpleJobFactory's implementation is no-op. 

            throw new NotImplementedException();
        }
    }
}