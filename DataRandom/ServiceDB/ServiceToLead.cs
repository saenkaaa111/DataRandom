namespace DataRandom.ServiceDB
{
    public class ServiceToLead
    {
        public int Id { get; set; }
        public int Period { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public int LeadId { get; set; }
        public int ServiceId { get; set; }
    }
}
