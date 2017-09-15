using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentInfo.Web.Startup))]
namespace StudentInfo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
