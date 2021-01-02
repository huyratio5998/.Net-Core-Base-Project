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
    public class SubsidiaryAgentsController : Controller
    {
        // just use for default user.
        private readonly ExportContext _context;        
        private ICommonServices _commonServices;
        private IUserServices _userServices;
        public SubsidiaryAgentsController(ICommonServices commonServices, IUserServices userServices)
        {                       
            _commonServices = commonServices;
            _userServices = userServices;
        }        
        #region SubsidiaryAgent
        // GET: SubsidiaryAgent
        public async Task<IActionResult> Index()
        {
            var subAgent = await _userServices.GetSubsidiaryAgents();               
            return View(subAgent);
        }
        // GET: Users/Details/5 
        public async Task<IActionResult> Details(int id)
        {                       
            var user = await _userServices.GetSubsidiaryAgentById(id);
            if (user == null)
            {
                return NotFound();
            }
            user.CreatedDate= user.CreatedDate.ToLocalTime();
            user.ModifiedDate=user.ModifiedDate.ToLocalTime();
            user.ExpireContractDate=user.ExpireContractDate.ToLocalTime();
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Phone,Email,Note,Age,Gender,Address,City,AgentName,ImageFile,ExpireContractDate")] User user)
        {
            if (ModelState.IsValid)
            {                
                user.Avatar = await _commonServices.CreateImage(user.ImageFile, user.Avatar, "/images/People");                
                await _userServices.CreateSubsidiaryAgent(user);                
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userServices.GetSubsidiaryAgentById(id);
            user.ExpireContractDate= user.ExpireContractDate.ToLocalTime();
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
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Phone,Email,Username,Password,Note,Age,Gender,Address,City,UserType,SubsidiaryTotalProduct,AgentName,SupplyCode,ImageFile,SupplyName,Salary,Id,CreatedDate,ModifiedDate,Avatar,ExpireContractDate,IsActive,UserType")] User user)
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
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userServices.GetSubsidiaryAgentById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        // POST: Users/Delete/5
        [HttpPost, ActionName("DeleteSubsidiaryAgent")]        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteSubsidiaryAgent(int id)
        {
            await _userServices.DeleteVirtual(id);
            return RedirectToAction(nameof(Index));
        }      
        #endregion
    }
}
