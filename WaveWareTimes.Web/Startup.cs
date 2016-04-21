using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WaveWareTimes.Web.Startup))]

namespace WaveWareTimes.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureWebApi(app);
        }
    }
}
