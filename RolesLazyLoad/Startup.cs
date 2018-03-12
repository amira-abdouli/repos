using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RolesLazyLoad.Startup))]
namespace RolesLazyLoad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
