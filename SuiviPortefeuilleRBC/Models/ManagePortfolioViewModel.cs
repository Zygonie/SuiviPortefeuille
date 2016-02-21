using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuiviPortefeuilleRBC.Models
{
   public class ManagePortfolioViewModel
   {
      public List<SelectListItem> Portfolios { get; set; }
      public int CurrentPortfolioId { get; set; }
   }
}