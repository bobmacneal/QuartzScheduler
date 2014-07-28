using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RWS.Models;
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
                var orderData = JsonConvert.DeserializeObject<OrderModel>(jsonOrderData.ToString());
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

                var orderCreationService = new OrderCreationService();

                //var orderService = new OrderService(new QuartzFactory().Scheduler);
                //if (orderService.CreateOrder(orderData))
                //{
                //    return Request.CreateResponse(HttpStatusCode.Created, orderData);
                //}
                return Request.CreateResponse(HttpStatusCode.NotModified);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    string.Format("OrderModel Creation Error:\n{0}", ex.Message)
                    );
            }
        }
    }
}