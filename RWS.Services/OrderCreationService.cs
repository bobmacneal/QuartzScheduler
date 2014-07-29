using RWS.Repositories;

namespace RWS.Services
{
    public class OrderCreationService
    {
        private readonly OrderRequestRespository _repository;

        public OrderCreationService(OrderRequestRespository repository)
        {
            _repository = repository;
        }

        public void AddOrderRequest(string requestPayload)
        {
            _repository.AddRequest(requestPayload);
        }
    }
}