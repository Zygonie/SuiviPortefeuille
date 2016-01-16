using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuiviPortefeuilleRBC.Models
{
   public class StockDescription
   {
      public int StockDescriptionId { get; set; }
      [Required]
      public string Name { get; set; }
      [Required]
      public string Code { get; set; }
      public double LastPrice { get; set; }
      public double ChangePercent { get; set; }
      public double SpreadLowTargetPercent { get; set; }
      public double SpreadHighTargetPercent { get; set; }
      public double PriceTargetMin { get; set; }
      public double PriceTargetMax { get; set; }
      public double DividendYield { get; set; }
      public double Payout { get; set; }
      public double PriceEarningRatio { get; set; }
      public double DividendPerShare { get; set; }
      public double EarningsPerShare { get; set; }
      public double EpsEstimateCurrentYear { get; set; }
      public double PriceToBook { get; set; }
      public DateTime? ExDividendDate { get; set; }
      public double BookToValuePerShare { get; set; }
      public double GrahamPrice { get; set; }
      public double GrahamSpread { get; set; }
      public double PtfPercent { get; set; }
      public double Amount { get; set; }
      public int NumberOfSharesTarget { get; set; }
      public double AmountTarget { get; set; }
   }
}