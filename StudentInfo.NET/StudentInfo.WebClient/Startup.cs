using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentInfo.WebClient.Startup))]
namespace StudentInfo.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}