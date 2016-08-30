using System.Web.Http;
using WebActivatorEx;
using GPA.API;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace GPA.API
{
	public class SwaggerConfig
	{
		public static void Register()
		{
			var thisAssembly = typeof(SwaggerConfig).Assembly;

			GlobalConfiguration.Configuration
                .EnableSwagger(c => { c.SingleApiVersion("v1", "GPA.API");
                c.IncludeXmlComments(string.Format(@"{0}\bin\GPA.API.XML",           
                           System.AppDomain.CurrentDomain.BaseDirectory)); })
				.EnableSwaggerUi(c => { });
		}
	}
}
