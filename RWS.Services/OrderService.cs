using Repositories;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRequestRespository _repository;

        public OrderService(OrderRequestRespository repository)
        {
            _repository = repository;
        }

        public void AddOrderRequest(string requestPayload)
        {
            _repository.AddRequest(requestPayload);
        }
    }
}