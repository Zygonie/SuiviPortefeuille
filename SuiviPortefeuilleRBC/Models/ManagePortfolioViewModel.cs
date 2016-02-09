using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviPortefeuilleRBC.Models
{
   public class ManagePortfolioViewModel
   {
      public List<int> PortfolioIds { get; set; }
      public int CurrentPortfolioId { get; set; }
   }
}