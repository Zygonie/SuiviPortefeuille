using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SuiviPortefeuilleRBC.Models
{
   public class Portfolio
   {
      public int PortfolioId { get; set; }
      [Required]
      public string Name { get; set; }
      [Required]
      public double TargetValue { get; set; }
      public double Value { get; set; }
      public double Liquidity { get; set; }
      public List<Stock> Stocks { get; set; }
      public List<Operation> Operations { get; set; }
   }
}