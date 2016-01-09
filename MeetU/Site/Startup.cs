using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SIte.Startup))]
namespace SIte
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
