using System;
using System.Collections.Generic;
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
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        // Create a View
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // Load View sellers data and merge to database, POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        // Show message on View to confirm Delete
        public IActionResult Delete(int? Id)
        {
            if (Id == null) // Check faulty action
            {
                return NotFound();
            }
            // else, one more check
            var obj = _sellerService.FindById(Id.Value);
            if (obj == null) // Check if ID are a null value
            {
                return NotFound();
            }
            // Else, goto page to delete :(
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        // Details page
        // Create view
        public IActionResult Details(int? id)
        {
            if (id == null) // Check faulty action
            {
                return NotFound();
            }
            // else, one more check
            var obj = _sellerService.FindById(id.Value);
            if (obj == null) // Check if ID are a null value
            {
                return NotFound();
            }
            // Else, goto page detais
            return View(obj);
        }

        // Edit view
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}