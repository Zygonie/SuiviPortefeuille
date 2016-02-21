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
using SuiviPortefeuilleRBC.BusinessServices;
//using System.Data.Entity.Migrations;

namespace SuiviPortefeuilleRBC.Controllers
{
   public class ManagePortfolioController : Controller
   {
      private IStockServices stockServices;
      private IStockDescriptionServices stockDescriptionServices;
      private IPortfolioServices portfolioServices;
      private IOperationServices operationServices;

      public ManagePortfolioController(IStockServices stockServices, 
         IStockDescriptionServices stockDescriptionServices, 
         IPortfolioServices portfolioServices,
         IOperationServices operationServices)
      {
         this.stockServices = stockServices;
         this.stockDescriptionServices = stockDescriptionServices;
         this.portfolioServices = portfolioServices;
         this.operationServices = operationServices;
      }

      // GET: ManagePortfolio
      public ActionResult Index()
      {
         UpdateStockDescription();
         ManagePortfolioViewModel viewModel = new ManagePortfolioViewModel();
         viewModel.Portfolios = new List<SelectListItem>();
         foreach(var ptf in portfolioServices.GetPortfoliosForUser(User.Identity.Name))
         {
            viewModel.Portfolios.Add(new SelectListItem() { Text = ptf.Name, Value = ptf.PortfolioId.ToString() });
         }
         if(viewModel.Portfolios.Count > 0)
         {
            viewModel.CurrentPortfolioId = int.Parse(viewModel.Portfolios[0].Value);
         }
         return View(viewModel);
      }

      // GET: Partial list of stocks
      public ActionResult StockList(int currentPortfolioId)
      {
         //Voir pour passer le modele au complet 
         var stocks = stockServices.GetStockByPortfolioId(currentPortfolioId);
         stocks = stocks.OrderBy(s => s.Code);
         return PartialView("StockListPartialView", stocks);
      }

      // GET: AddOperation
      public ActionResult DisplayAddOperation(int currentPortfolioId)
      {
         Operation operation = new Operation(currentPortfolioId);
         operation.Sens = OperationOnStock.Buy;
         return PartialView("AddOperation", operation);
      }

      // POST: AddOperation
      [HttpPost]
      public ActionResult AddOperation(Operation operation)
      {
         if(ModelState.IsValid)
         {
            var existingStock = stockServices.GetSingle(s => s.Code == operation.Code && s.PortfolioId == operation.PortfolioId);

            if(existingStock == null)
            {
               var existingDescription = stockDescriptionServices.GetSingle(s => s.Code == operation.Code);
               Stock stock = null;
               if(existingDescription == null)
               {
                  stock = new Stock(operation);
               }
               else
               {
                  stock = new Stock(operation, existingDescription);
               }
               stockServices.CreateStock(stock);
            }
            else
            {
               existingStock.UpdateStock(operation);
               stockServices.UpdateStock(existingStock);
               UpdateStockDescription();
            }
            var stocks = stockServices.GetStockByPortfolioId(operation.PortfolioId);
            operationServices.CreateOperation(operation);
            return PartialView("StockListPartialView", stocks);
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
         Stock stock = stockServices.GetStockById(stockId);
         Operation operation = new Operation(stock.PortfolioId);
         operation.Sens = sens;
         operation.Code = stock.Code;
         return PartialView("AddOperation", operation);
      }

      #region Methods

      private void UpdateStockDescription()
      {
         using(Controllers.DetailedInfosStocksController controller = new Controllers.DetailedInfosStocksController())
         {
            IEnumerable<Models.DetailedQuoteQueryResultModel> infos = controller.RetrieveStockDetailedInfos();
            List<string> codes = infos.Select(p=>p.Symbol).ToList();            
            foreach(StockDescription description in stockDescriptionServices.GetMany(p=>codes.Contains(p.Code)))
            {
               var info = infos.Where(p => p.Symbol == description.Code).FirstOrDefault < Models.DetailedQuoteQueryResultModel>();
               description.FillInfos(info);
               stockDescriptionServices.UpdateStockDescription(description);
            }
         }
      }

      #endregion
   }
}