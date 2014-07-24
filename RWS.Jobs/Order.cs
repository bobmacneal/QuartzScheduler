using Quartz;
using RWS.Repositories;

namespace RWS.Jobs
{
    public class CreateOrder : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var m3OrderRespository = new M3OrderRespository();
            if (m3OrderRespository.CreateOrder(new Models.Order()) == false)
            {
                //Do something if fails
            }
        }
    }
}