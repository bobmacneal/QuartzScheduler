using System.Diagnostics;

namespace Repositories
{
    public class M3OrderRespository : IM3OrderRespository
    {
        public void CreateOrder(Models.OrderModel orderModel)
        {
            //TODO Presumably use M3 API to create an OrderModel
            Debug.Print("------ Call to insert order in M3 ------");
        }
    }
}