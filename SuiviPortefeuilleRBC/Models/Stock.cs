using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SuiviPortefeuilleRBC.Models
{
   public class Stock
   {
      public int StockId { get; set; }
      [ForeignKey("Description")]
      public int StockDescriptionId { get; set; }
      public StockDescription Description { get; set; }
      [Required]
      public int NumberOfShares { get; set; }
      public double InvestedValue { get; set; }
      [Required]
      public double UnitaryPrice { get; set; }
      public double DividendYield { get; set; }
      public double Return { get; set; }
   }
}