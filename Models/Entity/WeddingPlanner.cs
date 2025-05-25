namespace Wedding_Planner.Models.Entity
{
    public class WeddingPlanner
    {
        public int PlannerId { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? YearsOfExperience { get; set; }
        public string ServiceAreas { get; set; }
        public decimal? HourlyRate { get; set; }
        public string Contact { get; set; }

        public User User { get; set; }
    }
}
