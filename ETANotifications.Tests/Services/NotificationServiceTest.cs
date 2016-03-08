using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETANotifications.Services;
using Moq;
using NUnit.Framework;
using Twilio;

namespace ETANotifications.Tests.Services
{
    [TestFixture]
    public class NotificationServiceTest
    {
        [Test]
        public void WhenSendANotification_AMessageIsSent()
        {
            var phoneNumber = "+5555555555";
            var message = "Message";
            var callbackUrl = "http://callback.com";
            var twilioClientMock = new Mock<TwilioRestClient>("AccountSid", "AuthToken");
            twilioClientMock
                .Setup(c => c.SendMessage(phoneNumber, message, callbackUrl));
                
            var notificationServices = new NotificationService(twilioClientMock.Object);
            notificationServices.SendSmsNotification(phoneNumber, message, callbackUrl);

            twilioClientMock.Verify(
                c => c.SendMessage(It.IsAny<string>(), phoneNumber, message, callbackUrl), Times.Once);
        }
    }
}
