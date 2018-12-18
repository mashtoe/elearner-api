namespace ELearner.Core.Entity.BusinessObjects
{
    public class ApplicationBO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserBO User { get; set; }
    }
}