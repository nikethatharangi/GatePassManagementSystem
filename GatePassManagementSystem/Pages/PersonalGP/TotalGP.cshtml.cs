using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GatePassManagementSystem.Pages.PersonalGP
{
    public class TotalGPModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private Common cm;
        public static List<Model.PersonalGP> PersonalGP { get; set; }
        public static List<Model.WorkerGP> WorkerGP { get; set; }
        public TotalGPModel(ApplicationDbContext db)
        {
            _db = db;
            cm = new Common();
        }
        public ApplicationDbContext Db => _db;
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public IEnumerable<Model.WorkerGP> WorkerGPs { get; set; }
        public Model.Workers Workers { get; set; }
        public int Uid { get; set; }
        public string WUid { get; set; }
        public string Userrole { get; set; }

        public async Task OnGet()
        {
            try
            {
                Userrole = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                WUid = HttpContext.Session.GetString("UserId");

                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                PersonalGPs = await _db.PersonalGP.Where(gp => gp.UserId == Uid).ToListAsync();
                WorkerGPs = await _db.WorkerGP.Where(gp => gp.CreateUser == WUid).ToListAsync();
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewGPModel OnGet method :" + ex.Message);
            }
        }
    }
}
