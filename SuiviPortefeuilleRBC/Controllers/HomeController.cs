using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace SuiviPortefeuilleRBC.Controllers
{
   public class HomeController : Controller
   {
      [AllowAnonymous]
      public ActionResult Index()
      {
         return View();
      }

      [AllowAnonymous]
      public ActionResult About()
      {
         ViewBag.Message = "Your application description page.";
         return View();
      }

      [AllowAnonymous]
      public ActionResult Contact()
      {
         ViewBag.Message = "Your contact page.";
         return View();
      }
      
      public ActionResult ManagePortfolio()
      {
         ViewBag.Title = "Manage Portfolio";
         return View();
      }
   }
}