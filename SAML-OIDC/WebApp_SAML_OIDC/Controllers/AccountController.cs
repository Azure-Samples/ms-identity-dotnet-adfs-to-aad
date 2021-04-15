using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.WsFederation;
using System.Security.Claims;
using System.Security.Principal;

namespace WebApp_SAML_OIDC.Controllers
{
    public class AccountController : Controller
    {
        public void SignIn(bool isOIDC)
        {
            
            if (!Request.IsAuthenticated)
            {
                if (isOIDC)
                {
                    // Send an OpenID Connect sign-in request.
                    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "https://localhost:44347/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
                }
                else
                {
                    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "https://localhost:44347/" },
                        WsFederationAuthenticationDefaults.AuthenticationType);
                }
            }
        }

        public void SignOut()
        {
            string callbackUrl = Url.Action("SignOutCallback", "Account", routeValues: null, protocol: Request.Url.Scheme);

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(Constants.AuthenticationType);

            if (claim != null && claim.Value == OpenIdConnectAuthenticationDefaults.AuthenticationType)
            {
                HttpContext.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
            }
            else
            {
                HttpContext.GetOwinContext().Authentication.SignOut(
                    new AuthenticationProperties { RedirectUri = callbackUrl },
                    WsFederationAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
            }
        }

        public ActionResult SignOutCallback()
        {
            if (Request.IsAuthenticated)
            {
                // Redirect to home page if the user is authenticated.
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
