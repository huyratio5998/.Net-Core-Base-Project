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
    public class UsersController : Controller
    {
        //private readonly ExportContext _context;        
        private ICommonServices _commonServices;
        private IUserServices _userServices;
        public UsersController(ICommonServices commonServices, IUserServices userServices)
        {                       
            _commonServices = commonServices;
            _userServices = userServices;
        }
        #region UserDefault
        //// GET: Users
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Users.OrderBy(x=>x.UserType).ToListAsync());
        //}

        //// GET: Users/Details/5 
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //// GET: Users/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("FirstName,LastName,Phone,Email,Username,Password,Note,Age,Gender,Address,City,UserType,SubsidiaryTotalProduct,AgentName,SupplyCode,ImageFile,SupplyName,Salary,Id,CreatedDate,ModifiedDate")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Save image to wwwroot/image
        //        //string wwwRootPath = _hostEnvironment.WebRootPath;
        //        //string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
        //        //string extension = Path.GetExtension(user.ImageFile.FileName);
        //        user.Avatar= await _commonServices.CreateImage(user.ImageFile, user.Avatar, "/images/People");
        //        //Insert record
        //        user.CreatedDate=user.ModifiedDate = DateTime.Now.ToUniversalTime();                
        //        _context.Add(user);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));               
        //    }
        //    return View(user);
        //}

        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Phone,Email,Username,Password,Note,Age,Gender,Address,City,UserType,SubsidiaryTotalProduct,AgentName,SupplyCode,ImageFile,SupplyName,Salary,Id,CreatedDate,ModifiedDate,Avatar")] User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return NotFound();   
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            user.Avatar= await _commonServices.EditImage(user.ImageFile, user.Avatar, "/images/People");
        //            user.ModifiedDate = DateTime.Now.ToUniversalTime();
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}

        //// GET: Users/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
        #endregion

        #region SubsidiaryAgent
        // GET: SubsidiaryAgent
        public async Task<IActionResult> SubsidiaryAgentIndex()
        {
            var subAgent = await _userServices.GetSubsidiaryAgents();
            return View(subAgent);
        }
        // GET: Users/Details/5 
        public async Task<IActionResult> SubsidiaryAgentDetails(int id)
        {                       
            var user = _userServices.GetSubsidiaryAgentById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult CreateSubsidiaryAgent()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubsidiaryAgent([Bind("FirstName,LastName,Phone,Email,Username,Password,Note,Age,Gender,Address,City,UserType,SubsidiaryTotalProduct,AgentName,SupplyCode,ImageFile,SupplyName,Salary,Id,CreatedDate,ModifiedDate")] User user)
        {
            if (ModelState.IsValid)
            {                
                user.Avatar = await _commonServices.CreateImage(user.ImageFile, user.Avatar, "/images/People");
                //Insert record
                user.CreatedDate = user.ModifiedDate = DateTime.UtcNow;
                await _userServices.CreateSubsidiaryAgent(user);                
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> EditSubsidiaryAgent(int id)
        {
            var user = await _userServices.GetSubsidiaryAgentById(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubsidiaryAgent(int id, [Bind("FirstName,LastName,Phone,Email,Username,Password,Note,Age,Gender,Address,City,UserType,SubsidiaryTotalProduct,AgentName,SupplyCode,ImageFile,SupplyName,Salary,Id,CreatedDate,ModifiedDate,Avatar")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    user.Avatar = await _commonServices.EditImage(user.ImageFile, user.Avatar, "/images/People");
                    user.ModifiedDate = DateTime.UtcNow;
                    await _userServices.UpdateSubsidiaryAgent(user);                    
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw e;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> DeleteSubsidiaryAgent(int id)
        {


            var user = await _userServices.GetSubsidiaryAgentById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userServices.DeleteSubsidiaryAgent(id);
            return RedirectToAction(nameof(Index));
        }      
        #endregion
    }
}
