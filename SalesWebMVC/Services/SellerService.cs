using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        // Constructor
        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        // Find all to return a list of all sellers
        public List<Seller> FindAll()
        {
            // Method to call a database to Sellers and convert to list
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            obj.Department = _context.Department.First();
            _context.Add(obj); // Add on object
            _context.SaveChanges(); // Save on database
        }

        // Find Sellers
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        // Remove Seller
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj); // Remove on object
            _context.SaveChanges(); // Save on database
        }
    }
}
