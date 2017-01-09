<a href="https://www.twilio.com">
  <img src="https://static0.twilio.com/marketing/bundles/marketing/img/logos/wordmark-red.svg" alt="Twilio" width="250" />
</a>

# ETA Notifications Node. Powered by Twilio - ASP.NET MVC

[![Build status](https://ci.appveyor.com/api/projects/status/yqxays3pbnjsau25?svg=true)](https://ci.appveyor.com/project/TwilioDevEd/eta-notifications-csharp)

GPS tracking implementation with ASP.NET MVC and Twilio

[Read the full tutorial here](https://www.twilio.com/docs/tutorials/walkthrough/eta-notifications/csharp/mvc)!

### Local development

1. First clone this repository and `cd` into it
   ```
   git clone git@github.com:TwilioDevEd/eta-notifications-csharp.git

   cd eta-notifications-csharp
   ```

1. Create a new file `ETANotifications/Local.config` and update the content with

   ```
    <appSettings>
      <add key="TwilioAccountSid" value="your_account_SID" />
      <add key="TwilioAuthToken" value="your_twilio_auth_token" />
      <add key="TwilioPhoneNumber" value="your_twilio_number" />
      <add key="TestDomain" value="[your-ngrok-subdomain].ngrok.io"/>
    </appSettings>
   ```
   Be sure to replace placeholders like `your_account_SID`, `your_twilio_auth_token` and `your_twilio_number` with valid information from your
   [Twilio Account Settings](https://www.twilio.com/user/account/settings).

   For better understanding while replacing ```[your-ngrok-subdomain]``` placeholder see [Exposing the application to Internet](#ngrok) section

1. Build the solution

1. Run migrations by executing the following in the Package Manager Console

  	```bash
    PM> Update-Database
    ```

1. Run the application

1. Check it out at [http://localhost:1928](http://localhost:1928)

That's it!

#### Exposing the application to Internet<a name="ngrok">

Some application's endpoints need to be publicly accessible. [We recommend using ngrok](https://www.twilio.com/blog/2015/09/6-awesome-reasons-to-use-ngrok-when-testing-webhooks.html). Here's an example:

  ```bash
  $ ngrok http 1928 -host-header="localhost:1928"
  ```

## Meta

* No warranty expressed or implied. Software is as is. Diggity.
* [MIT License](http://www.opensource.org/licenses/mit-license.html)
* Lovingly crafted by Twilio Developer Education.
