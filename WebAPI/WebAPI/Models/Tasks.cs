namespace WebAPI.Models
{
    public class Tasks
    {
        public int IdTask { get; set; }
        public int IdCollaborator { get; set; }
        public string Description { get; set; }
        public string? CollaboratorName { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Notes { get; set; }
  
    }
}
