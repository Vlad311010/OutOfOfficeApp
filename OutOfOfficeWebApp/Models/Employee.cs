using Microsoft.EntityFrameworkCore;
using OutOfOfficeWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOfficeWebApp.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        
        [Required, MaxLength(100), Column(TypeName = "varchar")]
        public string FullName { get; set; } = string.Empty;

        [Required, ForeignKey("Subdivision")]
        public int SubdivisionId { get; set; }
        public Subdivision Subdivision { get; set; } = default!;

        [Required, ForeignKey("Position")]
        public int PositionId { get; set; }
        public Position Position { get; set; } = default!;

        [Required, ForeignKey("Status")]
        public int StatusId { get; set; }
        public ActiveStatus Status { get; set; } = default!;

        [Required, ForeignKey("PeoplePartner")]
        public int PeoplePartnerId { get; set; }
        public virtual Employee PeoplePartner { get; set; } = null!;

        [Required]
        public int OutOfOfficeBalance { get; set; }

        public virtual byte[]? Photo { get; set; }

        [Required, ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }


}
