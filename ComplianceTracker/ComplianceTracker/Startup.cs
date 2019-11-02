using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ComplianceTracker.Startup))]
namespace ComplianceTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
