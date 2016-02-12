using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
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
      private IPortfolioServices portfolioServices;
      private IStockDescriptionServices stockDescriptionServices;
      private readonly ApplicationDbContext db;
      private Random random;

      private StockTicker(IHubConnectionContext<dynamic> clients)
      {
         Clients = clients;
         var unitWork = new UnitOfWork();
         stockServices = new StockServices(unitWork);
         stockDescriptionServices = new StockDescriptionServices(unitWork);
         portfolioServices = new PortfolioServices(unitWork);
         db = new ApplicationDbContext();

         timer = new Timer(UpdateStockPrices, null, updateInterval, updateInterval);

         //random = new Random(0);
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
                     
                     //To test updates and styles at frontend
                     //description.ChangePercent = random.NextDouble();
                     //description.LastPrice *= random.NextDouble() + 0.5;
                     
                     BroadcastStockPrice(description);
                  }
               }

               updatingStockPrices = false;
            }
         }
      }
      
      private void BroadcastStockPrice(StockDescription description)
      {
         //Broadcast the description only to users that actually need it
         var portfolioIds = stockServices.GetStockPortfolioIdsHavingStock(description.Code);

         ////Broadcast to every interected user
         //List<string> usernames = db.SignalRUsers.Where(u => portfolioIds.Contains(u.PortfolioId)).Select(u => u.UserName).ToList();         
         //Clients.Users(usernames).updateStockPrice(description);

         ////Autre methode: on broadcast sur toutes les connexions de tous les usagers qui ont besoin d'etre mis a jour
         //foreach(var user in db.SignalRUsers.Where(u => portfolioIds.Contains(u.PortfolioId)))         
         //{
         //   //Explicit loading see https://msdn.microsoft.com/en-us/data/jj574232.aspx
         //   //Load() is not an extension method on Queryable, so it doesn't come together with all the usual LINQ methods. 
         //   //If you are using Entity Framework, you need to import the corresponding namespace: System.Data.Entity
         //   db.Entry(user).Collection(u => u.Connections).Query().Where(c => c.Connected == true).Load(); //need using System.Data.Entity; 
         //   if(user.Connections !=null)
         //   {
         //      foreach(var connection in user.Connections)
         //      {
         //         Clients.Client(connection.SignalRConnectionId).updateStockPrice(description);
         //      }
         //   }
         //}

         foreach(var connection in db.SignalRConnections.Where(c=>portfolioIds.Contains(c.PortfolioId)))
         {
            Clients.Client(connection.SignalRConnectionId).updateStockPrice(description);
         }

      }
   }
}