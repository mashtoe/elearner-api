namespace ELearner.Core.Entity.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}