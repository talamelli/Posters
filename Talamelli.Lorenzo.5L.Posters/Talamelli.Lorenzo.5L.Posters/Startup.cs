using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Talamelli.Lorenzo._5L.Posters.Startup))]
namespace Talamelli.Lorenzo._5L.Posters
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
