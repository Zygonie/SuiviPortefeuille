using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuiviPortefeuilleRBC.Models;
using SuiviPortefeuilleRBC.Repository;

namespace SuiviPortefeuilleRBC.BusinessServices
{
   public class StockDescriptionServices : IStockDescriptionServices
   {
      #region Fields

      private readonly UnitOfWork unitOfWork;

      #endregion

      #region

      public StockDescriptionServices(UnitOfWork unitOfWork)
      {
         this.unitOfWork = unitOfWork;
      }

      #endregion

      #region Interface implementation

      public Models.StockDescription GetStockDescriptionById(int stockDescriptionId)
      {
         return unitOfWork.StockDescriptionRepository.GetByID(stockDescriptionId);
      }

      public IEnumerable<Models.StockDescription> GetAllStockDescriptions()
      {
         return unitOfWork.StockDescriptionRepository.GetAll().ToList();
      }

      public string CreateStockDescription(Models.StockDescription stockDescription)
      {
         unitOfWork.StockDescriptionRepository.Insert(stockDescription);
         unitOfWork.Save();
         return stockDescription.Code;
      }

      public bool UpdateStockDescription(Models.StockDescription newStockDescription)
      {
         var success = false;
         if(newStockDescription != null)
         {
            //var description = unitOfWork.StockDescriptionRepository.GetByID(stockDescriptionId);
            //if(description != null)
            //{
            //   description.Amount = newStockDescription.Amount;
            //   description.AmountTarget = newStockDescription.AmountTarget;
            //   description.BookToValuePerShare = newStockDescription.BookToValuePerShare;
            //   description.ChangePercent = newStockDescription.ChangePercent;
            //   description.DividendPerShare = newStockDescription.DividendPerShare;
            //   description.DividendYield = description.DividendYield;
            //   description.EarningsPerShare = newStockDescription.EarningsPerShare;
            //   description.EpsEstimateCurrentYear = newStockDescription.EpsEstimateCurrentYear;
            //   description.ExDividendDate = newStockDescription.ExDividendDate;
            //   description.GrahamPrice = newStockDescription.GrahamPrice;
            //   description.GrahamSpread = newStockDescription.GrahamSpread;
            //   description.LastPrice = newStockDescription.LastPrice;
            //   description.NumberOfSharesTarget = newStockDescription.NumberOfSharesTarget;
            //   description.Payout = newStockDescription.Payout;
            //   description.PriceEarningRatio = newStockDescription.PriceEarningRatio;
            //   description.PriceTargetMax = newStockDescription.PriceTargetMax;
            //   description.PriceTargetMin = newStockDescription.PriceTargetMin;
            //   description.PriceToBook = newStockDescription.PriceToBook;
            //   description.PtfPercent = newStockDescription.PtfPercent;
            //   description.SpreadHighTargetPercent = newStockDescription.SpreadHighTargetPercent;
            //   description.SpreadLowTargetPercent = newStockDescription.SpreadLowTargetPercent;

            unitOfWork.StockDescriptionRepository.Update(newStockDescription);
               unitOfWork.Save();
               success = true;
            //}
         }
         return success;
      }

      public bool DeleteStockDescription(int stockDescriptionId)
      {
         var success = false;
         if(stockDescriptionId > 0)
         {
            var description = unitOfWork.StockDescriptionRepository.GetByID(stockDescriptionId);
            if(description != null)
            {
               unitOfWork.StockDescriptionRepository.Delete(description);
               unitOfWork.Save();
               success = true;
            }
         }
         return success;
      }

      public StockDescription GetSingle(Func<StockDescription, bool> where)
      {
         return unitOfWork.StockDescriptionRepository.GetSingle(where);
      }

      public IEnumerable<Models.StockDescription> GetMany(Func<StockDescription, bool> where)
      {
         return unitOfWork.StockDescriptionRepository.GetMany(where).ToList();
      }

      #endregion
   }
}