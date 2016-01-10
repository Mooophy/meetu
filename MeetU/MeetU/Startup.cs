using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MeetU.Startup))]
namespace MeetU
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
