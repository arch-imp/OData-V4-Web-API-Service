using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebAPIODataV4.Startup))]

namespace WebAPIODataV4
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}