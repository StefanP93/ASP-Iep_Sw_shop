using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IEPProjekatPS120511.Startup))]
namespace IEPProjekatPS120511
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
