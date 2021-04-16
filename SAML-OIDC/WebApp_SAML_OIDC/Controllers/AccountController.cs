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
        /// <summary>
        /// Send an authentication request based on input parameter named isOIDC.
        /// If isOIDC is true then sign-in using OpenID Connect.
        /// Else sign-in using SAML.
        /// </summary>
        /// <param name="isOIDC"></param>
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
                    // Send a Federation sign-in request.
                    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "https://localhost:44347/" },
                        WsFederationAuthenticationDefaults.AuthenticationType);
                }
            }
        }

        /// <summary>
        /// Checks the claims collection for AuthenticationType claim. 
        /// Send the sign-out request based on the presence and value of the AuthenticationType claim.
        /// </summary>
        public void SignOut()
        {
            string callbackUrl = Url.Action("SignOutCallback", "Account", routeValues: null, protocol: Request.Url.Scheme);

            var claimsIdentity = (ClaimsIdentity)User.Identity;

            // Retreive AuthenticationType claim from ClaimsCollection. 
            var claim = claimsIdentity.FindFirst(Constants.AuthenticationType);

            // If AuthenticationType claim exists then sign-out the user on the basis of OpenID Connect.
            if (claim != null && claim.Value == OpenIdConnectAuthenticationDefaults.AuthenticationType)
            {
                HttpContext.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
            }

            // If AuthenticationType does not exist then sign-out the user on the basis of Federation.
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
