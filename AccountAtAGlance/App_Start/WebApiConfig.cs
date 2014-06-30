using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using AccountAtAGlance.Controllers;
using AccountAtAGlance.IoC;
using AccountAtAGlance.Repository;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AccountAtAGlance
{
    public class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var mappingContainer = IoCMappingContainer.GetInstance();
            config.DependencyResolver = new IoCScopeContainer(mappingContainer);
        }
    }
}