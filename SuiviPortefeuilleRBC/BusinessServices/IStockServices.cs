using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuiviPortefeuilleRBC.Models;

namespace SuiviPortefeuilleRBC.BusinessServices
{
   public interface IStockServices
   {
      IEnumerable<Models.Stock> GetStockByPortfolioId(int portfolioId);
      Stock GetStockById(int stockId);
      IEnumerable<Stock> GetAllStocks();
      int CreateStock(Stock stock);
      bool UpdateStock(Stock stock);
      bool DeleteStock(int stockId);
      Stock GetFirst(Func<Stock, bool> where);
      IEnumerable<int> GetStockPortfolioIdsHavingStock(string code);
   }
}