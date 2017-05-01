using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMath.Models.BindingModels
{
    public class DeleteCategoryBindingModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
    }
}
