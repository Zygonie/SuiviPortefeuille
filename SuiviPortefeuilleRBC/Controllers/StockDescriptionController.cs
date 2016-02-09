using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SuiviPortefeuilleRBC.Models;

namespace SuiviPortefeuilleRBC.Controllers
{
   public class StockDescriptionController : Controller
   {
      private ApplicationDbContext db = new ApplicationDbContext();

      #region HTTP requests

      // GET: StockDescription
      public ActionResult Index()
      {
         return View(db.StockDescriptions.ToList());
      }

      // GET: StockDescription/Details/5
      public ActionResult Details(int? id)
      {
         if(id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         StockDescription stockDescription = db.StockDescriptions.Find(id);
         if(stockDescription == null)
         {
            return HttpNotFound();
         }
         return View(stockDescription);
      }

      // GET: StockDescription/Create
      [Authorize(Roles = "canEdit")]
      public ActionResult Create()
      {
         return View();
      }

      // POST: StockDescription/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [Authorize(Roles = "canEdit")]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "StockDescriptionId,Name,LastPrice,ChangePercent,SpreadLowTargetPercent,SpreadHighTargetPercent,PriceTargetMin,PriceTargetMax,DividendYield,Payout,PriceEarningRatio,DividendPerShare,EarningsPerShare,EpsEstimateCurrentYear,PriceToBook,ExDividendDate,BookToValuePerShare,GrahamPrice,GrahamSpread,PtfPercent,Amount,NumberOfSharesTarget,AmountTarget")] StockDescription stockDescription)
      {
         if(ModelState.IsValid)
         {
            db.StockDescriptions.Add(stockDescription);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         return View(stockDescription);
      }

      // GET: StockDescription/Edit/5
      [Authorize(Roles = "canEdit")]
      public ActionResult Edit(int? id)
      {
         if(id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         StockDescription stockDescription = db.StockDescriptions.Find(id);
         if(stockDescription == null)
         {
            return HttpNotFound();
         }
         return View(stockDescription);
      }

      // POST: StockDescription/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [Authorize(Roles = "canEdit")]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "StockDescriptionId,Name,LastPrice,ChangePercent,SpreadLowTargetPercent,SpreadHighTargetPercent,PriceTargetMin,PriceTargetMax,DividendYield,Payout,PriceEarningRatio,DividendPerShare,EarningsPerShare,EpsEstimateCurrentYear,PriceToBook,ExDividendDate,BookToValuePerShare,GrahamPrice,GrahamSpread,PtfPercent,Amount,NumberOfSharesTarget,AmountTarget")] StockDescription stockDescription)
      {
         if(ModelState.IsValid)
         {
            db.Entry(stockDescription).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         return View(stockDescription);
      }

      // GET: StockDescription/Delete/5
      [Authorize(Roles = "canEdit")]
      public ActionResult Delete(int? id)
      {
         if(id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         StockDescription stockDescription = db.StockDescriptions.Find(id);
         if(stockDescription == null)
         {
            return HttpNotFound();
         }
         return View(stockDescription);
      }

      // POST: StockDescription/Delete/5
      [HttpPost, ActionName("Delete")]
      [Authorize(Roles = "canEdit")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         StockDescription stockDescription = db.StockDescriptions.Find(id);
         db.StockDescriptions.Remove(stockDescription);
         db.SaveChanges();
         return RedirectToAction("Index");
      }

      #endregion

      #region Help Methods
      
      //see https://developer.yahoo.com/yql/console/?q=show%20tables&env=store://datatables.org/alltableswithkeys to build custom YQL request

      #region Dividend history
      //Dividend History
      //select * from yahoo.finance.dividendhistory where symbol = "KO" and startDate = "1962-01-01" and endDate = "2013-12-31"
      //https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.dividendhistory%20where%20symbol%20%3D%20%22KO%22%20and%20startDate%20%3D%20%221962-01-01%22%20and%20endDate%20%3D%20%222013-12-31%22&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys
      #endregion
      #region Historical data
      //Historical data
      //select * from yahoo.finance.historicaldata where symbol = "YHOO" and startDate = "2009-09-11" and endDate = "2010-03-10"
      //https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.historicaldata%20where%20symbol%20%3D%20%22YHOO%22%20and%20startDate%20%3D%20%222009-09-11%22%20and%20endDate%20%3D%20%222010-03-10%22&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys
      #endregion
     
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
