using Microsoft.EntityFrameworkCore;

namespace OutOfOfficeWebApp.Models
{
    [PrimaryKey("ProjectId", "EmployeeId")]
    public class ProjectEmployees
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }

        public Project Project { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}
