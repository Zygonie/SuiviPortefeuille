using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuiviPortefeuilleRBC.Models;
using SuiviPortefeuilleRBC.Repository;

namespace SuiviPortefeuilleRBC.BusinessServices
{
   public class StockServices : IStockServices
   {
      #region Fields

      private readonly UnitOfWork unitOfWork;

      #endregion

      #region

      public StockServices(UnitOfWork unitOfWork)
      {
         this.unitOfWork = unitOfWork;
      }

      #endregion

      #region Interface implementation

      public IEnumerable<Models.Stock> GetStockByPortfolioId(int portfolioId)
      {
         return unitOfWork.StockRepository.GetMany(s => s.PortfolioId == portfolioId).ToList();
      }

      public IEnumerable<int> GetStockPortfolioIdsHavingStock(string code)
      {
         return unitOfWork.StockRepository.GetMany(s => s.Code == code).Select(s=>s.PortfolioId).ToList();
      }

      public Models.Stock GetStockById(int stockId)
      {
         return unitOfWork.StockRepository.GetSingle(p => p.StockId == stockId);
      }

      public IEnumerable<Models.Stock> GetAllStocks()
      {
         return unitOfWork.StockRepository.GetAll().ToList();
      }

      public int CreateStock(Models.Stock stock)
      {
         unitOfWork.StockRepository.Insert(stock);
         unitOfWork.Save();
         return stock.StockId;
      }

      public bool UpdateStock(Models.Stock newStock)
      {
         var success = false;
         if(newStock != null)
         {
            //var stock = unitOfWork.StockRepository.GetByID(stockId);
            //if(stock != null)
            //{
            //   stock.Description = newStock.Description;
            //   stock.InvestedValue = newStock.InvestedValue;
            //   stock.NumberOfShares = newStock.NumberOfShares;
            //   stock.PerformanceCash = newStock.PerformanceCash;
            //   stock.PerformancePercent = newStock.PerformancePercent;
            //   stock.UnitaryPrice = newStock.UnitaryPrice;
            //   unitOfWork.StockRepository.Update(stock);
               unitOfWork.StockRepository.Update(newStock);
               unitOfWork.Save();
               success = true;
            //}
         }
         return success;
      }

      public bool DeleteStock(int stockId)
      {
         var success = false;
         if(stockId > 0)
         {
            var stock = unitOfWork.StockRepository.GetByID(stockId);
            if(stock != null)
            {
               unitOfWork.StockRepository.Delete(stock);
               unitOfWork.Save();
               success = true;
            }
         }
         return success;
      }

      public Stock GetFirst(Func<Stock, bool> where)
      {
         return unitOfWork.StockRepository.GetFirst(where);
      }

      #endregion
   }
}