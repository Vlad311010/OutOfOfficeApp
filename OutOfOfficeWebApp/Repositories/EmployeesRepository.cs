using app.Repositories;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;
using System.Linq.Expressions;

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
            return await context.Employees
                .Include(e => e.Status)
                .Include(e => e.Position)
                .Include(e => e.Subdivision)
                .ToListAsync();
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

        public async Task<IEnumerable<Employee>> FindByName(string name)
        {
            return await context.Employees.Where(p => p.FullName.ToString().Contains(name))
                .Include(e => e.PeoplePartner)
                .Include(e => e.Status)
                .Include(e => e.Role)
                .Include(e => e.Position)
                .Include(e => e.Subdivision)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> Where(Expression<Func<Employee, bool>> predicate)
        {
            return await context.Employees.Where(predicate)
                .Include(e => e.PeoplePartner)
                .Include(e => e.Status)
                .Include(e => e.Role)
                .Include(e => e.Position)
                .Include(e => e.Subdivision)
                .ToListAsync();
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

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
