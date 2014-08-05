using Repositories;

namespace Services
{
    public class IErpCreateOrderJobService : IErpCreateOrderJobService
    {
        private readonly IErpOrderRespository _m3OrderRepository;
        private readonly IOrderRequestRespository _orderRequestRepository;

        public IErpCreateOrderJobService(IOrderRequestRespository orderRequestRespository,
            IErpOrderRespository m3OrderRespository)
        {
            _m3OrderRepository = m3OrderRespository;
            _orderRequestRepository = orderRequestRespository;
        }
    }
}