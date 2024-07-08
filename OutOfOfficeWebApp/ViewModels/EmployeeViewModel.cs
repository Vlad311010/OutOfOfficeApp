using System.ComponentModel.DataAnnotations;

namespace OutOfOfficeWebApp.ViewModels
{
    public class EmployeeViewModel
    {
        public int ID { get; set; }
        [Required, StringLength(50, MinimumLength=1)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public int SubdivisionId { get; set; }
        [Required]
        public int PositionId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int PeoplePartnerId { get; set; }
        [Required, Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int OutOfOfficeBalance { get; set; }
        public virtual byte[]? Photo { get; set; }
        
        [Required]
        public int RoleId { get; set; }
        public List<int> AssignedProjects { get; set; } = new List<int>();
    }
}
