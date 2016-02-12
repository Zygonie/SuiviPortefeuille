using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SuiviPortefeuilleRBC.Models
{
   public class Operation
   {
      #region

      public int OperationId { get; set; }
      [Required]
      public string Code { get; set; }
      [Required]
      public OperationOnStock Sens { get; set; }
      [Required]
      public int NumberOfShares { get; set; }
      [Required]
      public double Fees { get; set; }
      [Required]
      public double Price { get; set; }
      [Required]
      public double Amount { get; set; }
      [Required]
      public int PortfolioId { get; set; }
      [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
      public DateTime? Date { get; set; }

      #endregion

      #region Constructors

      public Operation()
      {
      }

      public Operation(int portfolioId)
      {
         this.PortfolioId = portfolioId;
      }

      #endregion
   }
}