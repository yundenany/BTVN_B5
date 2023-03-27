using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BTVN_B5_5.Startup))]
namespace BTVN_B5_5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
