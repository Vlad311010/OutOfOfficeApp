using OutOfOfficeWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOfficeWebApp.Models
{
    public class Project
    {
        [Key]
        public int ID { get; set; }

        [Required, ForeignKey("ProjectType")]
        public int ProjectTypeId { get; set; }
        public ProjectType ProjectType { get; set; } = new ProjectType(ProjectTypeEnum.Inner);

        [Required, Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? EndDate { get; set; }

        [Required, ForeignKey("ProjectManager")]
        public int ProjectManagerId { get; set; }
        public Employee ProjectManager { get; set; } = default!;

        public string? Comment { get; set; }

        [Required, ForeignKey("Status")]
        public int StatusId { get; set; }
        public ActiveStatus Status { get; set; } = new ActiveStatus(ActiveStatusEnum.Incative);
    }
}
