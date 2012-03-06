using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Web.Http.Routing;

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

            using (HttpSelfHostServer server = new HttpSelfHostServer(config)) {
                server.OpenAsync().Wait();

                Console.ReadLine();
            }
        }
    }
}
