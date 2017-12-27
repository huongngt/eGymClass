using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eGymClass.Startup))]
namespace eGymClass
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
