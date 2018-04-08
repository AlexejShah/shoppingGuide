using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingGuide.DbModels;

namespace ShoppingGuide.Controllers
{
    public class CustomersAdditionalsController : Controller
    {
        private readonly DBContext _context;

        public CustomersAdditionalsController(DBContext context)
        {
            _context = context;
        }
        
        // GET: CustomersAdditionals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return RedirectToRoute("Customers");
            }

            var customersAdditional = await _context.CustomersAdditional
                .SingleOrDefaultAsync(m => m.Customerid == id);
            if (customersAdditional == null)
            {
                return RedirectToAction(nameof(Create), new { id = id });
            }
            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == id);
            ViewData["customerName"] = customers.FirstName + " " + customers.LastName;

            return View(customersAdditional);
        }

        // GET: CustomersAdditionals/Create
        public async Task<IActionResult> Create(Guid id)
        {
            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == id);
            ViewData["id"] = id;
            ViewData["customerName"] = customers.FirstName + " " + customers.LastName;
            return View();
        }

        // POST: CustomersAdditionals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Customerid,Email,PhoneFull,Phone,BirthDay")] CustomersAdditional customersAdditional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customersAdditional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = customersAdditional.Customerid });
            }
            return View(customersAdditional);
        }

        // GET: CustomersAdditionals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customersAdditional = await _context.CustomersAdditional.SingleOrDefaultAsync(m => m.Customerid == id);
            if (customersAdditional == null)
            {
                return RedirectToAction(nameof(Create), new { id = id });
            }

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == id);
            ViewData["customerName"] = customers.FirstName + " " + customers.LastName;

            return View(customersAdditional);
        }

        // POST: CustomersAdditionals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Customerid,Email,PhoneFull,Phone,BirthDay")] CustomersAdditional customersAdditional)
        {
            if (id != customersAdditional.Customerid)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == id);
            ViewData["customerName"] = customers.FirstName + " " + customers.LastName;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customersAdditional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersAdditionalExists(customersAdditional.Customerid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = customersAdditional.Customerid });
            }
            return View(customersAdditional);
        }

        // GET: CustomersAdditionals/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customersAdditional = await _context.CustomersAdditional
                .SingleOrDefaultAsync(m => m.Customerid == id);
            if (customersAdditional == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == id);
            ViewData["customerName"] = customers.FirstName + " " + customers.LastName;

            return View(customersAdditional);
        }

        // POST: CustomersAdditionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var customersAdditional = await _context.CustomersAdditional.SingleOrDefaultAsync(m => m.Customerid == id);
            _context.CustomersAdditional.Remove(customersAdditional);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = id });
        }

        private bool CustomersAdditionalExists(Guid id)
        {
            return _context.CustomersAdditional.Any(e => e.Customerid == id);
        }
    }
}
