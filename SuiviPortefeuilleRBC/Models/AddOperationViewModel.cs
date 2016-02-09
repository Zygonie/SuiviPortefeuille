using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SuiviPortefeuilleRBC.Models
{
   public class AddOperationViewModel
   {
      #region Properties

      public string Code { get; set; }
      public int NumberOfShares { get; set; }
      public double UnitaryPrice { get; set; }
      public double Fees { get; set; }
      public int PortfolioId { get; set; }
      public OperationOnStock Sens {get;set;}

      #endregion

      #region Constructors

      public AddOperationViewModel()
      {
      }

      public AddOperationViewModel(int portfolioId)
      {
         this.PortfolioId = portfolioId;
      }

      #endregion
   }
}