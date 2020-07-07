using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.WsFederation;
using Owin;

namespace WebApp_SAML
{
    public partial class Startup
    {
        private static string realm = ConfigurationManager.AppSettings["ida:Wtrealm"];
        private static string adfsMetadata = ConfigurationManager.AppSettings["ida:ADFSMetadata"];

        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string authority = String.Concat(aadInstance, tenantId, "/v2.0");
        private static string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];

        public void ConfigureAuth(IAppBuilder app)
        {
            IdentityModelEventSource.ShowPII = true;
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            //app.UseWsFederationAuthentication(
            //    new WsFederationAuthenticationOptions
            //    {
            //        Wtrealm = realm,
            //        MetadataAddress = adfsMetadata,
            //        Wreply = "https://localhost:44347/"
            //    });

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    // The `Authority` represents the v2.0 endpoint - https://login.microsoftonline.com/{tenantId}/v2.0
                    Authority = authority,
                    ClientId = clientId,
                    RedirectUri = redirectUri,
                    PostLogoutRedirectUri = redirectUri
                    

                    // Handling SameSite cookie according to https://docs.microsoft.com/en-us/aspnet/samesite/owin-samesite
                    //CookieManager = new SameSiteCookieManager(
                    //                 new SystemWebCookieManager())
                }); ;
        }
    }
}