using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
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

      #region Store Connections http://www.asp.net/signalr/overview/guide-to-the-api/mapping-users-to-connections

      public override Task OnConnected()
      {
         var name = Context.User.Identity.Name;
         using(var db = new ApplicationDbContext())
         {
            var user = db.SignalRUsers
                .Include(u => u.Connections)
                .SingleOrDefault(u => u.UserName == name);

            if(user == null)
            {
               user = new SignalRUser
               {
                  UserName = name,
                  Connections = new List<SignalRConnection>()
               };
               db.SignalRUsers.Add(user);
            }

            user.Connections.Add(new SignalRConnection
            {
               SignalRConnectionId = Context.ConnectionId,
               UserAgent = Context.Request.Headers["User-Agent"],
               Connected = true,
               PortfolioId = -1
            });
            db.SaveChanges();
         }
         return base.OnConnected();
      }

      public override Task OnDisconnected(bool stopCalled)
      {
         using(var db = new ApplicationDbContext())
         {
            var connection = db.SignalRConnections.Find(Context.ConnectionId);
            connection.Connected = false;
            db.SaveChanges();
         }
         return base.OnDisconnected(stopCalled);
      }

      #endregion

      #region Client calls

      public IEnumerable<Stock> GetStocksFromServer(int portfolioId)
      {
         return stockTicker.GetAllStocks(portfolioId);
      }

      public void setCurrentPortfolioId(int portfolioId)
      {
         var name = Context.User.Identity.Name;
         using(var db = new ApplicationDbContext())
         {
            var connection = db.SignalRConnections.Find(Context.ConnectionId);
            connection.PortfolioId = portfolioId;
            db.SignalRConnections.Attach(connection);
            db.Entry(connection).State = EntityState.Modified;
            db.SaveChanges();
         }
      }

      #endregion
   }
}