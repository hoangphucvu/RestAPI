using log4net;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace KMS.TwitterClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MvcApplication).Name);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Server.ClearError();
            Application["ErrorInfo"] = exception.Message;
            Log.Error(string.Format("Error {0} \r\n {1} \r\n", exception.Message, exception.StackTrace));
            Response.Redirect("/Home/Error/");
        }
    }
}