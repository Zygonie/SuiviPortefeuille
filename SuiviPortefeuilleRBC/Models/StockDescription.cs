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

      [Required]
      public DateTime? LastTimeUpdated { get; set; }

      [NotMapped]
      public bool HasChanged { get; set; }

      public void FillInfos(DetailedQuoteQueryResultModel infos)
      {
         double result;
         //LastPrice
         double oldPrice = this.LastPrice;
         if(double.TryParse(infos.LastTradePriceOnly, out result)) //On affiche en pourcentage (format P2)
         {
            LastPrice = result;
            HasChanged = Math.Abs(oldPrice - LastPrice) < 1 - 6;
         }
         else
         {
            LastPrice = 0.0;
         }
         //ChangePercent
         if(double.TryParse(infos.ChangeinPercent.Replace("%", ""), out result)) //On affiche en pourcentage (format P2)
         {
            ChangePercent = result / 100;
         }
         else
         {
            ChangePercent = 0.0;
         }

         if((DateTime.Now - (DateTime)LastTimeUpdated).TotalDays > 5)
         {
            HasChanged = true;
            LastTimeUpdated = DateTime.Now;

            //DividendYield
            if(double.TryParse(infos.DividendYield, out result))
            {
               DividendYield = result / 100;
            }
            else
            {
               DividendYield = 0.0;
            }
            //PriceEarningRatio
            if(double.TryParse(infos.PERatio, out result))
            {
               PriceEarningRatio = result;
            }
            else
            {
               PriceEarningRatio = 0.0;
            }
            //DividendPerShare
            if(double.TryParse(infos.DividendShare, out result))
            {
               DividendPerShare = result;
            }
            else
            {
               DividendPerShare = 0.0;
            }
            //EarningsPerShare
            if(double.TryParse(infos.EarningsShare, out result))
            {
               EarningsPerShare = result;
            }
            else
            {
               EarningsPerShare = 0.0;
            }
            //EpsEstimateCurrentYear
            if(double.TryParse(infos.EPSEstimateCurrentYear, out result))
            {
               EpsEstimateCurrentYear = result;
            }
            else
            {
               EpsEstimateCurrentYear = 0.0;
            }
            //PriceToBook
            if(double.TryParse(infos.PriceBook, out result))
            {
               PriceToBook = result;
            }
            else
            {
               PriceToBook = 0.0;
            }
            //ExDividendDate
            DateTime resultDate;
            if(DateTime.TryParse(infos.ExDividendDate, out resultDate))
            {
               ExDividendDate = resultDate;
            }
            else
            {
               ExDividendDate = DateTime.MinValue;
            }
            //BookToValuePerShare
            if(double.TryParse(infos.BookValue, out result))
            {
               BookToValuePerShare = result;
            }
            else
            {
               BookToValuePerShare = 0.0;
            }

            Payout = DividendPerShare / EarningsPerShare;
            GrahamPrice = Math.Sqrt(22.5 * EarningsPerShare * BookToValuePerShare);
            GrahamSpread = (GrahamPrice - LastPrice) / GrahamPrice;
         }
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