using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;
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

        // Update Seller
        public void Update(Seller obj)
        {
            if (!_context.Seller.Any( x => x.Id == obj.Id ))
            {
                throw new NotFoundException("ID not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbConcurrencyException dbError)
            {
                throw new DbConcurrencyException(dbError.Message);
            }
        }
    }
}
