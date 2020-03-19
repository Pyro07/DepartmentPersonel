using DepartmentPersonel.Business;
using System.Web.Mvc;
using System.Web.Routing;


namespace DepartmentPersonel.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinessModule()));

            UnityConfig.RegisterComponents();
        }
    }
}
