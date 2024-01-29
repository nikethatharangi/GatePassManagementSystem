using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatePassManagementSystem.Controllers
{
    [Route("api/Workers")]
    [ApiController]
    public class WorkerGPController : Controller
    {
        private readonly ApplicationDbContext _db;

        public Workers Workers { get; set; }
        public WorkerGPController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        //public ActionResult CreateWGP()
        //{
        //    ViewBag.Workers = new SelectList(_db.Workers, "Id", "Name");
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.WorkerGP.ToListAsync() });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var WGPFromDb = await _db.WorkerGP.FirstOrDefaultAsync(u => u.Id == id);
            if (WGPFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.WorkerGP.Remove(WGPFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}
