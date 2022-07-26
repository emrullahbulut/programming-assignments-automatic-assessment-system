using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public RedirectToRouteResult Index()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}