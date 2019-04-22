using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        // Dependency Injection
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        // Constructor
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // Go to database and find all sellers and print on list
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        // Create a View
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // Load View sellers data and merge to database, POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid) // Validation test to ensure all data has been filed in case of javascript disabled
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        // Show message on View to confirm Delete
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null) // Check faulty action
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided." });
            }
            // else, one more check
            var obj = await _sellerService.FindByIdAsync(Id.Value);
            if (obj == null) // Check if ID are a null value
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found." });
            }
            // Else, goto page to delete :(
            return View(obj);
        }

        // Delete POST Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // Details page
        // Create view
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // Check faulty action
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided." });
            }
            // else, one more check
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) // Check if ID are a null value
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found." });
            }
            // Else, goto page detais
            return View(obj);
        }

        // Edit view
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided." });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found." });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid) // Validation test to ensure all data has been filed in case of javascript disabled
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            if (id != seller.Id) // If seller gives a null value, give a error
            {
                return RedirectToAction(nameof(Error), new { message = "ID mismatch." });
            }
            try
            {
                await _sellerService.UpdateAsync(seller); // Edit method
                return RedirectToAction(nameof(Index)); // After sucess, go to index
            }
            catch (ApplicationException e) // Not found to avoid crash on application
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // Error View
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}