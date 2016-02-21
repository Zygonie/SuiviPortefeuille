using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuiviPortefeuilleRBC.Models;

namespace SuiviPortefeuilleRBC.BusinessServices
{
   public interface IStockDescriptionServices
   {
      StockDescription GetStockDescriptionById(int stockDescriptionId);
      IEnumerable<StockDescription> GetAllStockDescriptions();
      string CreateStockDescription(StockDescription stockDescription);
      bool UpdateStockDescription(StockDescription stockDescription);
      bool DeleteStockDescription(int stockDescriptionId);
      StockDescription GetSingle(Func<StockDescription, bool> where);
      IEnumerable<Models.StockDescription> GetMany(Func<StockDescription, bool> where);
   }
}
