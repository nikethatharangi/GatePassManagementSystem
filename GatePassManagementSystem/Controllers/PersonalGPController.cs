using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace GatePassManagementSystem.Controllers
{
    [Route("api/PersonalGP")]
    
    [ApiController]
    public class PersonalGPController : Controller
    {
        private readonly Common cm;
        private readonly ApplicationDbContext _db;

        
        public Model.User user;
        
        public PersonalGPController( ApplicationDbContext db)
        {
            cm = new Common();
            _db = db;
           
        }

        //public IActionResult Login()
        //{
        //    HttpContext.Session.SetString("Username","Hello");
        //    return View(); 
        //}

        //public IActionResult CreatePGP()
        //{
        //    user.UserName = HttpContext.Session.GetString("Username");
        //    return View();
        //}

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var GPFromDb = await _db.WorkerGP.FirstOrDefaultAsync(u => u.Id == id);
            if (GPFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.WorkerGP.Remove(GPFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }

    }
}
