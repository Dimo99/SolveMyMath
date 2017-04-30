using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMath.Models.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public IEnumerable<TopicHeaderViewModel> Topics { get; set; }

    }
}
