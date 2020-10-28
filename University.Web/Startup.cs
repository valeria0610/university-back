using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using University.BL.Data;

[assembly: OwinStartup(typeof(University.Web.Startup))]

namespace University.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {

            //Configura el DB context para que sea usado como una unica instancia por request

            app.CreatePerOwinContext(UniversityContext.Create);



            // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
