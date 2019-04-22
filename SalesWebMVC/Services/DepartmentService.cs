using SalesWebMVC.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;

        // Constructor
        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        // method to return all list of departments
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name ).ToListAsync(); // Ordered by name
        }
    }
}
