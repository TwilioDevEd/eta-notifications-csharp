using System;
using System.Threading.Tasks;
using System.Web.Configuration;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ETANotifications.Services
{
    public interface INotificationService
    {
        Task SendSmsNotification(string phoneNumber, string message, string statusCallback);
    }

    public class NotificationService : INotificationService
    {
        private readonly ITwilioRestClient _client;
        private readonly string _accountSid = WebConfigurationManager.AppSettings["TwilioAccountSid"];
        private readonly string _authToken = WebConfigurationManager.AppSettings["TwilioAuthToken"];
        private readonly string _twilioNumber = WebConfigurationManager.AppSettings["TwilioPhoneNumber"];

        public NotificationService()
        {
            _client = new TwilioRestClient(_accountSid, _authToken);
        }

        public NotificationService(ITwilioRestClient client)
        {
            _client = client;
        }

        public async Task SendSmsNotification(string phoneNumber, string message, string statusCallback)
        {
            var to = new PhoneNumber(phoneNumber);
            await MessageResource.CreateAsync(
                to,
                from: new PhoneNumber(_twilioNumber),
                body: message,
                statusCallback: new Uri(statusCallback),
                client: _client);
        }
    }
}