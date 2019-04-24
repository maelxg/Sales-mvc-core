using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<List<Seller>> FindAllAsync()
        {
            // Method to call a database to Sellers and convert to list
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            obj.Department = _context.Department.First();
            _context.Add(obj); // Add on object
            await _context.SaveChangesAsync(); // Save on database
        }

        // Find Sellers
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        // Remove Seller
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj); // Remove on object
                await _context.SaveChangesAsync(); // Save on database
            }
            catch (DbUpdateException)
            {

                throw new IntegrityException("Can´t delete Seller, because he/she has a Sales.");
            }
            
        }

        // Update Seller
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("ID not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException dbError)
            {
                throw new DbConcurrencyException(dbError.Message);
            }
        }
    }
}
