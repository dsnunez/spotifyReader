using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(spotifyAcid.Startup))]
namespace spotifyAcid
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
