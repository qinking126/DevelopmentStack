using System.Web.Mvc;
using System.Web.Routing;
using DevelopmentStack.Domain.Entities;
using DevelopmentStack.Init;
using DevelopmentStack.Web.Binders;
using SharpLite.NHibernateProvider;
using SharpLite.Web.Mvc.ModelBinder;

namespace DevelopmentStack.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            DependencyResolverInitializer.Initialize();

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();
            //ModelBinders.Binders.Add(typeof(User), new UserModelBinder(new Repository<User>()));
            //ModelBinders.Binders.Add(typeof(Money), new MoneyBinder());
        }
    }
}