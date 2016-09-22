using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoreBooksMVC.Startup))]
namespace StoreBooksMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
