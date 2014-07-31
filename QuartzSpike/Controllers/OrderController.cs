using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services;

namespace QuartzSpike.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST http://localhost:55085/api/order
        public HttpResponseMessage Post([FromBody] JToken jsonOrderData)
        {
            try
            {
                string orderRequest = jsonOrderData.ToString();
                var orderData = JsonConvert.DeserializeObject<OrderModel>(orderRequest);
                string correlationId = orderData.OurReferenceNumber;
                string source = orderData.Source;
                if (correlationId.IsNullOrWhiteSpace())
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Correlation identifier not provided.");
                }
                if (string.IsNullOrEmpty(source))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Source not provided.");
                }
                _orderService.AddOrderRequest(orderRequest);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    string.Format("Order creation error:\n{0}", ex.Message)
                    );
            }
        }
    }
}