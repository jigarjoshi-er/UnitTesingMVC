using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UnitTestDemo.Startup))]
namespace UnitTestDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
