using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using SolveMath.Models.BindingModels;
using SolveMath.Models.Entities;
using SolveMath.Models.ViewModels;

namespace SolveMath
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureAutoMapper();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Topic, TopicHeaderViewModel>()
                    .ForMember(vm => vm.Score, configurationExpression =>
                        configurationExpression.MapFrom(topic => topic.UpVotes - topic.DownVotes));
                expression.CreateMap<RegisterBindingModel, ApplicationUser>();
                expression.CreateMap<Category, CategoriesViewModel>();
            });
        }
    }
}
