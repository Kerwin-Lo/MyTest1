using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyTest1.Startup))]
namespace MyTest1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
