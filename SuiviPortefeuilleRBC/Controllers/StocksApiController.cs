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
   public class StocksApiController : ApiController
   {
      private ApplicationDbContext db = new ApplicationDbContext();

      // GET: api/StocksApi
      public IEnumerable<Models.DetailedQuoteQueryResultModel> Get()
      {
         List<Stock> stocks = db.Stocks.Include("Description").ToList();
         List<string> codeList = new List<string>();
         foreach(Stock stock in stocks)
         {
            codeList.Add(stock.Description.Code);
         }

         string urlPrefix = @"https://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.quotes where symbol in (";
         string codes = string.Join(@""",""", codeList);
         string urlSuffix = ")&env=store://datatables.org/alltableswithkeys";
         string url = string.Format(@"{0}""{1}""{2}", urlPrefix, codes, urlSuffix);
         HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
         webReq.Method = "GET";
         HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();

         XmlTextReader reader = new XmlTextReader(webResponse.GetResponseStream());
         XmlSerializer serializer = new XmlSerializer(typeof(Models.DetailedQuoteQueryResultModel));

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

         return detailedList;
      }

      // GET: api/StocksApi/5
      public string Get(int id)
      {
         return "value";
      }

      // POST: api/StocksApi
      public void Post([FromBody]string value)
      {
      }

      // PUT: api/StocksApi/5
      public void Put(int id, [FromBody]string value)
      {
      }

      // DELETE: api/StocksApi/5
      public void Delete(int id)
      {
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
