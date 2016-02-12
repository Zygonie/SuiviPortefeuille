using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SuiviPortefeuilleRBC.Models
{
   public interface IApplicationDbContext
   {
      System.Data.Entity.DbSet<SuiviPortefeuilleRBC.Models.Operation> Operations { get; set; }
      System.Data.Entity.DbSet<SuiviPortefeuilleRBC.Models.Portfolio> Portfolios { get; set; }
      System.Data.Entity.DbSet<SuiviPortefeuilleRBC.Models.Stock> Stocks { get; set; }
      System.Data.Entity.DbSet<SuiviPortefeuilleRBC.Models.StockDescription> StockDescriptions { get; set; }
      System.Data.Entity.DbSet<SuiviPortefeuilleRBC.Models.SignalRConnection> SignalRConnections { get; set; }
      System.Data.Entity.DbSet<SuiviPortefeuilleRBC.Models.SignalRUser> SignalRUsers { get; set; }
      int SaveChanges();
   }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public ApplicationDbContext(string connectionString)
           : base(connectionString, throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public static ApplicationDbContext Create(string connectionString)
        {
           return new ApplicationDbContext(connectionString);
        }

        public virtual System.Data.Entity.DbSet<SuiviPortefeuilleRBC.Models.Operation> Operations { get; set; }
        public virtual System.Data.Entity.DbSet<SuiviPortefeuilleRBC.Models.Portfolio> Portfolios { get; set; }
        public virtual System.Data.Entity.DbSet<SuiviPortefeuilleRBC.Models.Stock> Stocks { get; set; }
        public virtual System.Data.Entity.DbSet<SuiviPortefeuilleRBC.Models.StockDescription> StockDescriptions { get; set; }
        public virtual System.Data.Entity.DbSet<SuiviPortefeuilleRBC.Models.SignalRConnection> SignalRConnections { get; set; }
        public virtual System.Data.Entity.DbSet<SuiviPortefeuilleRBC.Models.SignalRUser> SignalRUsers { get; set; }
    }
}