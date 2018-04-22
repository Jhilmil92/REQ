using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RequestEnhancementQueue.Startup))]
namespace RequestEnhancementQueue
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
