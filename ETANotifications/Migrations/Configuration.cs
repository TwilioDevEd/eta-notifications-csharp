using System.Collections.Generic;
using ETANotifications.Models;

namespace ETANotifications.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ETANotifications.Models.OrderContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ETANotifications.Models.OrderContext";
        }

        protected override void Seed(ETANotifications.Models.OrderContext context)
        {
            var orders = new List<Order>
            {
                new Order { CustomerName = "Vincent Vega", CustomerPhoneNumber = "+17656732002",
                    Status = "Ready", NotificationStatus = "None" },
                new Order { CustomerName = "Mia Wallace", CustomerPhoneNumber = "+17654532001",
                    Status = "Ready", NotificationStatus = "None" },

            };
            orders.ForEach(order => context.Orders.AddOrUpdate(p => p.Id, order));
            context.SaveChanges();
        }
    }
}
