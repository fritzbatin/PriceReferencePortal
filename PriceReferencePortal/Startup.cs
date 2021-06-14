using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PriceReferencePortal.Startup))]
namespace PriceReferencePortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
