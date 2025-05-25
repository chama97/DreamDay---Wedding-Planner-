namespace Wedding_Planner.Models.DTO
{
    public class WeddingPlannerRegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }
        public string ServiceAreas { get; set; }
        public decimal HourlyRate { get; set; }
        public string Contact { get; set; }
    }
}
