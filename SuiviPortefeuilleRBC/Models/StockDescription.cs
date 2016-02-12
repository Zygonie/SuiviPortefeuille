using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuiviPortefeuilleRBC.Models
{
   public class StockDescription
   {
      [Key, Column(Order = 1)]
      public string Code { get; set; }

      [Required]
      public string Name { get; set; }
      
      [DisplayFormat(DataFormatString = "{0:F2}")]
      [DisplayName("Price")]
      public double LastPrice { get; set; }

      [DisplayFormat(DataFormatString = "{0:P2}")]
      [DisplayName("Variation")]
      public double ChangePercent { get; set; }

      public double SpreadLowTargetPercent { get; set; }

      public double SpreadHighTargetPercent { get; set; }

      [DisplayFormat(DataFormatString = "{0:F2}")]
      public double PriceTargetMin { get; set; }

      [DisplayFormat(DataFormatString = "{0:F2}")]
      public double PriceTargetMax { get; set; }

      [DisplayFormat(DataFormatString = "{0:P2}")]
      [DisplayName("Dividend Yield")]
      public double DividendYield { get; set; }

      public double Payout { get; set; }

      [DisplayName("PER")]
      public double PriceEarningRatio { get; set; }

      [DisplayName("Dividend per Share")]
      public double DividendPerShare { get; set; }

      [DisplayName("EPS")]
      public double EarningsPerShare { get; set; }

      [DisplayName("EPS current year")]
      public double EpsEstimateCurrentYear { get; set; }

      [DisplayName("Price to Book")]
      public double PriceToBook { get; set; }

      [DisplayName("Ex-Dividend Date")]
      public DateTime? ExDividendDate { get; set; }

      [DisplayName("Book to Value")]
      public double BookToValuePerShare { get; set; }

      [DisplayName("Graham Fair Price")]
      public double GrahamPrice { get; set; }

      [DisplayName("Distance from Graham")]
      public double GrahamSpread { get; set; }

      [DisplayFormat(DataFormatString = "{0:P2}")]
      [DisplayName("Percent. from Portfolio")]
      public double PtfPercent { get; set; }

      [DisplayFormat(DataFormatString = "{0:F2}")]
      [DisplayName("Cash to invest (target)")]
      public double Amount { get; set; }

      [DisplayName("#Shares to buy")]
      public int NumberOfSharesTarget { get; set; }

      [DisplayFormat(DataFormatString = "{0:F2}")]
      [DisplayName("Cash to invest (adjusted)")]
      public double AmountTarget { get; set; }

      public void FillInfos(DetailedQuoteQueryResultModel infos)
      {
         LastPrice = double.Parse(infos.LastTradePriceOnly);
         ChangePercent = double.Parse(infos.ChangeinPercent.Replace("%", "")) / 100; //On affiche en pourcentage (format P2)
         DividendYield = double.Parse(infos.DividendYield) / 100;         
         PriceEarningRatio = double.Parse(infos.PERatio);
         DividendPerShare = double.Parse(infos.DividendShare);
         EarningsPerShare = double.Parse(infos.EarningsShare);
         EpsEstimateCurrentYear = double.Parse(infos.EPSEstimateCurrentYear);
         PriceToBook = double.Parse(infos.PriceBook);
         ExDividendDate = DateTime.Parse(infos.ExDividendDate);
         BookToValuePerShare = double.Parse(infos.BookValue);
         Payout = DividendPerShare / EarningsPerShare;
         GrahamPrice = Math.Sqrt(22.5 * EarningsPerShare * BookToValuePerShare);
         GrahamSpread = (GrahamPrice - LastPrice) / GrahamPrice;
      }

      public void FillInfos(SimpleQuoteQueryResultModel infos)
      {
         LastPrice = double.Parse(infos.LastTradePriceOnly);
         ChangePercent = double.NaN;
         GrahamSpread = (GrahamPrice - LastPrice) / GrahamPrice;
      }

      public void UpdateStockDescription()
      {
         using(Controllers.DetailedInfosStocksController controller = new Controllers.DetailedInfosStocksController())
         {
            Models.DetailedQuoteQueryResultModel infos = controller.RetrieveStockDetailedInfos(this.Code);
            FillInfos(infos);
         }
      }
   }
}