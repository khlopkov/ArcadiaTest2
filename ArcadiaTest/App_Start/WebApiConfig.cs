using ArcadiaTest.BusinessLayer;
using ArcadiaTest.DataLayer;
using ArcadiaTest.Middlewares;
using ArcadiaTest.Infrastructure;
using ArcadiaTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;

namespace ArcadiaTest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
        public static IUnityContainer Container()
        {
            var container = new UnityContainer();
            container.RegisterType<ArcadiaTestEntities>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ITasksRepository, TaskRepository>();
            container.RegisterType<ITaskChangesRepository, TaskChangesRepository>();

            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ITaskService, TaskService>();
            container.RegisterType<IAuthService, AuthService>();
            container.RegisterType<ITaskHistoryService, TaskHistoryService>();

            return container;
        }
    }
}
