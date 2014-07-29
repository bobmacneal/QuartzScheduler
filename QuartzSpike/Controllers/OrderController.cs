using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RWS.Models;
using RWS.Repositories;
using RWS.Services;

namespace QuartzSpike.Controllers
{
    public class OrderController : ApiController
    {
        // POST http://localhost:55085/api/order
        public HttpResponseMessage Post([FromBody] JToken jsonOrderData)
        {
            try
            {
                var orderRequest = jsonOrderData.ToString();
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

                //ILog log = LogManager.GetCurrentClassLogger();
                //log.Debug("AddOrderRequest called***********************************************");

                var orderCreationService = new OrderCreationService(new OrderRequestRespository());
                orderCreationService.AddOrderRequest(orderRequest);

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