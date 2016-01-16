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

      [AllowAnonymous]
      public ActionResult Queries()
      {
         ViewBag.Message = "A query test page";

         string url = @"https://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.quote where symbol in(""YHOO"",""AAPL"",""POT"")&env=store://datatables.org/alltableswithkeys";
         HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
         webReq.Method = "GET";
         HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();

         XmlTextReader reader = new XmlTextReader(webResponse.GetResponseStream());
         XmlSerializer serializer = new XmlSerializer(typeof(Models.SimpleQuoteQueryResultModel));

         var list = new List<Models.SimpleQuoteQueryResultModel>();
         while(reader.Read())
         {
            if(reader.Name == "quote" && reader.IsStartElement())
            {
               Models.SimpleQuoteQueryResultModel item = (Models.SimpleQuoteQueryResultModel)serializer.Deserialize(reader.ReadSubtree());
               list.Add(item);
            }
         }
         reader.Close();

         url = @"https://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.quotes where symbol in (""YHOO"",""AAPL"",""POT"")&env=store://datatables.org/alltableswithkeys";      
         webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
         webReq.Method = "GET";
         webResponse = (HttpWebResponse)webReq.GetResponse();

         reader = new XmlTextReader(webResponse.GetResponseStream());
         serializer = new XmlSerializer(typeof(Models.DetailedQuoteQueryResultModel));

         var detailedList = new List<Models.DetailedQuoteQueryResultModel>();
         while(reader.Read())
         {
            if(reader.Name == "quote" && reader.IsStartElement())
            {
               Models.DetailedQuoteQueryResultModel item = (Models.DetailedQuoteQueryResultModel)serializer.Deserialize(reader.ReadSubtree());
               detailedList.Add(item);
            }
         }
         reader.Close();
      
         ViewData["quote"] = list;
         ViewData["detailedQuote"] = detailedList;
         return View();
      }
   }
}