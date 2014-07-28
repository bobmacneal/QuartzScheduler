using RWS.Repositories;

namespace RWS.Services
{
    public class OrderCreationService
    {
        private readonly OrderRequestRespository _repository;

        public OrderCreationService()
        {
            _repository = new OrderRequestRespository();
        }

        public void AddOrderRequest(string requestPayload)
        {
            _repository.AddRequest(requestPayload);
        }
    }
}