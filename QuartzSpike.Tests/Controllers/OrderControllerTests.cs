using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuartzSpike.Controllers;
using Services;

namespace QuartzSpike.Tests.Controllers
{
    [TestClass]
    public class OrderControllerTests : ControllerTestBase
    {
        private OrderController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, DevelopmentApiUrl);
            IHttpRoute route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary {{"controller", "products"}});

            var mockOrderService = new Mock<IOrderService>();
            _controller = new OrderController(mockOrderService.Object)
            {
                ControllerContext = new HttpControllerContext(config, routeData, request),
                Request = request
            };
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        /// <summary>
        ///     Tests the functionality of the POST method with an empty order number
        /// </summary>
        [TestMethod]
        public void PostEmptyOrderNumberTest()
        {
            const string requestMessageBody =
                "{\"Source\":\"INBOUND_ORDER\",\"Company\":100,\"CustomerNumber\":\"HG\",\"OrderType\":\"WUS\",\"CustomersOrderNumber\":\"A12512\",\"ReferenceAtCustomer\":\"193\",\"OurReferenceNumber\":\"\",\"ReferenceNumber\":\"100\",\"CustomerAddress\":{\"Company\":100,\"Name\":\"NEETU SINHA SR.\",\"AddressLine1\":\"1800 WEST PARK DRIVE SW\",\"AddressLine2\":\"PO BOX 1088\",\"AddressLine3\":\"SUITE 2000\",\"AddressLine4\":\"ADDRESS LINE 4\",\"City\":\"WESTBOROUGH\",\"StateOrArea\":\"MA\",\"PostalCode\":\"01581\",\"CountryCode\":\"US\",\"PhoneNumber\":\"95228392839\",\"PhoneNumber2\":\"95228392839\",\"FaxNumber\":\"98228392839\"},\"CustomerOrderDetails\":[{\"Company\":100,\"ItemNumber\":\"02963\",\"SKU\":\"D    080\",\"OrderQuantity\":1,\"LineNumber\":\"1\"},{\"Company\":100,\"ItemNumber\":\"02963\",\"SKU\":\"D    085\",\"OrderQuantity\":1,\"LineNumber\":\"2\"},{\"Company\":100,\"ItemNumber\":\"02963\",\"SKU\":\"D    090\",\"OrderQuantity\":1,\"LineNumber\":\"3\"}]}";
            HttpResponseMessage result = _controller.Post(requestMessageBody);
            Assert.IsInstanceOfType(result, typeof (HttpResponseMessage), "Unexpected Type");
            Assert.IsFalse(result.IsSuccessStatusCode, "Unexpected Success Status Code");
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode,
                "Order POST allowed Order Data with empty order number value.");
        }

        /// <summary>
        ///     Tests the functionality of the POST method with an empty source
        /// </summary>
        [TestMethod]
        public void PostEmptySourceTest()
        {
            const string requestMessageBody =
                "{\"Source\":\"\",\"Company\":100,\"CustomerNumber\":\"HG\",\"OrderType\":\"WUS\",\"CustomersOrderNumber\":\"A12512\",\"ReferenceAtCustomer\":\"193\",\"OurReferenceNumber\":\"176\",\"ReferenceNumber\":\"100\",\"CustomerAddress\":{\"Company\":100,\"Name\":\"NEETU SINHA SR.\",\"AddressLine1\":\"1800 WEST PARK DRIVE SW\",\"AddressLine2\":\"PO BOX 1088\",\"AddressLine3\":\"SUITE 2000\",\"AddressLine4\":\"ADDRESS LINE 4\",\"City\":\"WESTBOROUGH\",\"StateOrArea\":\"MA\",\"PostalCode\":\"01581\",\"CountryCode\":\"US\",\"PhoneNumber\":\"95228392839\",\"PhoneNumber2\":\"95228392839\",\"FaxNumber\":\"98228392839\"},\"CustomerOrderDetails\":[{\"Company\":100,\"ItemNumber\":\"02963\",\"SKU\":\"D    080\",\"OrderQuantity\":1,\"LineNumber\":\"1\"},{\"Company\":100,\"ItemNumber\":\"02963\",\"SKU\":\"D    085\",\"OrderQuantity\":1,\"LineNumber\":\"2\"},{\"Company\":100,\"ItemNumber\":\"02963\",\"SKU\":\"D    090\",\"OrderQuantity\":1,\"LineNumber\":\"3\"}]}";
            HttpResponseMessage result = _controller.Post(requestMessageBody);
            Assert.IsInstanceOfType(result, typeof (HttpResponseMessage), "Unexpected Type");
            Assert.IsFalse(result.IsSuccessStatusCode, "Unexpected Success Status Code");
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode,
                "Order POST allowed Order Data with empty source value.");
        }

        /// <summary>
        ///     Tests the functionality of the POST method with a new order number
        /// </summary>
        [TestMethod]
        public void OrderControllerPost_NewOrderNumber_Test()
        {
            const string requestMessageBody =
                "{\"Source\":\"INBOUND_ORDER\",\"Company\":100,\"CustomerNumber\":\"HG\",\"OrderType\":\"WUS\",\"CustomersOrderNumber\":\"A12512\",\"ReferenceAtCustomer\":\"193\",\"OurReferenceNumber\":\"176\",\"ReferenceNumber\":\"100\",\"CustomerAddress\":{\"Company\":100,\"Name\":\"NEETU SINHA SR.\",\"AddressLine1\":\"1800 WEST PARK DRIVE SW\",\"AddressLine2\":\"PO BOX 1088\",\"AddressLine3\":\"SUITE 2000\",\"AddressLine4\":\"ADDRESS LINE 4\",\"City\":\"WESTBOROUGH\",\"StateOrArea\":\"MA\",\"PostalCode\":\"01581\",\"CountryCode\":\"US\",\"PhoneNumber\":\"95228392839\",\"PhoneNumber2\":\"95228392839\",\"FaxNumber\":\"98228392839\"},\"CustomerOrderDetails\":[{\"Company\":100,\"ItemNumber\":\"02963\",\"SKU\":\"D    080\",\"OrderQuantity\":1,\"LineNumber\":\"1\"},{\"Company\":100,\"ItemNumber\":\"02963\",\"SKU\":\"D    085\",\"OrderQuantity\":1,\"LineNumber\":\"2\"},{\"Company\":100,\"ItemNumber\":\"02963\",\"SKU\":\"D    090\",\"OrderQuantity\":1,\"LineNumber\":\"3\"}]}";
            HttpResponseMessage result = _controller.Post(requestMessageBody);
            Assert.IsInstanceOfType(result, typeof (HttpResponseMessage), "Unexpected Type");
            Assert.IsTrue(result.IsSuccessStatusCode, "Unexpected Success Status Code");
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode, "Customer Order POST failed.");
        }
    }
}