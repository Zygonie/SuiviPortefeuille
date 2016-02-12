using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuiviPortefeuilleRBC.Models
{
   public class SignalRUser
   {
      [Key]
      public string UserName { get; set; }
      public ICollection<SignalRConnection> Connections { get; set; }      
   }
}