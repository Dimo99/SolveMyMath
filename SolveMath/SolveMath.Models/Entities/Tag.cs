using System.Collections.Generic;

namespace SolveMath.Models.Entities
{
    public class Tag
    {
        public Tag()
        {
            Topics = new HashSet<Topic>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }
}