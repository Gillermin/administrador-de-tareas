namespace WebAPI.Models
{
    public class Filter
    {
        public int IdCollaborator { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
