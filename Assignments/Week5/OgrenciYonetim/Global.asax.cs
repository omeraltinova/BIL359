using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OgrenciYonetim
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            // Hata loglama burada yapılabilir
        }
    }
}

