using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PocketScribe.Startup))]
namespace PocketScribe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
