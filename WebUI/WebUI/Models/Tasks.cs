using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class Tasks
    {
        public int IdTask { get; set; }

        [Range(1, 999, ErrorMessage = "Debe seleccionar un colaborador!")]
        public int IdCollaborator { get; set; }

        public string? CollaboratorName { get; set; }

        [Required(ErrorMessage = "Campo requerido!")]
        public string? Description { get; set; }

        public string? Status { get; set; }

        public string? Priority { get; set; }

        [Required(ErrorMessage = "Fecha requerida!")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }
      
        [Required(ErrorMessage = "Fecha requerida!")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        public string? Notes { get; set; }
    }

    public class TaskNew
    {
        public int IdTask { get; set; }
        public int IdCollaborator { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string? Notes { get; set; }
    }

    public class Priority
    {
        public int IdPriority { get; set; }
        public string? PriorityData { get; set; }
    }

    public class Status
    {
        public int IdStatus { get; set; }
        public string? StatusData { get; set; }
    }

    public class Collaborator
    {
        public int IdCollaborator { get; set; }
        public string? CollaboratorName { get; set; }
    }
}
