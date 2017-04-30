using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
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
                        configurationExpression.MapFrom(topic => (topic.UpVotes - topic.DownVotes)));
                expression.CreateMap<Category, CategoryNamesViewModel>();
                expression.CreateMap<Category, CategoryNavbarViewModel>();
                expression.CreateMap<Topic, TopicEditViewModel>();
                expression.CreateMap<Reply, ReplyEditViewModel>();
                expression.CreateMap<ForumComment, ForumCommentEditViewModel>();
                expression.CreateMap<Topic, ManageIndexTopicViewModel>();
                expression.CreateMap<Topic, ManageIndexTopicViewModel>();
                expression.CreateMap<Reply, ManageIndexReplyViewModel>();
                expression.CreateMap<ForumComment, ManageIndexForumCommentViewModel>().ForMember(vm => vm.Topic,
                    configurationExpression => configurationExpression.MapFrom(forumcomment => forumcomment.Topic));
            });
        }
    }
}
