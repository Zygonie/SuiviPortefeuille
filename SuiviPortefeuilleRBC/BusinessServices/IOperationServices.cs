using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuiviPortefeuilleRBC.Models;

namespace SuiviPortefeuilleRBC.BusinessServices
{
   public interface IOperationServices
   {
      IEnumerable<Operation> GetOperationByPortfolioId(int portfolioId);
      Operation GetOperationById(int stockId);
      IEnumerable<Operation> GetAllOperations();
      int CreateOperation(Operation stock);
      bool UpdateOperation(Operation stock);
      bool DeleteOperation(int stockId);
      Operation GetFirst(Func<Operation, bool> where);
   }
}