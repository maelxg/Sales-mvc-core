using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;

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
    }
}
