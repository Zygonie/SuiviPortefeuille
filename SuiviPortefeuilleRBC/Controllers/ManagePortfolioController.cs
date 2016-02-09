using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using SuiviPortefeuilleRBC.Models;
using System.Xml;
using System.Xml.Serialization;

//using System.Data.Entity.Migrations;

namespace SuiviPortefeuilleRBC.Controllers
{
   public class ManagePortfolioController : Controller
   {
      private ApplicationDbContext db = new ApplicationDbContext();

      public ManagePortfolioController()
      {
      }

      // GET: ManagePortfolio
      public ActionResult Index()
      {
         UpdateStockDescription();
         ManagePortfolioViewModel viewModel = new ManagePortfolioViewModel();
         viewModel.PortfolioIds = db.Portfolios.Select(p=>p.PortfolioId).Distinct().ToList();
         if(viewModel.PortfolioIds.Count > 0)
         {
            viewModel.CurrentPortfolioId = viewModel.PortfolioIds[0];
         }
         return View(viewModel);
      }

      // GET: Partial list of stocks
      public ActionResult StockList(int currentPortfolioId)
      {
         //Voir pour passer le modele au complet 
         return PartialView("StockListPartialView", db.Stocks.Where(p => p.PortfolioId == currentPortfolioId).ToList());
      }

      // GET: AddOperation
      public ActionResult DisplayAddOperation(int currentPortfolioId)
      {
         AddOperationViewModel addViewModel = new AddOperationViewModel(currentPortfolioId);
         addViewModel.Sens = OperationOnStock.Buy;
         return PartialView("AddOperation", addViewModel);
      }

      // POST: AddOperation
      [HttpPost]
      public ActionResult AddOperation(AddOperationViewModel addViewModel)
      {
         if(ModelState.IsValid)
         {
            var existingStock = db.Stocks.Where(s => s.Code == addViewModel.Code && s.PortfolioId == addViewModel.PortfolioId).FirstOrDefault<Stock>();

            if(existingStock == null)
            {
               var existingDescription = db.StockDescriptions.Where(s => s.Code == addViewModel.Code).FirstOrDefault<StockDescription>();
               Stock stock = null;
               if(existingDescription == null)
               {
                  stock = new Stock(addViewModel);
               }
               else
               {
                  stock = new Stock(addViewModel, existingDescription);
               }
               db.Stocks.Add(stock);
               db.SaveChanges();
            }
            else
            {
               existingStock.UpdateStock(addViewModel);
               db.Entry(existingStock).State = EntityState.Modified;
               db.SaveChanges();
               UpdateStockDescription();
            }

            return PartialView("StockListPartialView", db.Stocks.ToList());
         }
         return new JsonResult()
         {
            JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            Data = new { success = false }
         };
      }

      // GET: Buy more / sell stock
      public ActionResult BuySellStock(int stockId, OperationOnStock sens)
      {
         Stock stock = db.Stocks.Where(s=>s.StockId == stockId).FirstOrDefault();
         AddOperationViewModel addViewModel = new AddOperationViewModel(stock.PortfolioId);
         addViewModel.Sens = sens;
         addViewModel.Code = stock.Code;
         return PartialView("AddOperation", addViewModel);
      }

      #region Methods

      private void UpdateStockDescription()
      {
         using(Controllers.DetailedInfosStocksController controller = new Controllers.DetailedInfosStocksController())
         {
            IEnumerable<Models.DetailedQuoteQueryResultModel> infos = controller.RetrieveStockDetailedInfos();
            List<string> codes = infos.Select(p=>p.Symbol).ToList();
            foreach(StockDescription description in db.StockDescriptions.Where(p=>codes.Contains(p.Code)))
            {
               var info = infos.Where(p => p.Symbol == description.Code).FirstOrDefault < Models.DetailedQuoteQueryResultModel>();
               description.FillInfos(info);
               db.Entry(description).State = EntityState.Modified;
            }
            db.SaveChanges();
         }
      }

      #endregion
   }
}