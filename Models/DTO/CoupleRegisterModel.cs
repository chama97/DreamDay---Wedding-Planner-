namespace Wedding_Planner.Models.DTO
{
    public class CoupleRegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string GroomName { get; set; }
        public string BrideName { get; set; }
        public DateTime? WeddingDate { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
    }
}
