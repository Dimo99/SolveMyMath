namespace SolveMath.Models.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public ApplicationUser User { get; set; }
    }
}