using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviPortefeuilleRBC.Models
{
   public class ManagePortfolioViewModel
   {
      public string Code { get; set; }
      public string Name { get; set; }
      public int NumberOfShare { get; set; }
      public double Pru { get; set; }
      public double DailyVariation { get; set; }
      public double PerformanceCash { get; set; }
      public double PerformancePercent { get; set; }
   }
}