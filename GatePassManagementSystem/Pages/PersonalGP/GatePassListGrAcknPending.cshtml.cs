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
    public class GatePassListGrAcknPendingModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        private Common cm;
        public static List<Model.PersonalGP> PersonalGP { get; set; }
        public Department Department { get; set; }
        public GatePassListGrAcknPendingModel(ApplicationDbContext db)
        {
            _db = db;
            cm = new Common();
        }
        public ApplicationDbContext Db => _db;
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public int Uid { get; set; }
        public string Userrole { get; set; }
        public string DeptHead { get; set; }
        public string Fullname { get; set; }
        public string DeptGm { get; set; }
        public string deptName { get; set; }
        public int deptId { get; set; }
        public int chAprl { get; set; }
        public async Task OnGet()
        {
            try
            {
                Userrole = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                Fullname = HttpContext.Session.GetString("FullName");

                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                deptName = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.DeptName).FirstOrDefault();
                DeptHead = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Hod).FirstOrDefault();

               
                if (deptId == 14 || deptId == 16 || deptId == 17 || deptId == 18 || deptId == 19 || deptId == 21 || deptId == 22)
                {
                    DeptGm = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Dgm).FirstOrDefault();
                }
                else
                {
                    DeptGm = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Gm).FirstOrDefault();
                }
                
                TempData["hod"] = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Hod).FirstOrDefault();

                if (deptId == 14 || deptId == 16 || deptId == 17 || deptId == 18 || deptId == 19 || deptId == 21 || deptId == 22)
                {
                    TempData["gm"] = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Dgm).FirstOrDefault();
                }
                else
                {
                    TempData["gm"] = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Gm).FirstOrDefault();
                }
                
                if (deptId == 8 || deptId == 9)
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm != null && gp.ASguard == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if(deptId == 10)
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASmd != null && gp.ASguard == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod != null && gp.ASdgm != null && gp.ASguard == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in GatePassListGrAcknPendingModel OnGet method :" + ex.Message);
            }
        }
    }
}
