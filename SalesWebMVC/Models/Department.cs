using System;
using System.Linq;
using System.Collections.Generic;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        // Default Constructor
        public Department()
        {

        }
        // Constructor with arguments
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // Add a seller
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        // Calculate number of sales
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
