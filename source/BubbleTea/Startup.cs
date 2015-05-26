using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BubbleTea.Startup))]
namespace BubbleTea
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
