using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Configuration;
using System.Web.Mvc;
using ETANotifications.Models;
using ETANotifications.Models.Repository;
using ETANotifications.Services;

namespace ETANotifications.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly INotificationService _notificationServices;

        public OrdersController() : this(new OrderRepository(), new NotificationServices())
        {
        }

        public OrdersController(IOrderRepository orderRepository, INotificationService notificationServices)
        {
            _orderRepository = orderRepository;
            _notificationServices = notificationServices;
        }

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            return View(await _orderRepository.FindAllAsync());
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await _orderRepository.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Pickup/5
        [HttpPost]
        public async Task<ActionResult> Pickup(int id)
        {
            Order order = await _orderRepository.FindAsync(id);
            order.Status = "Shipped";
            order.NotificationStatus = "Queued";
            await _orderRepository.UpdateAsync(order);

            _notificationServices.SendSmsNotification(order.CustomerPhoneNumber
                , "Your clothes will be sent and will be delivered in 20 minutes", GetCallbackUri(id));
            return RedirectToAction("Details", new { id = id });
        }

        // POST: Orders/Deliver/5
        [HttpPost]
        public async Task<ActionResult> Deliver(int id)
        {
            Order order = await _orderRepository.FindAsync(id);
            order.Status = "Delivered";
            order.NotificationStatus = "Queued";
            await _orderRepository.UpdateAsync(order);

            _notificationServices.SendSmsNotification(order.CustomerPhoneNumber
                , "Your clothes have been delivered", GetCallbackUri(id));
            return RedirectToAction("Details", new { id = id });
        }

        // POST: Orders/UpateNotificationStatus/5
        [HttpPost]
        public async Task<ActionResult> UpdateNotificationStatus(int id, string MessageStatus)
        {
            Order order = await _orderRepository.FindAsync(id);
            order.NotificationStatus = MessageStatus.First().ToString().ToUpper() + MessageStatus.Substring(1);

            await _orderRepository.UpdateAsync(order);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected string GetCallbackUri(int id)
        {
            Uri requestUrl = Url.RequestContext.HttpContext.Request.Url;

            return $"{requestUrl.Scheme}://{WebConfigurationManager.AppSettings["TestDomain"]}" +
                   $"{Url.Action("UpdateNotificationStatus", "Orders", new {id = id})}";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _orderRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
