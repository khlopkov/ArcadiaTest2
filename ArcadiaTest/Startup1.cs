using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using ArcadiaTest.Middlewares;
using System.Web.Http;
using System.Web.Routing;
using ArcadiaTest.Infrastructure;
using System.Web.Http.Cors;
using ArcadiaTest.BusinessLayer;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(ArcadiaTest.Startup))]

namespace ArcadiaTest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            //AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            var container = WebApiConfig.Container();
            config.DependencyResolver = new DependencyResolver(container);
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            app.UseCors(CorsOptions.AllowAll);
            app.Use<BasicAuthenticationMiddleware>(config.DependencyResolver.GetService(typeof(IAuthService)) as IAuthService);
            app.UseWebApi(config);
            
        }
    }
}
