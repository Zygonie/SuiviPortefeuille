using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;
using SuiviPortefeuilleRBC.Models;

namespace SuiviPortefeuilleRBC.Controllers
{
   public class SimpleInfosStocksApiController : ApiController
   {
      #region Fields

      private ApplicationDbContext db = new ApplicationDbContext();

      #endregion

      #region API

      // GET: api/SimpleInfosStocks
      public IEnumerable<Models.SimpleQuoteQueryResultModel> Get()
      {
         return RetrieveStockSimpleInfos();
      }

      #endregion

      #region Methods
      
      public IEnumerable<Models.SimpleQuoteQueryResultModel> RetrieveStockSimpleInfos()
      {
         List<string> codeList = db.Stocks.Select(m => m.Code).Distinct().ToList();
         return RetrieveStockSimpleInfos(codeList);
      }

      public SimpleQuoteQueryResultModel RetrieveStockSimpleInfos(string code)
      {
         IEnumerable<SimpleQuoteQueryResultModel> infosList = RetrieveStockSimpleInfos(new List<string>() { code });
         return infosList.FirstOrDefault();
      }

      public IEnumerable<Models.SimpleQuoteQueryResultModel> RetrieveStockSimpleInfos(List<string> codeList)
      {
         string urlPrefix = @"https://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.quote where symbol in (";
         string codes = string.Join(@""",""", codeList);
         string urlSuffix = ")&env=store://datatables.org/alltableswithkeys";
         string url = string.Format(@"{0}""{1}""{2}", urlPrefix, codes, urlSuffix);
         HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
         webReq.Method = "GET";
         HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();

         XmlTextReader reader = new XmlTextReader(webResponse.GetResponseStream());
         XmlSerializer serializer = new XmlSerializer(typeof(Models.SimpleQuoteQueryResultModel));

         var detailedList = new List<Models.SimpleQuoteQueryResultModel>();
         while(reader.Read())
         {
            if(reader.Name == "quote" && reader.IsStartElement())
            {
               Models.SimpleQuoteQueryResultModel item = (Models.SimpleQuoteQueryResultModel)serializer.Deserialize(reader.ReadSubtree());
               detailedList.Add(item);
            }
         }
         reader.Close();
         return detailedList;
      }

      #endregion

      protected override void Dispose(bool disposing)
      {
         if(disposing)
         {
            db.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}
