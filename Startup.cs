using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Peoples.Startup))]
namespace Peoples
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
