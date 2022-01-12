using ECMSS.Web;
using Swashbuckle.Application;
using System.Web.Http;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ECMSS.Web
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var assembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration.EnableSwagger(w => w.SingleApiVersion("v1", "ECM Api")).EnableSwaggerUi();
        }
    }
}