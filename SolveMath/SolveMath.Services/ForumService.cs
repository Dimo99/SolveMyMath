using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SolveMath.Models.Entities;
using SolveMath.Models.ViewModels;
using SolveMath.Services.Contracts;

namespace SolveMath.Services
{
    public class ForumService : Service,IForumService
    {
        private const int TopicsPerPage = 10;
        public IEnumerable<TopicHeaderViewModel> GetTopicsForPage(int? page)
        {
            if (page == null||page <= 1)
            {
                page = 0;
            }
            int pageNotNull = (int)page;
            IEnumerable<Topic> topics = Context.Topics;
            topics = topics.Skip(pageNotNull * TopicsPerPage).Take(TopicsPerPage);
            return Mapper.Map<IEnumerable<Topic>, IEnumerable<TopicHeaderViewModel>>(topics);
        }

        public IEnumerable<CategoriesViewModel> GetCategories()
        {
            return Mapper.Map<IEnumerable<CategoriesViewModel>>(Context.Categories);
        }
    }
}