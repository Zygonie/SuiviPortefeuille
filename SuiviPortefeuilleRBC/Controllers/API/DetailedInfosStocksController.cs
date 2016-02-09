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
   public class DetailedInfosStocksController : ApiController
   {
      #region Fields

      private ApplicationDbContext db = new ApplicationDbContext();

      #endregion

      #region API

      // GET: api/DetailedInfosStocks
      public IEnumerable<Models.DetailedQuoteQueryResultModel> Get()
      {
         return RetrieveStockDetailedInfos();
      }

      #endregion

      #region Methods

      public IEnumerable<DetailedQuoteQueryResultModel> RetrieveStockDetailedInfos()
      {
         List<string> codeList = db.Stocks.Select(m => m.Code).Distinct().ToList();
         return RetrieveStockDetailedInfos(codeList);
      }

      public DetailedQuoteQueryResultModel RetrieveStockDetailedInfos(string code)
      {
         IEnumerable<DetailedQuoteQueryResultModel> infosList = RetrieveStockDetailedInfos(new List<string>() { code });
         return infosList.FirstOrDefault();
      }

      public IEnumerable<DetailedQuoteQueryResultModel> RetrieveStockDetailedInfos(List<string> codeList)
      {
         string urlPrefix = @"https://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.quotes where symbol in (";
         string codes = string.Join(@""",""", codeList);
         string urlSuffix = ")&env=store://datatables.org/alltableswithkeys";
         string url = string.Format(@"{0}""{1}""{2}", urlPrefix, codes, urlSuffix);
         HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
         webReq.Method = "GET";
         HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();

         XmlTextReader reader = new XmlTextReader(webResponse.GetResponseStream());
         XmlSerializer serializer = new XmlSerializer(typeof(DetailedQuoteQueryResultModel));

         var detailedList = new List<Models.DetailedQuoteQueryResultModel>();
         while(reader.Read())
         {
            if(reader.Name == "quote" && reader.IsStartElement())
            {
               DetailedQuoteQueryResultModel item = (DetailedQuoteQueryResultModel)serializer.Deserialize(reader.ReadSubtree());
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
      //[HttpPost]
      //[Authorize(Roles = "canEdit")]
      //public HttpResponseMessage Post([FromUri] Operation operation)
      //{
      //   if(ModelState.IsValid)
      //   {
      //      db.Operations.Add(operation);
      //      db.SaveChanges();
      //      HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, operation);
      //      return response;
      //   }
      //   HttpResponseMessage badResponse = Request.CreateResponse(HttpStatusCode.NotAcceptable);
      //   return badResponse;
      //}
   }
}
