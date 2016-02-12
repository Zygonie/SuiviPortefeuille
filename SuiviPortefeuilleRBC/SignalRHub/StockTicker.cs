using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SuiviPortefeuilleRBC.Repository;
using SuiviPortefeuilleRBC.BusinessServices;
using SuiviPortefeuilleRBC.Models;

namespace SuiviPortefeuilleRBC.SignalRHub
{
   public class StockTicker
   {
      // Singleton instance
      private readonly static Lazy<StockTicker> instance = new Lazy<StockTicker>(() => new StockTicker(GlobalHost.ConnectionManager.GetHubContext<StockHub>().Clients));
      private readonly object updateStockPricesLock = new object();      
      private readonly TimeSpan updateInterval = TimeSpan.FromMilliseconds(250);
      private volatile bool updatingStockPrices = false;
      private readonly Timer timer;
      private IStockServices stockServices;
      private IStockDescriptionServices stockDescriptionServices;

      private StockTicker(IHubConnectionContext<dynamic> clients)
      {
         Clients = clients;
         var unitWork = new UnitOfWork();
         stockServices = new StockServices(unitWork);
         stockDescriptionServices = new StockDescriptionServices(unitWork);

         timer = new Timer(UpdateStockPrices, null, updateInterval, updateInterval);

      }

      public static StockTicker Instance
      {
         get
         {
            return instance.Value;
         }
      }

      private IHubConnectionContext<dynamic> Clients { get; set; }

      public IEnumerable<Stock> GetAllStocks(int portfolioId)
      {
         return stockServices.GetStockByPortfolioId(portfolioId);
      }

      private void UpdateStockPrices(object state)
      {
         lock(updateStockPricesLock)
         {
            if(!updatingStockPrices)
            {
               updatingStockPrices = true;

               using(Controllers.DetailedInfosStocksController controller = new Controllers.DetailedInfosStocksController())
               {
                  IEnumerable<Models.DetailedQuoteQueryResultModel> infos = controller.RetrieveStockDetailedInfos();
                  List<string> codes = infos.Select(p => p.Symbol).ToList();
                  foreach(StockDescription description in stockDescriptionServices.GetMany(p => codes.Contains(p.Code)))
                  {
                     var info = infos.Where(p => p.Symbol == description.Code).FirstOrDefault<Models.DetailedQuoteQueryResultModel>();
                     description.FillInfos(info);
                     stockDescriptionServices.UpdateStockDescription(description);
                     BroadcastStockPrice(description);
                  }
               }

               updatingStockPrices = false;
            }
         }
      }
      
      private void BroadcastStockPrice(StockDescription description)
      {
         Clients.All.updateStockPrice(description);
      }
   }
}