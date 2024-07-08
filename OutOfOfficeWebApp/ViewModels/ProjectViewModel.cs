using System.ComponentModel.DataAnnotations;

namespace OutOfOfficeWebApp.ViewModels
{
    public class ProjectViewModel
    {
        public int ID { get; set; }
        [Required]
        public int ProjectTypeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public int ProjectManagerId { get; set; }
        public string? Comment { get; set; }
        [Required]
        public int StatusId { get; set; }
    }
}
