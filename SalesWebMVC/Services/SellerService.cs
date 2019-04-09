﻿using SalesWebMVC.Models;
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
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
