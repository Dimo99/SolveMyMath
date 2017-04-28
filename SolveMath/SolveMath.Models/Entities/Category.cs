using System.Collections.Generic;

namespace SolveMath.Models.Entities
{
    public class Category
    {
        public Category()
        {
            Topics = new HashSet<Topic>();
            Categories = new HashSet<Category>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}