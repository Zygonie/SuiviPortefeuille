using System.Web;
using System.Web.Optimization;

namespace SuiviPortefeuilleRBC
{
   public class BundleConfig
   {
      // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
      public static void RegisterBundles(BundleCollection bundles)
      {
         bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                     "~/Scripts/jquery-{version}.js",
                     "~/Scripts/jquery-ui-{version}.js"));

         bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                     "~/Scripts/jquery.validate*",
                     "~/Scripts/jquery.unobtrusive*"));
         
         // Use the development version of Modernizr to develop with and learn from. Then, when you're
         // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
         bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                     "~/Scripts/modernizr-*"));

         bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                   "~/Scripts/bootstrap.js",
                   //"~/Scripts/moment.js",
                   "~/Scripts/bootstrap-datepicker.js",
                   //"~/Scripts/bootstrap-datetimepicker.js",
                   "~/Scripts/respond.js"));

         bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/Content/bootstrap.css",
                   "~/Content/bootstrap-datepicker.css",
                   //"~/Content/bootstrap-datetimepicker.css",
                   "~/Content/site.css",
                   "~/Content/zocial.css"));

         bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
            "~/Scripts/jquery.signalR-{version}.js",
            "~/Scripts/SuiviPortefeuilleRBC/ManagePortfolio.js",
            "~/signalr/hubs"));
      }
   }
}


//http://forums.asp.net/t/1975676.aspx?Tutorial+for+Adding+Datepicker+in+MVC+5
//http://www.asp.net/mvc/overview/older-versions/using-the-html5-and-jquery-ui-datepicker-popup-calendar-with-aspnet-mvc/using-the-html5-and-jquery-ui-datepicker-popup-calendar-with-aspnet-mvc-part-4
//http://stackoverflow.com/questions/21104633/how-to-add-date-picker-bootstrap-3-on-mvc-5-project-using-the-razor-engine