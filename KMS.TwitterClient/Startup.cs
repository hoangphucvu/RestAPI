using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KMS.TwitterClient.Startup))]
namespace KMS.TwitterClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
