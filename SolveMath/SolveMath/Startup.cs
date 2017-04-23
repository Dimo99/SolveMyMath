using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SolveMath.Startup))]
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
