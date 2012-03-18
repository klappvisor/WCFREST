using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Practices.Unity;
using DAL;

namespace AspNetWebApiHost {
    class Program {
        static void Main(string[] args) {
            var config = new HttpSelfHostConfiguration("http://localhost:8111/");
            config.Routes.MapHttpRoute(
                name:"DefaultAPI",
                routeTemplate: "api/{controller}/{nodeId}/{action}",
                defaults: new { 
                    controller = "Tree",
                    nodeId = RouteParameter.Optional, 
                    action = "Index" }
            );

            RegisterDependencies(config);

            using (HttpSelfHostServer server = new HttpSelfHostServer(config)) {
                server.OpenAsync().Wait();

                Console.ReadLine();
            }
        }

        private static void RegisterDependencies(HttpSelfHostConfiguration config) {
            IUnityContainer container = new UnityContainer();
            container.RegisterInstance<IStorage>(new Storage());

            config.ServiceResolver.SetResolver(x => {
                try { return container.Resolve(x); }
                catch (ResolutionFailedException) { return null; }
            }, x => {
                try { return container.ResolveAll(x); }
                catch (ResolutionFailedException) { return new List<object>(); }
            });

        }
    }
}
