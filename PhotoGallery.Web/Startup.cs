using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoGallery.Web.Startup))]
namespace PhotoGallery.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
