using ETANotifications.Services;
using Moq;
using NUnit.Framework;
using Twilio.Clients;
using Twilio.Http;

namespace ETANotifications.Tests.Services
{
    [TestFixture]
    public class NotificationServiceTest
    {
        [Test]
        public async void WhenSendANotification_AMessageIsSent()
        {
            var phoneNumber = "+5555555555";
            var message = "Message";
            var callbackUrl = "http://callback.com";

            var twilioClientMock = new Mock<ITwilioRestClient>();
            twilioClientMock.Setup(c => c.AccountSid).Returns("AccountSID");
            twilioClientMock.Setup(c => c.RequestAsync(It.IsAny<Request>()))
                            .ReturnsAsync(new Response(System.Net.HttpStatusCode.Created, ""));

            var notificationServices = new NotificationService(twilioClientMock.Object);
            await notificationServices.SendSmsNotification(phoneNumber, message, callbackUrl);

            twilioClientMock.Verify(
                c => c.RequestAsync(It.IsAny<Request>()), Times.Once);
        }
    }
}
