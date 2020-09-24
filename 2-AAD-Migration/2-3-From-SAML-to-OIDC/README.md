
# Migrate a SAML web application to use OpenIdConnect

## Scenario

You have a web application using SAML protocol on Azure AD and you would like to migrate it to use OpenId Connect protocol.

## About the sample

This sample uses a .NET MVC application registered on Microsoft Active Directory, to migrate the authentication protocol from SAML to OpenId Connect. The SAML web application project can be found in [chapter 1-1-Setup-SAML-Playground](https://github.com/Azure-Samples/ms-identity-dotnet-adfs-to-aad/tree/master/1-ADFS-Host/1-1-Setup-SAML-Playground).

This sample assumes that the SAML application uses `Microsoft.Owin.Security.WsFederation`, which will be changed to use `Microsoft.Owin.Security.OpenIdConnect` instead.

### Pre-requisites

- [Visual Studio](https://aka.ms/vsdownload)
- .NET Framework 4.7.2
- An Azure Active Directory (Azure AD) tenant. For more information on how to get an Azure AD tenant, see [How to get an Azure AD tenant](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/)

## Migrate from SAML to OpenId Connect

Microsoft Azure Active Directory supports [SAML protocol](https://docs.microsoft.com/azure/active-directory/develop/single-sign-on-saml-protocol) and [OpenId Connect protocol](https://docs.microsoft.com/azure/active-directory/develop/v2-protocols-oidc) for single sign-on authentication. While SAML is still used by the industry, companies are changing their applications to use OpenId Connect instead, due to its ability to connect to multiple Identity Providers (IdP), it is a newer protocol and when used with [OAuth2.0](https://docs.microsoft.com/azure/active-directory/develop/active-directory-v2-protocols), the application can have a distinction between authentication and authorization.   

If you have a SAML application registered on Azure AD, you can use the same application registration for the OpenId Connect configuration, requiring minimal code changes.

### Application changes in Azure Active Directory

First, sign in to the [Azure portal](https://portal.azure.com) and:

1. Note down the `TenantId`, which can be found in the [Azure Active Directory menu](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/Overview)
2. Note down the `ClientId` of the SAML application that is already registered on Azure AD. To find it, navigate to **App Registrations**, search for the application name and copy the value for the **Application (client) ID** column.
3. Add Microsoft Graph delegated permission `User.Read` to the application:
    - Open the application registration
    - Navigate to **API Permissions** on the left blade
    - Select **Add a permission**
    - Click on **Microsoft Graph** option
    - Select **Delegated permissions**
    - Search for `User.Read` and select it
    - Select `Add permissions` button

### Code Changes

Open the web application, and in the `web.config` file, add the following keys changing the values with the ones obtained previously:

```xml
    <add key="ida:ClientId" value="{Client_Id}" />
    <add key="ida:TenantId" value="{TenantId}" />
    <add key="ida:AADInstance" value="https://login.microsoftonline.com/" />
    <add key="ida:RedirectUri" value="https://localhost:44347/" />
```

1. Import the NuGet package `Microsoft.Owin.Security.OpenIdConnect`. 
1. Open the `Startup.Auth.cs` class under the `App_Start` folder.
1. Add the following variables
    ```c#
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string aadInstance = EnsureTrailingSlash(ConfigurationManager.AppSettings["ida:AADInstance"]);
        private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];
        private static string authority = String.Concat(aadInstance, tenantId, "/v2.0");
    ```
1. Replace the code for `app.UseWsFederationAuthentication` to
    ```c#
        app.UseOpenIdConnectAuthentication(
        new OpenIdConnectAuthenticationOptions
        {
            Authority = authority,
            ClientId = clientId,
            RedirectUri = redirectUri,
            PostLogoutRedirectUri = redirectUri,
            MetadataAddress = $"https://login.microsoftonline.com/{tenantId}/.well-known/openid-configuration?appid={clientId}"
        });
    ```
1. Open the `AccountController.cs` class and change `WsFederationAuthenticationDefaults.AuthenticationType` to `OpenIdConnectAuthenticationDefaults.AuthenticationType`.
   
### Testing the application

Clean and build the solution, then run the web application and sign-in with a user existing in your tenant. The user will get authenticated and the claims containing in the **Id Token** will be displayed in the screen.

