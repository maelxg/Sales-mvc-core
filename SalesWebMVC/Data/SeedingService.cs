using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        private SalesWebMVCContext _context;

        public SeedingService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Check if have any data inside a database, then stop
            if (_context.Department.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecord.Any())
            {
                return; // DataBase has been seeded
            }

            // Fills Database
            // Create Departments
            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Eletronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");
            // Create Sellers
            Seller s1 = new Seller(1, "Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0, d1);
            // Create Sales
            SalesRecord sr1 = new SalesRecord(1, new DateTime(2019, 04, 01), 1000.0, SaleStatus.Billed, s1);
            // Add objects on Database
            _context.Department.AddRange(d1,d2,d3,d4);
            _context.Seller.AddRange(s1);
            _context.SalesRecord.AddRange(sr1);
            // Save objects on Database
            _context.SaveChanges();
        }
    }
}
