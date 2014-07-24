using System.Diagnostics;

namespace RWS.Repositories
{
    public class M3OrderRespository
    {
        public bool CreateOrder(Models.OrderModel orderModel)
        {
            //TODO Presumably use M3 API to create an OrderModel
            Debug.Print("------ Call to insert order in M3 ------");
            return true;
        }
    }
}