using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SuiviPortefeuilleRBC.Models
{
   public class Stock
   {
      #region Properties

      [Key, Column(Order = 0)]
      [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
      public int StockId { get; set; }
            
      [Key, Column(Order = 1)]
      [ForeignKey("Description")]
      public string Code { get; set; }

      [Key, Column(Order = 2)]
      public int PortfolioId { get; set; }
      
      public virtual StockDescription Description { get; set; }

      [Required]
      [DisplayName("# Shares")]
      public int NumberOfShares { get; set; }

      [DisplayFormat(DataFormatString = "{0:F2}")]
      [DisplayName("Invested")]
      public double InvestedValue { get; set; }

      [Required]
      [DisplayFormat(DataFormatString = "{0:F2}")]
      public double UnitaryPrice { get; set; }

      [DisplayFormat(DataFormatString = "{0:F2}")]
      [DisplayName("Plus Value")]
      public double PerformanceCash { get; set; }

      [DisplayFormat(DataFormatString = "{0:P2}")]
      [DisplayName("Performance")]
      public double PerformancePercent { get; set; }

      #endregion

      #region Constructor

      public Stock()
      {
      }
      
      public Stock(int portfolioId, string code, int nbShares, double investedValue, StockDescription description)
      {
         Controllers.DetailedInfosStocksController controller = new Controllers.DetailedInfosStocksController();
         Models.DetailedQuoteQueryResultModel infos = controller.RetrieveStockDetailedInfos(code);
         PortfolioId = portfolioId;
         Code = infos.Symbol;
         NumberOfShares = nbShares;
         InvestedValue = investedValue;
         UnitaryPrice = InvestedValue / NumberOfShares;
         Description = description;
         FillInfos(infos);
      }

      public Stock(int portfolioId, string code, int nbShares, double investedValue)
         : this(portfolioId, code, nbShares, investedValue, null)
      {
      }
      
      public Stock(AddOperationViewModel viewModel)
         : this(viewModel, null)
      {
      }

      public Stock(AddOperationViewModel viewModel, StockDescription description)
         : this(viewModel.PortfolioId, viewModel.Code, viewModel.NumberOfShares, viewModel.NumberOfShares * viewModel.UnitaryPrice + viewModel.Fees, description)
      {         
      }      

      #endregion

      #region Methods

      public void FillInfos(DetailedQuoteQueryResultModel infos)
      {         
         if(this.Description == null)
         {
            this.Description = new StockDescription();
            this.Description.Code = infos.Symbol;
            this.Description.Name = infos.Name;
         }
         this.Description.FillInfos(infos);
         PerformanceCash = (this.Description.LastPrice - UnitaryPrice) * NumberOfShares;
         PerformancePercent = (this.Description.LastPrice - UnitaryPrice) / UnitaryPrice;
      }

      public void UpdateStock(AddOperationViewModel viewModel)
      {
         switch(viewModel.Sens)
         {
            case  OperationOnStock.Buy:
               double invested = viewModel.NumberOfShares * viewModel.UnitaryPrice + viewModel.Fees;
               this.NumberOfShares += viewModel.NumberOfShares;
               this.InvestedValue += invested;
               this.UnitaryPrice = this.InvestedValue / this.NumberOfShares;
               break;
            case OperationOnStock.Sell:
               this.NumberOfShares -= viewModel.NumberOfShares;
               this.InvestedValue -= viewModel.NumberOfShares * viewModel.UnitaryPrice;
               break;
            default:
               throw new Exception();
         }
         using(Controllers.DetailedInfosStocksController controller = new Controllers.DetailedInfosStocksController())
         {
            Models.DetailedQuoteQueryResultModel infos = controller.RetrieveStockDetailedInfos(this.Code);
            FillInfos(infos);
         }
      }

      #endregion
   }
}