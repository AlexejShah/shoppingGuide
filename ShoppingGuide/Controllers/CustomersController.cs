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
    public class CustomersController : Controller
    {
        private readonly DBContext _context;

        public CustomersController(DBContext context)
        {
            _context = context;
        }
        
        // GET: Customers
        public async Task<ActionResult> Index(string sort, string type)
        {
            var customers = await _context.Customers.ToListAsync();
            if (type != null)
            {
                sort = sort != null ? sort : "asc";
                switch (type) {
                    case "FirstName":
                        if (sort == "asc")
                        {
                            customers = customers.OrderBy(x => x.FirstName).ToList();
                        }
                        else
                        {
                            customers = customers.OrderByDescending(x => x.FirstName).ToList();
                        }
                        break;
                    case "LastName":
                        if (sort == "asc")
                        {
                            customers = customers.OrderBy(x => x.LastName).ToList();
                        }
                        else
                        {
                            customers = customers.OrderByDescending(x => x.LastName).ToList();
                        }
                        break;
                    case "Patronimic":
                        if (sort == "asc")
                        {
                            customers = customers.OrderBy(x => x.Patronimic).ToList();
                        }
                        else
                        {
                            customers = customers.OrderByDescending(x => x.Patronimic).ToList();
                        }
                        break;
                }
                ViewData.Clear();
                ViewData[type + "SortParm"] = sort == "asc" ? "desc" : "asc";
            }
            return View(customers);
        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("GustomerId,FirstName,LastName,Patronimic")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                customers.CustomerId = Guid.NewGuid();
                _context.Add(customers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customers);
        }

        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [Bind("CustomerId,FirstName,LastName,Patronimic")] Customers customers)
        {
            if (id != customers.CustomerId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersExists(customers.CustomerId))
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
            
            return View(customers);
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var customers = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerId == id);
            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomersExists(Guid id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
        
    }
}
