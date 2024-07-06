using Microsoft.EntityFrameworkCore.Migrations;
using OutOfOfficeWebApp.Models.Enums;

#nullable disable

namespace OutOfOfficeWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AbsenceReason.InsertEnumData(migrationBuilder, "AbsenceReasons");
            ActiveStatus.InsertEnumData(migrationBuilder, "ActiveStatuses");
            Position.InsertEnumData(migrationBuilder, "Positions");
            ProjectType.InsertEnumData(migrationBuilder, "ProjectTypes");
            RequestStatus.InsertEnumData(migrationBuilder, "RequestStatuses");
            Role.InsertEnumData(migrationBuilder, "Roles");
            Subdivision.InsertEnumData(migrationBuilder, "Subdivisions");

            var sql =
            $"""
            -- Disable foreign key constraints
            ALTER TABLE [dbo].[Employees] NOCHECK CONSTRAINT [FK_Employees_Employees_PeoplePartnerId];
            ALTER TABLE [dbo].[Projects] NOCHECK CONSTRAINT [FK_Projects_Employees_ProjectManagerId];

            DBCC CHECKIDENT ('Employees', RESEED, 1);
            DBCC CHECKIDENT ('Projects', RESEED, 1);

            INSERT INTO [dbo].[Employees] 
                ([FullName], [SubdivisionId], [PositionId], [StatusId], [PeoplePartnerId], [OutOfOfficeBalance], [Photo], [RoleId])
            VALUES 
                ('HR 0',       1,  3, 2, 1, 5,  NULL,  3),
                ('HR 1',       2,  3, 2, 2, 5,  NULL,  3),
                ('PM 0',       1,  2, 2, 1, 3,  NULL,  2),
                ('PM 1',       2,  2, 2, 2, 3,  NULL,  2),
                ('Employee 0', 1,  1, 2, 1, 10, NULL,  1),
                ('Employee 1', 2,  1, 2, 2, 10, NULL,  1),
                ('Employee 2', 1,  1, 1, 2, 0,  NULL,  1),
                ('Admin 0',    1,  3, 2, 1, 0,  NULL,  4);
    

            INSERT INTO [dbo].[Projects] 
                ([ProjectTypeId], [StartDate], [EndDate], [ProjectManagerId], [Comment], [StatusId])
            VALUES 
                (1, '2024-01-01', '2024-12-31', 3, 'Project 1 Description', 2),
                (1, '2024-01-01', '2024-12-31', 4, 'Project 2 Description', 2),
                (2, '2023-01-01', '2023-12-31', 3, 'Project 3 Description', 1);


            -- Enable foreign key constraints
            ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Employees_PeoplePartnerId];
            ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Employees_ProjectManagerId];

            INSERT INTO [dbo].[ProjectEmployees] 
                ([ProjectId], [EmployeeId])
            VALUES 
                (1, 1),
                (2, 2),
                (1, 3),
                (2, 4),
                (1, 5),
                (2, 6),
                (1, 7),
                (1, 8),

                (3, 1),
                (3, 3),
                (3, 5);
            """;


            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
