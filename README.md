<a href="https://www.twilio.com">
  <img src="https://static0.twilio.com/marketing/bundles/marketing/img/logos/wordmark-red.svg" alt="Twilio" width="250" />
</a>

# ETA Notifications Node. Powered by Twilio - ASP.NET MVC

![](https://github.com/TwilioDevEd/eta-notifications-csharp/workflows/NetFx/badge.svg)

GPS tracking implementation with ASP.NET MVC and Twilio

[Read the full tutorial here](https://www.twilio.com/docs/tutorials/walkthrough/eta-notifications/csharp/mvc)!

### Requirements

- [Visual Studio](https://visualstudio.microsoft.com/downloads/) 2019 or later.
- SQL Server Express 2019 with [LocalDB support](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

### Local development

1. First clone this repository and `cd` into it
   ```
   git clone git@github.com:TwilioDevEd/eta-notifications-csharp.git

   cd eta-notifications-csharp
   ```

1. Rename the file `ETANotifications/Local.config.example` to `ETANotifications/Local.config` and update the content with your info.

   Be sure to replace placeholders like `your_account_SID`, `your_twilio_auth_token` and `your_twilio_number` with valid information from your
   [Twilio Account Settings](https://www.twilio.com/user/account/settings).

   For better understanding while replacing ```[your-ngrok-subdomain]``` placeholder see [Exposing the application to Internet](#ngrok) section

1. Build the solution

1. Open `ETANotifications.Web/Migrations/Configuration.cs` and update the the list of orders accordingly to your requirements. 
    
    NOTE: You need to change the phone numbers with the ones you own so you can see the notifications.

1. Run migrations by executing the following in the Package Manager Console

  	```bash
    PM> Update-Database
    ```
    *(Be sure to check that your database server name matches the one from the connection string on `Web.config`. For reference, default values where used upon SQLServer installation)*

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
