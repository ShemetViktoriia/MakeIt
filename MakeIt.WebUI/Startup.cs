using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;

[assembly: OwinStartup(typeof(MakeIt.WebUI.Startup))]
namespace MakeIt.WebUI
{
    public partial class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            DataProtectionProvider = app.GetDataProtectionProvider();
            ConfigureApp(app);
        }
    }
}