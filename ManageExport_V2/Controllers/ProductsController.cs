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
using Microsoft.AspNetCore.Http;
using ManageExport_V2.Services;

namespace ManageExport_V2.Controllers
{
    public class ProductsController : Controller
    {        
        private IProductServices _productServices;
        private IUserServices _userServices;     
        private IImageServices _imageServices;     
        private IBrandServices _brandServices;     
        private IStockServices _stockServices;
        private IExportProductServices _exportProductServices;        
        public ProductsController(IProductServices productServices, IUserServices userServices, IImageServices imageServices, IBrandServices brandServices, IStockServices stockServices,IExportProductServices exportProductServices)
        {            
            _productServices = productServices;
            _userServices = userServices;
            _imageServices = imageServices;
            _brandServices = brandServices;
            _stockServices = stockServices;
            _exportProductServices = exportProductServices;
        }
        #region Product
        // GET: Products
        public async Task<IActionResult> Index(string SubAgentId,string SubAgentName)
        {
            ViewBag.SAID = SubAgentId;
            ViewBag.SAName = SubAgentName;
            var products = await _productServices.GetProducts(new string[] { "User", "Stock", "Brand" });
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productServices.GetProductById((int)id, new string[] { "User", "Stock", "Brand" });
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BrandId"] = new SelectList(await _brandServices.GetBrand(), "Id", "ShortName");
            ViewData["StockId"] = new SelectList(await _stockServices.GetStock(), "Id", "Name");
            ViewData["SupplyId"] = new SelectList(await _userServices.GetSupply(), "Id", "SupplyName");
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
                await _productServices.CreateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(await _brandServices.GetBrand(), "Id", "ShortName", product.Brand.ShortName);
            ViewData["StockId"] = new SelectList(await _stockServices.GetStock(), "Id", "Name", product.Stock.Name);
            ViewData["SupplyId"] = new SelectList(await _userServices.GetSupply(), "Id", "SupplyName", product.User.SupplyName);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productServices.GetProductById((int)id, new string[] { "User", "Stock", "Brand" });
            if (product == null)
            {
                return NotFound();
            }
            var lstSupplies = await _userServices.GetSupply();
            var currentBrand = await _brandServices.GetBrandById(product.BrandId);
            var currentStock = await _stockServices.GetStockById(product.StockId);
            var currentSupply = lstSupplies.FirstOrDefault(x => x.Id == product.SupplyId).SupplyName;

            ViewData["BrandId"] = new SelectList(await _brandServices.GetBrand(), "Id", "ShortName", product.Brand.ShortName);
            ViewData["StockId"] = new SelectList(await _stockServices.GetStock(), "Id", "Name", product.Stock.Name);
            ViewData["SupplyId"] = new SelectList(lstSupplies, "Id", "SupplyName", product.User.SupplyName);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Code,Name,Number,MFG,EXP,Country,Description,Price,MainImage,RecieveDate,SupplyId,StockId,BrandId,Id,CreatedDate,ModifiedDate,ImageFile,DisplayName,IsActive")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productServices.UpdateProduct(product);
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
            ViewData["BrandId"] = new SelectList(await _brandServices.GetBrand(), "Id", "ShortName", product.Brand.ShortName);
            ViewData["StockId"] = new SelectList(await _stockServices.GetStock(), "Id", "Name", product.Stock.Name);
            ViewData["SupplyId"] = new SelectList(await _userServices.GetSupply(), "Id", "SupplyName", product.User.SupplyName);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _productServices.GetProductById((int)id, new string[] { "User", "Stock", "Brand" });
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
            await _productServices.DeleteProductById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            if (_productServices.GetProductById(id) != null)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Export        
        public async Task<IActionResult> ExportProductPage(string id,string SubAgentId,string SubAgentName)
        {
            var exportProduct = new ExportProductPageViewModel();
            var product = await _productServices.GetProductById(int.Parse(id), new string[] { "User", "Stock", "Brand" });
            exportProduct.Product = product;
            exportProduct.SAID = int.Parse(SubAgentId);
            exportProduct.SubAgentName = SubAgentName;
            if (exportProduct == null)
            {
                return NotFound();
            }
            return View(exportProduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExportProductPage(ExportProductPageViewModel exportProduct,string ProductId)
        {
            // add to session.
            if (ModelState.IsValid)
            {
                if (ProductId != null)
                {
                    var product = await _productServices.GetProductById(int.Parse(ProductId));
                    exportProduct.Product = product;
                    List<ExportProductPageViewModel> lstProducts = HttpContext.Session.GetComplexData<List<ExportProductPageViewModel>>("lstExportProduct");
                    if (lstProducts == null)
                    {
                        HttpContext.Session.SetComplexData("lstExportProduct", new List<ExportProductPageViewModel>());
                        lstProducts = HttpContext.Session.GetComplexData<List<ExportProductPageViewModel>>("lstExportProduct");
                    }
                    if (lstProducts != null)
                    {
                        foreach (var item in lstProducts)
                        {
                            if (item.Product.Code.Equals(exportProduct.Product.Code)&& exportProduct.ExportPrice==item.ExportPrice)
                            {
                                item.ExportNumber += exportProduct.ExportNumber;
                                HttpContext.Session.SetComplexData("lstExportProduct", lstProducts);
                                return RedirectToAction(nameof(Index), new { SubAgentId = exportProduct.SAID.ToString(), SubAgentName = exportProduct.SubAgentName });
                            }
                        }
                        lstProducts.Add(exportProduct);
                    }
                    HttpContext.Session.SetComplexData("lstExportProduct", lstProducts);
                    return RedirectToAction(nameof(Index),new { SubAgentId= exportProduct.SAID.ToString(), SubAgentName=exportProduct.SubAgentName});
                }                            
            }            
            return View(exportProduct);
        }
        public async Task<IActionResult> ListExportProducts(string SubAgentId, string SubAgentName)
        {
            // view list export from session
            List<ExportProductPageViewModel> lstProducts = HttpContext.Session.GetComplexData<List<ExportProductPageViewModel>>("lstExportProduct");
            if (lstProducts != null)
            {
                ViewBag.SAID=SubAgentId;
                ViewBag.SAName=SubAgentName;
               
                return View(lstProducts);
            }            
            return View(new List<ExportProductPageViewModel>());
        }
        public async Task<IActionResult> CreateExportBill()
        {
            // create export bill
            List<ExportProductPageViewModel> lstProducts = HttpContext.Session.GetComplexData<List<ExportProductPageViewModel>>("lstExportProduct");
            if (lstProducts != null)
            {
                var model = new ExportProductViewModel();
                foreach (var item in lstProducts)
                {
                    model.SubsidiaryAgent = new User() { Id = item.SAID,UserType=UserType.SubsidiaryAgent };
                    model.ExportManager = new User() { Id = 1,UserType=UserType.StockExportManager };
                    var exportProduct = item.Product;
                    exportProduct.ExportNumber = item.ExportNumber;
                    exportProduct.ExportPrice = item.ExportPrice;
                    model.ExportProducts.Add(exportProduct);
                    model.TotalMoney += (item.ExportNumber* item.ExportPrice);
                    model.ExportDate = DateTime.UtcNow;

                }                                
                await _exportProductServices.AddExportProduct(model);
                HttpContext.Session.Remove("lstExportProduct");
                return RedirectToAction("ListAllExportProducts");
            }
            return View(new List<ExportProductPageViewModel>());
        }
        public async Task<IActionResult> ListAllExportProducts()
        {
            // after click export view (List all export)

            var lst = await _exportProductServices.ExportProduct(new string[] { "User" });
            return View(lst);
        }

        public async Task<IActionResult> ListExportProductDetail(int Id)
        {
            var exportProductDetail = await _exportProductServices.GetExportProductDetail(Id, new string[] { "Products"});
            foreach (var product in exportProductDetail)
            {
                product.Products.ExportNumber = product.ExportNumber;
                product.Products.ExportPrice = product.ExportPrice;
            }
            return View(exportProductDetail);
        }
        #endregion


    }
}
