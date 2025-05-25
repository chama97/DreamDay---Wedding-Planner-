namespace Wedding_Planner.Models.Entity
{
    public class Couple
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GroomName { get; set; }
        public string BrideName { get; set; }
        public DateTime? WeddingDate { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }

        public User User { get; set; }
    }
}
