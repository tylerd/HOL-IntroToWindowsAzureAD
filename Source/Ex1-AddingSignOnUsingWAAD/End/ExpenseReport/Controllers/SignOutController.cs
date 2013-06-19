﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IdentityModel.Services;
using System.IdentityModel.Services.Configuration;

namespace ExpenseReport.Controllers
{
    public class SignOutController : Controller
    {
        public ActionResult Index()
        {
            return View("SignOut");
        }

        public void SignOut()
        {
            WsFederationConfiguration fc =
                   FederatedAuthentication.FederationConfiguration.WsFederationConfiguration;

            string request = System.Web.HttpContext.Current.Request.Url.ToString();
            string wreply = request.Substring(0, request.Length - 7);

            SignOutRequestMessage soMessage =
                            new SignOutRequestMessage(new Uri(fc.Issuer), wreply);
            soMessage.SetParameter("wtrealm", fc.Realm);

            FederatedAuthentication.SessionAuthenticationModule.SignOut();
            Response.Redirect(soMessage.WriteQueryString());
        }
    }
}
