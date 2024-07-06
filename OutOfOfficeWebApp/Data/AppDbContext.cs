using OutOfOfficeWebApp.Models;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeWebApp.Models.Enums;

namespace app.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectEmployees> ProjectEmployees { get; set; }

        public DbSet<AbsenceReason> AbsenceReasons { get; set; }
        public DbSet<ActiveStatus> ActiveStatuses { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Subdivision> Subdivisions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Employee
            modelBuilder.Entity<Employee>().HasOne(x => x.PeoplePartner).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee>().HasIndex(x => x.PeoplePartnerId).IsUnique(false);

            modelBuilder.Entity<Employee>().HasOne(x => x.Status).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee>().HasIndex(x => x.StatusId).IsUnique(false);

            modelBuilder.Entity<Employee>().HasOne(x => x.Position).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee>().HasIndex(x => x.PositionId).IsUnique(false);

            modelBuilder.Entity<Employee>().HasOne(x => x.Subdivision).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee>().HasIndex(x => x.SubdivisionId).IsUnique(false);

            modelBuilder.Entity<Employee>().HasOne(x => x.Role).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee>().HasIndex(x => x.RoleId).IsUnique(false);

            // Project
            modelBuilder.Entity<Project>().HasOne(x => x.ProjectManager).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Project>().HasIndex(x => x.ProjectManagerId).IsUnique(false);

            modelBuilder.Entity<Project>().HasOne(x => x.ProjectType).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Project>().HasIndex(x => x.ProjectTypeId).IsUnique(false);

            modelBuilder.Entity<Project>().HasOne(x => x.Status).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Project>().HasIndex(x => x.StatusId).IsUnique(false);

            // Approval Request
            modelBuilder.Entity<ApprovalRequest>().HasOne(x => x.LeaveRequest).WithOne().OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ApprovalRequest>().HasOne(x => x.Approver).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ApprovalRequest>().HasIndex(x => x.ApproverId).IsUnique(false);

            modelBuilder.Entity<ApprovalRequest>().HasOne(x => x.Status).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ApprovalRequest>().HasIndex(x => x.StatusId).IsUnique(false);

            // Leave Request
            modelBuilder.Entity<LeaveRequest>().HasOne(x => x.Employee).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LeaveRequest>().HasIndex(x => x.EmployeeId).IsUnique(false);

            modelBuilder.Entity<LeaveRequest>().HasOne(x => x.AbsenceReason).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LeaveRequest>().HasIndex(x => x.AbsenceReasonId).IsUnique(false);

            modelBuilder.Entity<LeaveRequest>().HasOne(x => x.Status).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LeaveRequest>().HasIndex(x => x.StatusId).IsUnique(false);

            // ProjectEmployees
            modelBuilder.Entity<ProjectEmployees>().HasOne(x => x.Project).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProjectEmployees>().HasIndex(x => x.ProjectId).IsUnique(false);

            modelBuilder.Entity<ProjectEmployees>().HasOne(x => x.Employee).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProjectEmployees>().HasIndex(x => x.EmployeeId).IsUnique(false);

        }
    }
}
