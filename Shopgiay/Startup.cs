using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shopgiay.Startup))]
namespace Shopgiay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
