﻿using app.Repositories;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;

namespace OutOfOfficeWebApp.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly AppDbContext context;
        public EmployeesRepository(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Employee>> All()
        {
            return await context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetById(int id)
        {
            return await context.Employees.Where(e => e.ID == id)
                .Include(e => e.PeoplePartner)
                .Include(e => e.Status)
                .Include(e => e.Role)
                .Include(e => e.Position)
                .Include(e => e.Subdivision)
                .FirstOrDefaultAsync();
        }

        public Task<Employee?> Add(Employee user)
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> Update(Employee user)
        {
            throw new NotImplementedException();
        }
    }
}
