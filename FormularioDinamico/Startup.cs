using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FormularioDinamico.Startup))]
namespace FormularioDinamico
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
