using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(ENTP_Project.Hubs.Startup))]
namespace ENTP_Project.Hubs
{
    public class Startup
    {
        public void Configure(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
