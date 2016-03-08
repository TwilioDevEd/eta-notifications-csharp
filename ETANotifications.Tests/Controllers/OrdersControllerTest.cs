using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ETANotifications.Controllers;
using ETANotifications.Models;
using ETANotifications.Models.Repository;
using ETANotifications.Services;
using ETANotifications.Tests.Models;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace ETANotifications.Tests.Controllers
{
    [TestFixture]
    public class OrdersControllerTest
    {
        private Mock<INotificationService> _notificationServiceMock;
        private IOrderRepository _orderRepository;

        [SetUp]
        public void Setup()
        {
            _orderRepository = new InMemoryOrdersRepository();
            _notificationServiceMock = new Mock<INotificationService>();

            _notificationServiceMock
                .Setup(c => c.SendSmsNotification(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()));
        }

        [Test]
        public void WhenInvokingIndexAction_ThenRendersTheDefaultView()
        {
            var controller = BuildController(_orderRepository, _notificationServiceMock.Object);

            controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<Order>>();

        }

        [Test]
        public void WhenInvokingDetailsAction_ThenRendersTheDefaultView()
        {
            var controller = BuildController(_orderRepository, _notificationServiceMock.Object);

            controller.WithCallTo(c => c.Details(1))
                .ShouldRenderDefaultView()
                .WithModel<Order>();
        }

        [Test]
        public void WhenInvokingPickupAction_ThenRedirectsToDetailsViewAndUpdateOrderStatus()
        {
            var controller = BuildController(_orderRepository, _notificationServiceMock.Object);

            controller.WithCallTo(c => c.Pickup(1))
                .ShouldRedirectTo<OrdersController>(c => c.Details(1));

            Assert.That(_orderRepository.FindAsync(1).GetAwaiter().GetResult().Status, Is.EqualTo("Shipped"));
            _notificationServiceMock
                .Verify(c => c.SendSmsNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void WhenInvokingDeliverAction_ThenRedirectsToDetailsViewAndUpdateOrderStatus()
        {
            var controller = BuildController(_orderRepository, _notificationServiceMock.Object);

            controller.WithCallTo(c => c.Deliver(1))
                .ShouldRedirectTo<OrdersController>(c => c.Details(1));

            Assert.That(_orderRepository.FindAsync(1).GetAwaiter().GetResult().Status, Is.EqualTo("Delivered"));
            _notificationServiceMock
                .Verify(c => c.SendSmsNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void WhenInvokingUpdateNotificationStatusAction_ThenReturns200AndUpdateOrderNotificationStatus()
        {
            var controller = BuildController(_orderRepository, _notificationServiceMock.Object);

            controller.WithCallTo(c => c.UpdateNotificationStatus(1, "sent"))
                .ShouldGiveHttpStatus(HttpStatusCode.OK);

            Assert.That(_orderRepository.FindAsync(1).GetAwaiter().GetResult().NotificationStatus, Is.EqualTo("Sent"));
        }

        private OrdersController BuildController(IOrderRepository orderRepository, INotificationService notificationService)
        {
            StringBuilder outputStream;
            var controller =
                new OrdersController(orderRepository, notificationService)
                {
                    ControllerContext = GetControllerContextBasedOnMocks(out outputStream)
                };
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            controller.Url = new UrlHelper(new RequestContext(controller.HttpContext, new RouteData()), routes);
            return controller;
        }

        protected ControllerContext GetControllerContextBasedOnMocks(out StringBuilder outputStream)
        {
            var result = new StringBuilder();
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(r => r.ApplicationPath).Returns(@"/");
            mockRequest.SetupGet(r => r.Url).Returns(new Uri("http://www.localhost.com"));

            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(r => r.Write(It.IsAny<string>()))
                .Callback<string>(c =>
                {
                    result.Append(c);
                });
            mockResponse.Setup(r => r.Output)
                .Returns(new StringWriter(result));
            mockResponse.Setup(r => r.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns((string s) => s);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.Response).Returns(mockResponse.Object);
            controllerContextMock.Setup(x => x.HttpContext.Request).Returns(mockRequest.Object);

            outputStream = result;
            return controllerContextMock.Object;
        }
    }
}
