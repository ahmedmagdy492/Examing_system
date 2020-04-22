using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exam_system.UI.Startup))]
namespace Exam_system.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
