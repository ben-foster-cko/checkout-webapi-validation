using APIValidationDemo.Validation;
using FluentValidation;
using Newtonsoft.Json.Serialization;
using Owin;
using StructureMap;
using System.Web.Http;

namespace APIValidationDemo
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = new Container(c =>
            {
                c.For<ILogger>().Use<Logger>();

                c.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                });
            });

            var validatorFactory = new StructureMapValidatorFactory(container);

            var config = new HttpConfiguration();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver 
                = new CamelCasePropertyNamesContractResolver();

            config.Filters.Add(new InputValidationFilter(validatorFactory));
            config.Filters.Add(new ValidateModelStateFilter());
            
            // Bind all commands decorated with IFromUriCommand from the Uri.
            //config.ParameterBindingRules.Insert(0,
            //    desc => typeof(IFromUriCommand).IsAssignableFrom(desc.ParameterType)
            //                ? new FromUriAttribute().GetBinding(desc)
            //                : null);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}
