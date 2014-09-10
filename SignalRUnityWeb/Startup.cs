using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRUnityWeb.Startup))]

namespace SignalRUnityWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
