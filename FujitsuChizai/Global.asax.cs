using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FujitsuChizai
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // データ投入したいときはコメントアウト
            // Database.SetInitializer<Models.Entities.ModelContext>(new Migrations.LightMapInitializer());

            // JSONをLowerCamelCaseで返す
            var config = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // JSONのみを使用
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}
