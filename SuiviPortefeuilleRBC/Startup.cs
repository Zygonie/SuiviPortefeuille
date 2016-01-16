using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SuiviPortefeuilleRBC.Startup))]
namespace SuiviPortefeuilleRBC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
