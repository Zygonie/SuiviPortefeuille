using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuiviPortefeuilleRBC.Models;

namespace SuiviPortefeuilleRBC.BusinessServices
{
   public interface IPortfolioServices
   {
      IEnumerable<Portfolio> GetPortfoliosForUser(string username);
      Portfolio GetPortfolioById(int portfolioId);
      IEnumerable<Portfolio> GetAllPortfolios();
      int CreatePortfolio(Portfolio portfolio);
      bool UpdatePortfolio(int portfolioId, Portfolio portfolio);
      bool DeletePortfolio(int portfolioId);
   }
}
