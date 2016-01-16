using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviPortefeuilleRBC.Models
{
   public class StockOperationApiModel
   {
      public string Name { get; set; }
      public string Code { get; set; }
      public OperationOnStock OperationType { get; set; }
      public double UnitaryPrice { get; set; }
      public double Fees { get; set; }
      public DateTime Date { get; set; }
   }
}