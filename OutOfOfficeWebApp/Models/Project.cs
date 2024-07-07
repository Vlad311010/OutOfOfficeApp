using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.ViewModels;
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
        public ProjectType ProjectType { get; set; } = default!;

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
        public ActiveStatus Status { get; set; } = default!;

        public Project() { }
        public Project(ProjectViewModel viewModel)
        {
            ID = viewModel.ID;
            ProjectTypeId = viewModel.ProjectTypeId;
            StartDate = viewModel.StartDate;
            EndDate = viewModel.EndDate;
            ProjectManagerId = viewModel.ProjectManagerId;
            Comment = viewModel.Comment;
            StatusId = viewModel.StatusId;
        }
    }
}
