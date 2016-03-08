using System.Web.Configuration;
using Twilio;

namespace ETANotifications.Services
{
    public interface INotificationService
    {
        void SendSmsNotification(string phoneNumber, string message, string statusCallback);
    }

    public class NotificationService : INotificationService
    {
        private readonly TwilioRestClient _twilioRestClient;

        private readonly string _accountSid = WebConfigurationManager.AppSettings["TwilioAccountSid"];
        private readonly string _authToken = WebConfigurationManager.AppSettings["TwilioAuthToken"];
        private readonly string _twilioNumber = WebConfigurationManager.AppSettings["TwilioPhoneNumber"];

        public NotificationService()
        {
            _twilioRestClient = new TwilioRestClient(_accountSid, _authToken);
        }

        public NotificationService(TwilioRestClient twilioRestClient)
        {
            _twilioRestClient = twilioRestClient;
        }

        public void SendSmsNotification(string phoneNumber, string message, string statusCallback)
        {
            _twilioRestClient.SendMessage(_twilioNumber, phoneNumber, message, statusCallback);
        }
    }
}