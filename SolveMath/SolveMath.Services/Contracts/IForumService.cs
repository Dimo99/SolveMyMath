﻿using System.Collections.Generic;
using SolveMath.Models.BindingModels;
using SolveMath.Models.ViewModels;

namespace SolveMath.Services.Contracts
{
    public interface IForumService
    {
        IEnumerable<TopicHeaderViewModel> GetTopicsForPage(int? page);
        IEnumerable<CategoryNavbarViewModel> GetCategories();
        IEnumerable<CategoryNamesViewModel> GetCategoryNames();
        void CreateTopic(TopicBindingModel tbm);
        TopicViewModel GetTopic(int id);
        void CreateAnswer(AnswerBindingModel abm,string userId);
        void CreateComment(AddForumCommentBindingModel abm, string userId);
    }
}