using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageExport_V2.Models;
using ManageExport_V2.Models.Entity;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ManageExport_V2.Services.Interfaces;

namespace ManageExport_V2.Controllers
{
    public class ImagesController : Controller
    {
        private readonly ExportContext _context;        
        private ICommonServices _commonServices;


        public ImagesController(ExportContext context, ICommonServices commonServices)
        {
            _context = context;            
            _commonServices = commonServices;
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            var exportContext = _context.Images.Include(i => i.Product);            
            return View(await exportContext.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Url,ProductId,Id,CreatedDate,ModifiedDate,ImageFile")] Image image)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Products.Find(image.ProductId);
                image.Url= await _commonServices.CreateImage(image.ImageFile, image.Url, "/images/Products/"+product.Name);
                image.CreatedDate = image.ModifiedDate = DateTime.UtcNow;
                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "DisplayName", image.Product.DisplayName);
            return View(image);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            var product = _context.Products.Find(image.ProductId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "DisplayName", product.DisplayName);
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Url,ProductId,Id,CreatedDate,ModifiedDate,ImageFile")] Image image)
        {
            if (id != image.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = _context.Products.Find(image.ProductId);
                    image.Url= await _commonServices.EditImage(image.ImageFile, image.Url, "/images/Products/" + product.Name);
                    image.ModifiedDate = DateTime.Now.ToUniversalTime();
                    _context.Update(image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "DisplayName", image.Product.DisplayName);
            return View(image);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Images.FindAsync(id);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}
