using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMath.Models.BindingModels
{
    public class EditTopicBindingModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string TagNames { get; set; }
        public string[] OldTagNames { get; set; }
    }
}
