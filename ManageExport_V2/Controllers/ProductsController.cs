using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageExport_V2.Models;
using ManageExport_V2.Models.Entity;
using ManageExport_V2.Services.Interfaces;

namespace ManageExport_V2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ExportContext _context;
        private ICommonServices _commonServices;
        private IProductServices _productServices;
        private IUserServices _userServices;

        public ProductsController(ExportContext context,ICommonServices commonServices, IProductServices productServices)
        {
            _context = context;
            _commonServices = commonServices;
            _productServices = productServices;
        }

        public ProductsController(ICommonServices commonServices, IProductServices productServices, IUserServices userServices)
        {
            _commonServices = commonServices;
            _productServices = productServices;
            _userServices = userServices;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products =await _productServices.GetProducts();            
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productServices.GetProductById((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "ShortName");
            ViewData["StockId"] = new SelectList(_context.Stocks, "Id", "Name");
            var lstSupplies = _userServices.GetSupply();
            ViewData["SupplyId"] = new SelectList(lstSupplies, "Id", "SupplyName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name,Number,MFG,EXP,Country,Description,Price,MainImage,RecieveDate,SupplyId,StockId,BrandId,Id,CreatedDate,ModifiedDate,ImageFile,DisplayName")] Product product)
        {
            if (ModelState.IsValid)
            {                
                product.MainImage = await _commonServices.CreateImage(product.ImageFile, product.MainImage, "/images/Products/"+ product.Name);
                product.CreatedDate = product.ModifiedDate = DateTime.UtcNow;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "ShortName", product.Brand.ShortName);
            ViewData["StockId"] = new SelectList(_context.Stocks, "Id", "Name", product.Stock.Name);
            var lstSupplies = _context.Users.Where(x => x.UserType.Equals(UserType.Supply));
            ViewData["SupplyId"] = new SelectList(lstSupplies, "Id", "SupplyName", product.User.SupplyName);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }            
            var lstSupplies = _context.Users.Where(x => x.UserType.Equals(UserType.Supply));            
            var currentBrand = _context.Brands.FirstOrDefault(x => x.Id == product.BrandId).ShortName;
            var currentStock = _context.Stocks.FirstOrDefault(x => x.Id == product.StockId).Name;
            var currentSupply = lstSupplies.FirstOrDefault(x => x.Id == product.SupplyId).SupplyName;

            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "ShortName", currentBrand);
            ViewData["StockId"] = new SelectList(_context.Stocks, "Id", "Name", currentStock);
            ViewData["SupplyId"] = new SelectList(lstSupplies, "Id", "SupplyName", currentSupply);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Code,Name,Number,MFG,EXP,Country,Description,Price,MainImage,RecieveDate,SupplyId,StockId,BrandId,Id,CreatedDate,ModifiedDate,ImageFile,DisplayName")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    product.MainImage= await _commonServices.EditImage(product.ImageFile, product.MainImage, "/images/Products/" + product.Name);
                    product.ModifiedDate = DateTime.UtcNow;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "ShortName", product.Brand.ShortName);
            ViewData["StockId"] = new SelectList(_context.Stocks, "Id", "Name", product.Stock.Name);
            ViewData["SupplyId"] = new SelectList(_context.Users, "Id", "SupplyName", product.User.SupplyName);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Stock)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
