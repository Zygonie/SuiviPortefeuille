using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SuiviPortefeuilleRBC.Models
{
   public class Operation
   {
      public int OperationId { get; set; }
      [Required]
      public StockDescription Stock { get; set; }
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
      public DateTime? Date { get; set; }
   }
}