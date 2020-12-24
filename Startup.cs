using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineFoodOrderingSystem.Startup))]
namespace OnlineFoodOrderingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
