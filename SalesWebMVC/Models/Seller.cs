using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ("{0} required"))]
        [StringLength(75, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = ("{0} required"))]
        [EmailAddress(ErrorMessage = ("Enter a valid email."))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = ("{0} required"))]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateBirth { get; set; }

        [Required(ErrorMessage = ("{0} required"))]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        // Default Constructor
        public Seller()
        {

        }
        // Constructor with arguments
        public Seller(int id, string name, string email, DateTime dateBirth, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            DateBirth = dateBirth;
            BaseSalary = baseSalary;
            Department = department;
        }

        // Custom methods
        // Add Sale
        public void AddSales(SalesRecord salesRecord)
        {
            Sales.Add(salesRecord);
        }
        // Remove Sale
        public void RemoveSales(SalesRecord salesRecord)
        {
            Sales.Remove(salesRecord);
        }
        // Calculate all Sales from Seller
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(salesRecord => salesRecord.Date >= initial && salesRecord.Date <= final)
                .Sum(salesRecord => salesRecord.Amount);
        }
    }
}
