using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using SuiviPortefeuilleRBC.Models;


namespace SuiviPortefeuilleRBC.Controllers
{
    public class ManagePortfolioController : Controller
    {
       private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ManagePortfolio
        public ActionResult Index()
        {
           List<Stock> stocks = db.Stocks.ToList();
           List<ManagePortfolioViewModel> data = new List<ManagePortfolioViewModel>();
           foreach(Stock stock in stocks)
           {
              ManagePortfolioViewModel entry = new ManagePortfolioViewModel();
              entry.Code = stock.Description.Code;
              entry.Name = stock.Description.Name;
              entry.NumberOfShare = stock.NumberOfShares;
              entry.Pru = stock.UnitaryPrice;


              
              //entry.DailyVariation = ;
              //entry.PerformanceCash = ;
              //entry.PerformancePercent=;
           }
            return View(db.Portfolios.FirstOrDefault());
        }
    }
}