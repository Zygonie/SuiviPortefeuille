using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuiviPortefeuilleRBC.Models;
using SuiviPortefeuilleRBC.Repository;

namespace SuiviPortefeuilleRBC.BusinessServices
{
   public class PortfolioServices : IPortfolioServices
   {
      #region Fields

      private readonly UnitOfWork unitOfWork;

      #endregion

      #region

      public PortfolioServices(UnitOfWork unitOfWork)
      {
         this.unitOfWork = unitOfWork;
      }

      #endregion

      #region Interface implementation

      public IEnumerable<int> GetIds()
      {
         return unitOfWork.PortfolioRepository.GetManySelect(p => p.PortfolioId).ToList();
      }

      public Models.Portfolio GetPortfolioById(int portfolioId)
      {
         return unitOfWork.PortfolioRepository.GetByID(portfolioId);
      }

      public IEnumerable<Models.Portfolio> GetAllPortfolios()
      {
         return unitOfWork.PortfolioRepository.GetAll().ToList();
      }

      public int CreatePortfolio(Models.Portfolio portfolio)
      {
         unitOfWork.PortfolioRepository.Insert(portfolio);
         unitOfWork.Save();
         return portfolio.PortfolioId;
      }

      public bool UpdatePortfolio(int portfolioId, Models.Portfolio newPortfolio)
      {
         var success = false;
         if(newPortfolio != null)
         {
            var portfolio = unitOfWork.PortfolioRepository.GetByID(portfolioId);
            if(portfolio != null)
            {
               portfolio.Liquidity = newPortfolio.Liquidity;
               portfolio.Name = newPortfolio.Name;
               portfolio.TargetValue = newPortfolio.TargetValue;
               portfolio.Value = newPortfolio.Value;
               unitOfWork.PortfolioRepository.Update(portfolio);
               unitOfWork.Save();
               success = true;
            }
         }
         return success;
      }

      public bool DeletePortfolio(int portfolioId)
      {
         var success = false;
         if(portfolioId > 0)
         {
            var stock = unitOfWork.PortfolioRepository.GetByID(portfolioId);
            if(stock != null)
            {
               unitOfWork.PortfolioRepository.Delete(stock);
               unitOfWork.Save();
               success = true;
            }
         }
         return success;
      }

      #endregion
   }
}