using Microsoft.Identity.Client;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;

namespace WebApp_SAML.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userClaims = ClaimsPrincipal.Current.Claims.ToList();
            return View(userClaims);
        }

        public async Task<ActionResult> UserProfile()
        {
            var app = PublicClientApplicationBuilder.Create("992f2eec-20ec-4eb4-952b-2d974f0db6ea")
                .WithAuthority("https://login.microsoftonline.com/979f4440-75dc-4664-b2e1-2cafa0ac67d1/v2.0")
                .Build();

            // Set public client true
            // Grant consent via portal

            var result = await app.AcquireTokenByIntegratedWindowsAuth(new string[] { "User.Read" })
                .ExecuteAsync()
                .ConfigureAwait(false);

            GraphServiceClient graphServiceClient = new GraphServiceClient("https://graph.microsoft.com/v1.0",
                new DelegateAuthenticationProvider(
                                                                         async (requestMessage) =>
                                                                         {
                                                                             await Task.Run(() =>
                                                                             {
                                                                                 requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);
                                                                             });
                                                                         }));

            var me = await graphServiceClient.Me.Request()
                .GetAsync();

            return View(me);
        }

    }
}