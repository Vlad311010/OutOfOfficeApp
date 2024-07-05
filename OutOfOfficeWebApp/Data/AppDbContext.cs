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
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.PeoplePartner)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Status)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Position)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Subdivision)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Role)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Project)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            // Project
            modelBuilder.Entity<Project>()
                .HasOne(x => x.ProjectManager)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Project>()
                .HasOne(x => x.ProjectType)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Project>()
                .HasOne(x => x.Status)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            // Approval Request
            modelBuilder.Entity<ApprovalRequest>()
                .HasOne(x => x.LeaveRequest)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ApprovalRequest>()
                .HasOne(x => x.Approver)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ApprovalRequest>()
                .HasOne(x => x.Status)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            // Leave Request
            modelBuilder.Entity<LeaveRequest>()
                .HasOne(x => x.Employee)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(x => x.AbsenceReason)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(x => x.Status)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            // ProjectEmployees
            modelBuilder.Entity<ProjectEmployees>()
                .HasOne(x => x.Project)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProjectEmployees>()
                .HasOne(x => x.Employee)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
