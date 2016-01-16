using SuiviPortefeuilleRBC.Models;
using System.Collections.Generic;

namespace SuiviPortefeuilleRBC.Migrations
{
   using System;
   using System.Data.Entity;
   using System.Data.Entity.Migrations;
   using System.Linq;
   using Microsoft.AspNet.Identity;
   using Microsoft.AspNet.Identity.EntityFramework;

   internal sealed class Configuration : DbMigrationsConfiguration<SuiviPortefeuilleRBC.Models.ApplicationDbContext>
   {
      public Configuration()
      {
         AutomaticMigrationsEnabled = false;
      }

      protected override void Seed(SuiviPortefeuilleRBC.Models.ApplicationDbContext context)
      {
         //if(System.Diagnostics.Debugger.IsAttached == false)
         //   System.Diagnostics.Debugger.Launch();

         try
         {
            context.Portfolios.AddOrUpdate(p => p.Name,
               new Portfolio
               {
                  Name = "MargeRBC",
                  TargetValue = 50000
               });

            List<StockDescription> descriptions = new List<StockDescription>
            {
               new StockDescription { Code = "Marge", Name = "Marge", ExDividendDate = DateTime.Now},
               new StockDescription { Code = "EMA.TO", Name = "EMERA INCORPORATED", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "RY.TO", Name = "ROYAL BANK OF CANADA", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "POT.TO", Name = "POTASH CORP OF SASK INC", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "BMO.TO", Name = "BANK OF MONTREAL", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "TD.TO", Name = "TORONTO-DOMINION BANK", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "BNS.TO", Name = "BANK OF NOVA SCOTIA", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "AGF-B.TO", Name = "AGF MANAGEMENT LTD. CL.B NV", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "TCL-A.TO", Name = "TRANSCONTINENTAL INC. CL A SV", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "ACD.TO", Name = "ACCORD FINANCIAL", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "FTS.TO", Name = "FORTIS INC", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "MIC.TO", Name = "GENWORTH MI CANADA INC.", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "CTY.TO", Name = "CALIAN TECHNOLOGIES LTD.", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "CU.TO", Name = "CANADIAN UTILITIES LTD. CL.A", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "TRI.TO", Name = "THOMSON REUTERS CORPORATION", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "FTT.TO", Name = "FINNING INTL", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "RCI-B.TO", Name = "ROGERS COMMUNICATIONS INC. CL.", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "BCE.TO", Name = "BCE INC.", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "CPX.TO", Name = "CAPITAL POWER CORPORATION", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "PWF.TO", Name = "POWER FINANCIAL CORP.", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "LB.TO", Name = "LAURENTIAN BANK", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "CM.TO", Name = "CANADIAN IMPERIAL BANK OF COMME", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "POW.TO", Name = "POWER CORPORATION OF CANADA SV", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "SXP.TO", Name = "SUPREMEX INC.", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "TRP.TO", Name = "TRANSCANADA CORP.", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "ESI.TO", Name = "ENSIGN ENERGY SERVICES INC.", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "SDY.TO", Name = "STRAD ENERGY SERVICES LTD", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "NA.TO", Name = "NATIONAL BANK OF CANADA", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "CPG.TO", Name = "CRESCENT POINT ENERGY CORP.", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "FC.TO", Name = "FIRM CAPITAL MORTGAGE INV. CORP", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "AI.TO", Name = "ATRIUM MORTGAGE INVESTMENT CORP", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "AFN.TO", Name = "AG GROWTH INTERNATIONAL INC.", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "ENF.TO", Name = "ENBRIDGE INCOME FUND HOLDINGS I", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "BDT.TO", Name = "BIRD CONSTR INC", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "NPI.TO", Name = "NORTHLAND POWER INC.", ExDividendDate = DateTime.Now },
               new StockDescription { Code = "CJR-B.TO", Name = "CORUS ENTERTAINMENT INC. CL.B", ExDividendDate = DateTime.Now }
            };

            descriptions.ForEach(s => context.StockDescriptions.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var stockRY = context.StockDescriptions.First(p => p.Code == "RY.TO");
            var stockNA = context.StockDescriptions.First(p => p.Code == "NA.TO");

            context.Stocks.AddOrUpdate(p => p.StockId,
               new Stock { StockDescriptionId = stockRY.StockDescriptionId, NumberOfShares = 10, UnitaryPrice = 11, InvestedValue = 119.95 },
               new Stock { StockDescriptionId = stockNA.StockDescriptionId, NumberOfShares = 20, UnitaryPrice = 12, InvestedValue = 249.95 });
         }
         catch(System.Data.Entity.Validation.DbEntityValidationException ex)
         {
            var sb = new System.Text.StringBuilder();

            foreach(var failure in ex.EntityValidationErrors)
            {
               sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
               foreach(var error in failure.ValidationErrors)
               {
                  sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                  sb.AppendLine();
               }
            }

            throw new System.Data.Entity.Validation.DbEntityValidationException(
               "Entity Validation Failed - errors follow:\n" +
               sb.ToString(), ex
               ); // Add the original exception as the innerException
         }
      }

   }
}
