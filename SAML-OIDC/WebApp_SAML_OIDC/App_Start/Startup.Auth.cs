using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.WsFederation;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using System.Security.Claims;

namespace WebApp_SAML_OIDC
{
    public partial class Startup
    {
        // URI that identifies the Relying Party.
        private static string realm = ConfigurationManager.AppSettings["ida:Wtrealm"];
        //Address to retrieve the wsFederation metadata.
        private static string adfsMetadata = ConfigurationManager.AppSettings["ida:ADFSMetadata"];

        // The Client ID used by the application to uniquely identify itself to Azure AD.
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        // Tenant ID where the application is registered (e.g. Z963c147-dc13-433f-9520-6db1ff177c34).
        private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        private static string aadInstance = EnsureTrailingSlash(ConfigurationManager.AppSettings["ida:AADInstance"]);
        // Authority is the URL for authority, composed by Microsoft identity platform endpoint and the tenant name (e.g. https://login.microsoftonline.com/contoso.onmicrosoft.com/v2.0)
        private static string authority = aadInstance + tenantId;

        /// <summary>
        /// Configure OWIN to use OpenID Connect and SAML
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            // Adds the OpenIdConnectAuthenticationMiddleware into the OWIN runtime to authenticate using OpenID Connect.
            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = authority,
                    PostLogoutRedirectUri = postLogoutRedirectUri,
                    Notifications = new OpenIdConnectAuthenticationNotifications()
                    {
                        SecurityTokenValidated = context =>
                        {
                            var identity = context.AuthenticationTicket.Identity;

                            // Adds AuthenticationType claim in claims collection to verify if the user has logged-in using OIDC or SAML
                            Claim claim = new Claim(Constants.AuthenticationType, OpenIdConnectAuthenticationDefaults.AuthenticationType);
                            identity.AddClaim(claim);

                            return Task.FromResult(0);
                        }
                    }
                });

            // Adds the WsFederationAuthenticationMiddleware into the OWIN runtime to authenticate using SAML.
            app.UseWsFederationAuthentication(
                new WsFederationAuthenticationOptions
                {
                    Wtrealm = realm,
                    MetadataAddress = adfsMetadata,
                    Wreply = "https://localhost:44347/"
                 });
            IdentityModelEventSource.ShowPII = true;
        }

        private static string EnsureTrailingSlash(string value)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            if (!value.EndsWith("/", StringComparison.Ordinal))
            {
                return value + "/";
            }

            return value;
        }
    }
}
