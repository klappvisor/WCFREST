using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.UI.WebControls;

namespace AspNetWebApiHost {
    class Program {
        static void Main(string[] args) {
            var config = new HttpSelfHostConfiguration("http://localhost:8081/");
            config.Routes.MapHttpRoute(
                name:"Default API",
                routeTemplate: "{controller}/{id}/{action}",
                defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
            );

            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();

            Console.ReadLine();
            server.CloseAsync().Wait();
        
        }
    }
}
