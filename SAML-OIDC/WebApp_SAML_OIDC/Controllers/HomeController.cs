using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace WebApp_SAML_OIDC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [CustomAuthorization]
        //[Authorize]
        public ActionResult UserClaims()
        {
            var userClaims = ClaimsPrincipal.Current.Claims.ToList();
            return View(userClaims);
        }
    }
}