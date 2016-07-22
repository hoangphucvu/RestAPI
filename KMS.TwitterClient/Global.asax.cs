using KMS.TwitterClient.API;
using log4net;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace KMS.TwitterClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MvcApplication).Name);

        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<ITwitterServices, TwitterAPI>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        protected void Application_Start()
        {
            RegisterComponents();
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
            Response.Redirect("/Error/ShowError/");
        }
    }
}