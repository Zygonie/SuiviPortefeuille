using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviPortefeuilleRBC.Models
{
   public class SignalRConnection
   {
      public string SignalRConnectionId { get; set; }
      public string UserAgent { get; set; }
      public bool Connected { get; set; }
      public int PortfolioId { get; set; }
   }
}