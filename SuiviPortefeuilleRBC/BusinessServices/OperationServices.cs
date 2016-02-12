using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuiviPortefeuilleRBC.Models;
using SuiviPortefeuilleRBC.Repository;

namespace SuiviPortefeuilleRBC.BusinessServices
{
   public class OperationServices : IOperationServices
   {
      #region Fields

      private readonly UnitOfWork unitOfWork;

      #endregion

      #region Constructor

      public OperationServices(UnitOfWork unitOfWork)
      {
         this.unitOfWork = unitOfWork;
      }

      #endregion

      #region Interface implementation

      public IEnumerable<Operation> GetOperationByPortfolioId(int portfolioId)
      {
         return unitOfWork.OperationRepository.GetMany(s => s.PortfolioId == portfolioId);
      }

      public Operation GetOperationById(int operationId)
      {
         return unitOfWork.OperationRepository.GetSingle(p => p.OperationId == operationId);
      }

      public IEnumerable<Operation> GetAllOperations()
      {
         return unitOfWork.OperationRepository.GetAll();
      }

      public int CreateOperation(Operation operation)
      {
         unitOfWork.OperationRepository.Insert(operation);
         unitOfWork.Save();
         return operation.OperationId;
      }

      public bool UpdateOperation(Operation newOperation)
      {
         var success = false;
         if(newOperation != null)
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
            unitOfWork.OperationRepository.Update(newOperation);
               unitOfWork.Save();
               success = true;
            //}
         }
         return success;
      }

      public bool DeleteOperation(int operationId)
      {
         var success = false;
         if(operationId > 0)
         {
            var operation = unitOfWork.OperationRepository.GetByID(operationId);
            if(operation != null)
            {
               unitOfWork.OperationRepository.Delete(operation);
               unitOfWork.Save();
               success = true;
            }
         }
         return success;
      }

      public Operation GetFirst(Func<Operation, bool> where)
      {
         return unitOfWork.OperationRepository.GetFirst(where);
      }

      #endregion
   }
}