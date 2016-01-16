using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuiviPortefeuilleRBC.Models;

namespace SuiviPortefeuilleRBC.Controllers
{
   public class PortfolioApiController : ApiController
   {
      private ApplicationDbContext db = new ApplicationDbContext();

      // GET api/<controller>
      public IEnumerable<string> Get()
      {
         return new string[] { "value1", "value2" };
      }

      // GET api/<controller>/5
      public string Get(int id)
      {
         return "value";
      }

      // POST api/<controller>
      public void Post([FromBody]string value)
      {
      }

      // PUT api/<controller>/5
      public void Put(int id, [FromBody]string value)
      {
      }

      // DELETE api/<controller>/5
      public void Delete(int id)
      {
      }


      [HttpPost]
      [Authorize(Roles = "canEdit")]
      public HttpResponseMessage Post([FromUri] Operation operation)
      {
         if(ModelState.IsValid)
         {
            db.Operations.Add(operation);
            db.SaveChanges();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, operation);
            return response;
         }
         HttpResponseMessage badResponse = Request.CreateResponse(HttpStatusCode.NotAcceptable);
         return badResponse;
      }


   }
}