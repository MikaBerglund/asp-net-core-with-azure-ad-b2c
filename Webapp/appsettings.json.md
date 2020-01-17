appsettings.json
================

The `appsettings.json` is excluded from the source control, since it contains sensitive data. In order to be able to run and debug this application, you need to create this file in the same folder with this document. The necessary contents of this file is described below.

``` JSON
{
  "AzureAdB2C": {
    "Instance": "https://<B2C tenant name>.b2clogin.com/tfp/",
    "ClientId": "<Client ID of application>",
    "CallbackPath": "/signin-oidc",
    "Domain": "<B2C tenant name>.onmicrosoft.com",
    "SignUpSignInPolicyId": "<Sign-in policy>",
    "ResetPasswordPolicyId": "<Reset password policy>",
    "EditProfilePolicyId": ""
  }
}

```

This is the default configuration data structure that you get when you create a new ASP.NET Core application with Visual Studio, and select Azure AD B2C as authentication. You do that by selecting **Individual User Accounts** under **Authentication** for your web application, and choose the **Connect to an existing user store in the cloud** option.