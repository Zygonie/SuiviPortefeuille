using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SuiviPortefeuilleRBC.Repository;
using SuiviPortefeuilleRBC.Models;

namespace SuiviPortefeuilleRBC.SignalRHub
{
   [HubName("stockHub")]
   public class StockHub : Hub
   {
      #region Fields

      private readonly StockTicker stockTicker;

      #endregion

      #region

      public StockHub() : this(StockTicker.Instance) { }

      public StockHub(StockTicker stockTicker)
      {
         this.stockTicker = stockTicker;
      }

      #endregion

      public IEnumerable<Stock> GetStocksFromServer(int portfolioId)
      {
         return stockTicker.GetAllStocks(portfolioId);
      }
   }
}