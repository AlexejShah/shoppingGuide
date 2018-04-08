using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingGuide.DbModels;
using Microsoft.AspNetCore.Hosting;

namespace ShoppingGuide.Controllers
{
    public class ShoppingsController : Controller
    {
        private readonly DBContext _context;
        
        IHostingEnvironment _appEnvironment;

        public ShoppingsController(DBContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        
        // GET: Shippings
        public async Task<IActionResult> Index(Guid id, string sort, string type)
        {
            if (id == null)
            {
                id = (Guid)ViewData["id"];
            }

            IEnumerable<Shopping> shopping = await _context.Shopping.ToListAsync();

            if (shopping != null)
            {
                shopping = shopping.Where(m => m.CustomerId == id);
                foreach(var item in shopping)
                {
                    item.Photo = await _context.PhotoModel.SingleOrDefaultAsync(x => x.Path.Replace("/Photo/", "").StartsWith(item.Id.ToString()));
                }
            }

            if (shopping == null || shopping.Count() == 0)
            {
                return RedirectToAction(nameof(Create), new { id = id });
            }

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == id);

            if (type != null)
            {
                sort = sort != null ? sort : "asc";
                switch (type)
                {
                    case "DatePurchase":
                        if (sort == "asc")
                        {
                            shopping = shopping.OrderBy(x => x.DatePurchase).ToList();
                        }
                        else
                        {
                            shopping = shopping.OrderByDescending(x => x.DatePurchase).ToList();
                        }
                        break;
                    case "SumReciept":
                        if (sort == "asc")
                        {
                            shopping = shopping.OrderBy(x => x.SumReciept).ToList();
                        }
                        else
                        {
                            shopping = shopping.OrderByDescending(x => x.SumReciept).ToList();
                        }
                        break;
                }
                ViewData.Clear();
                ViewData[type + "SortParm"] = sort == "asc" ? "desc" : "asc";
            }

            ViewData["id"] = id;
            ViewData["customerName"] = customers.FirstName + " " + customers.LastName;

            return View(shopping.ToList());
        }

        // GET: Shippings/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var shopping = await _context.Shopping
                .SingleOrDefaultAsync(m => m.Id == id);
            if (shopping == null)
            {
                return NotFound();
            }

            shopping.Photo = await _context.PhotoModel.SingleOrDefaultAsync(x => x.Path.Replace("/Photo/", "").StartsWith(shopping.Id.ToString()));

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == shopping.CustomerId);
            ViewData["customerName"] = customers.FirstName + " " + customers.LastName;

            return View(shopping);
        }

        // GET: Shippings/Create
        public IActionResult Create(Guid id)
        {
            ViewData["id"] = id;
            return View();
        }

        // POST: Shippings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,DatePurchase,SumReciept,Photo")] Shopping shopping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shopping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = shopping.CustomerId });
            }
            return View(shopping);
        }

        // GET: Shippings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var shopping = await _context.Shopping.SingleOrDefaultAsync(m => m.Id == id);
            if (shopping == null)
            {
                return NotFound();
            }
            
            shopping.Photo = await _context.PhotoModel.SingleOrDefaultAsync(x => x.Path.Replace("/Photo/","").StartsWith(shopping.Id.ToString()));

            var customers = await _context.Customers
               .SingleOrDefaultAsync(m => m.CustomerId == shopping.CustomerId);
            ViewData["customerName"] = customers.FirstName + " " + customers.LastName;
            return View(shopping);
        }

        // POST: Shippings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,DatePurchase,SumReciept,Photo")] Shopping shopping)
        {
            if (id != shopping.Id)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == shopping.CustomerId);
            ViewData["customerName"] = customers.FirstName + " " + customers.LastName;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shopping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingExists(shopping.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(shopping);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Shippings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopping = await _context.Shopping
                .SingleOrDefaultAsync(m => m.Id == id);
            if (shopping == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == shopping.CustomerId);
            ViewData["id"] = customers.CustomerId;
            ViewData["customerName"] = customers.FirstName + " " + customers.LastName;

            return View(shopping);
        }

        // POST: Shippings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Guid CustomerId)
        {
            var shopping = await _context.Shopping.SingleOrDefaultAsync(m => m.Id == id);
            _context.Shopping.Remove(shopping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = CustomerId });
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile, int id, string method)
        {
            if (uploadedFile != null)
            {
                var photoModel = await _context.PhotoModel.SingleOrDefaultAsync(x => x.Path.Replace("/Photo/", "").StartsWith(id.ToString()));
                if (photoModel != null)
                    _context.PhotoModel.Remove(photoModel);
                await _context.SaveChangesAsync();
                string path = "/Photo/" + id + "_" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                PhotoModel file = new PhotoModel { Name = uploadedFile.FileName, Path = path };
                _context.PhotoModel.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction(method, new { id = method == "Create" ? ViewData["id"] : id });
        }

        private bool ShippingExists(int id)
        {
            return _context.Shopping.Any(e => e.Id == id);
        }
    }
}
