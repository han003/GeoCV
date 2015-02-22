using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeoCV.Startup))]
namespace GeoCV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
