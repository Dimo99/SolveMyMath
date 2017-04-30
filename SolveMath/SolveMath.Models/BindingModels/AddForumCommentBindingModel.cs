using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMath.Models.BindingModels
{
    public class AddForumCommentBindingModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Content { get; set; }
    }
}
