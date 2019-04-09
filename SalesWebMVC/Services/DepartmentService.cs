using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;

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
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name ).ToList(); // Ordered by name
        }
    }
}
