using Microsoft.Owin;
using Owin;
using SolveMath;

[assembly: OwinStartup(typeof(Startup))]
namespace SolveMath
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
