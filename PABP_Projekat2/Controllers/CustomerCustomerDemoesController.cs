using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PABP_Projekat2.Models;

namespace PABP_Projekat2.Controllers
{
    public class CustomerCustomerDemoesController : Controller
    {
        private readonly NorthwindContext _context;

        public CustomerCustomerDemoesController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: CustomerCustomerDemoes
        public async Task<IActionResult> Index()
        {
            var northwindContext = _context.CustomerCustomerDemos.Include(c => c.Customer).Include(c => c.CustomerType);
            return View(await northwindContext.ToListAsync());
        }

        // GET: CustomerCustomerDemoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerCustomerDemo = await _context.CustomerCustomerDemos
                .Include(c => c.Customer)
                .Include(c => c.CustomerType)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerCustomerDemo == null)
            {
                return NotFound();
            }

            return View(customerCustomerDemo);
        }

        // GET: CustomerCustomerDemoes/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["CustomerTypeId"] = new SelectList(_context.CustomerDemographics, "CustomerTypeId", "CustomerTypeId");
            return View();
        }

        // POST: CustomerCustomerDemoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerTypeId")] CustomerCustomerDemo customerCustomerDemo)
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", customerCustomerDemo.CustomerId);
            ViewData["CustomerTypeId"] = new SelectList(_context.CustomerDemographics, "CustomerTypeId", "CustomerTypeId", customerCustomerDemo.CustomerTypeId);
            
            if (_context.CustomerCustomerDemos.Contains(customerCustomerDemo))
            {
                ViewBag.existsCheck = "Record already exists in database!";
                return View();
            }

            if (ModelState.IsValid)
            {
                _context.Add(customerCustomerDemo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(customerCustomerDemo);
        }

        // GET: CustomerCustomerDemoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerCustomerDemo = await _context.CustomerCustomerDemos.FindAsync(id);
            if (customerCustomerDemo == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", customerCustomerDemo.CustomerId);
            ViewData["CustomerTypeId"] = new SelectList(_context.CustomerDemographics, "CustomerTypeId", "CustomerTypeId", customerCustomerDemo.CustomerTypeId);
            return View(customerCustomerDemo);
        }

        // POST: CustomerCustomerDemoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CustomerId,CustomerTypeId")] CustomerCustomerDemo customerCustomerDemo)
        {
            if (id != customerCustomerDemo.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerCustomerDemo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerCustomerDemoExists(customerCustomerDemo.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", customerCustomerDemo.CustomerId);
            ViewData["CustomerTypeId"] = new SelectList(_context.CustomerDemographics, "CustomerTypeId", "CustomerTypeId", customerCustomerDemo.CustomerTypeId);
            return View(customerCustomerDemo);
        }

        // GET: CustomerCustomerDemoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerCustomerDemo = await _context.CustomerCustomerDemos
                .Include(c => c.Customer)
                .Include(c => c.CustomerType)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerCustomerDemo == null)
            {
                return NotFound();
            }

            return View(customerCustomerDemo);
        }

        // POST: CustomerCustomerDemoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customerCustomerDemo = await _context.CustomerCustomerDemos.FindAsync(id);
            _context.CustomerCustomerDemos.Remove(customerCustomerDemo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerCustomerDemoExists(string id)
        {
            return _context.CustomerCustomerDemos.Any(e => e.CustomerId == id);
        }
    }
}
