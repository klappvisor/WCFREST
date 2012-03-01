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
            var config = new HttpSelfHostConfiguration("http://localhost:8081/");
            config.Routes.MapHttpRoute(
                name:"Default API",
                routeTemplate: "api/{controller}/{department}/{id}",
                defaults: new { department= RouteParameter.Optional, id = RouteParameter.Optional }
            );

            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();

            Console.ReadLine();
            server.Dispose();
        
        }
    }
}
