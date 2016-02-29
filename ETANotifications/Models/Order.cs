using System;
using System.ComponentModel.DataAnnotations;

namespace ETANotifications.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required, Display(Name = "Customer name")]
        public string CustomerName { get; set; }

        [Required, Phone, Display(Name = "Customer Phone Number")]
        public string CustomerPhoneNumber { get; set; }

        [Required, Display(Name = "Status")]
        public string Status { get; set; }

        [Required, Display(Name = "Notification Status")]
        public string NotificationStatus { get; set; }
    }
}