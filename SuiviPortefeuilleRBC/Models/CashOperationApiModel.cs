using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviPortefeuilleRBC.Models
{
   public class CashOperationApiModel
   {
      public OperationOnCash OperationType { get; set; }
      public DateTime Date { get; set; }
   }
}