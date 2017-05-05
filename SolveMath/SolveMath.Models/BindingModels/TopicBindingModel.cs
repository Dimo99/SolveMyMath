using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;

namespace SolveMath.Models.BindingModels
{
    public class TopicBindingModel
    {
        [AllowHtml]
        [Required(ErrorMessage = "Заглавието на темата е задължително.")]
        [StringLength(100,MinimumLength = 10,ErrorMessage = "Заглавието трябва да съдържа поне 10 букви и да не надвишава 100.")]
        public string Title { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Съдържанието на темата е зъдалжителнo.")]
        [StringLength(400,MinimumLength = 10,ErrorMessage = "Съдържанието трябва да бъде поне 10 букви и да не надвишава 400.")]
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorId { get; set; }
        [AllowHtml]
        public  string CategoryName { get; set; }
        public string TagsNames { get; set; }
    }
}
