using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GatePassManagementSystem.Pages.NonreturnableGP
{
    public class ViewPendingNGPModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        public readonly Common cm;

        public ViewPendingNGPModel(ApplicationDbContext db)
        {
            _db = db;
            cm = new Common();
        }

        public ApplicationDbContext Db => _db;

        public IEnumerable<Model.NonReturnableGP> NonReturnableGPs;
        public IEnumerable<Model.NonReturnItemDsc> NonReturnItemDscs;

        public Model.NonReturnableGP NonReturnableGP;
        public Model.NonReturnItemDsc NonReturnItemDsc;

        public int deptId { get; set; }
        public int Uid { get; set; }
        public string roleId { get; set; }
        public string Fullname { get; set; }
        public string DeptHead { get; set; }
        public string deptName { get; set; }
        public string DeptGm { get; set; }

        public async Task OnGet()
        {
            try
            {
                roleId = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                deptName = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.DeptName).FirstOrDefault();
                DeptHead = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Hod).FirstOrDefault();


                DeptGm = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Gm).FirstOrDefault();

                TempData["hod"] = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Hod).FirstOrDefault();
                TempData["gm"] = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Gm).FirstOrDefault();


                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                if (Uid == 4) //Mr.Darmapriya
                {
                    NonReturnableGPs = await _db.NonReturnableGP.Where(gp => gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date && gp.AShod == "A" && gp.ASdgm == null && (((gp.DepId == 1 || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 4 || gp.DepId == 5 || gp.DepId == deptId || gp.DepId == 6 || gp.DepId == 11 || gp.DepId == 13 || gp.DepId == 24 || gp.DepId == 27 || gp.DepId == 28) && gp.ChApprvlId == 0) || (gp.ChApprvlId == 6 && gp.AShod == "A"))).ToListAsync();
                }
                else if (Uid == 31) //Mr. Ruwan
                {
                    NonReturnableGPs = await _db.NonReturnableGP.Where(gp => gp.ASdgm == null && (((gp.DepId == 8 || gp.DepId == 9 || gp.DepId == 25) && gp.ChApprvlId == 0) || (gp.ChApprvlId == 8 && gp.AShod == "A")) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 172) //Mr. Thusitha
                {
                    NonReturnableGPs = await _db.NonReturnableGP.Where(gp => gp.ASdgm == null && ((gp.DepId == 7 && gp.ChApprvlId == 0) || (gp.ChApprvlId == 7 && gp.AShod == "A")) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 145) // Mr. Damith
                {
                    NonReturnableGPs = await _db.NonReturnableGP.Where(gp => gp.ASdgm == null && ((gp.DepId == deptId || gp.DepId == 14 || gp.DepId == 16 || gp.DepId == 17 || gp.DepId == 18 || gp.DepId == 21 && gp.ChApprvlId == 0) || (gp.ChApprvlId == 26)) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 92) // Mr. Vajira
                {
                    NonReturnableGPs = await _db.NonReturnableGP.Where(gp => gp.AShod == null && (gp.DepId == deptId || gp.DepId == 27) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 96) // mr. Wijekoon
                {
                    NonReturnableGPs = await _db.NonReturnableGP.Where(gp => gp.ASdgm == null && ((gp.DepId == deptId || gp.DepId == 19) && gp.ChApprvlId == 0) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 68) // mr. Geethanga
                {
                    NonReturnableGPs = await _db.NonReturnableGP.Where(gp => gp.ASdgm == null && (gp.DepId == 12 && gp.ChApprvlId == 0) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 109) // mrs. Kumudu
                {
                    var query1 = await _db.NonReturnableGP // packing
                        .Where(gp => gp.AShod == "A" && gp.ASdgm == null && (gp.DepId == deptId || gp.DepId == 18 && gp.ChApprvlId == 0) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();

                    var query2 = await _db.NonReturnableGP  // QA dept
                        .Where(gp => gp.ASdgm == null && (gp.DepId == deptId || gp.DepId == 22 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();

                    NonReturnableGPs = query1.Union(query2).ToList();

                }
                else if (Uid == 90) // Mr. Rohan
                {
                    var query1 = await _db.NonReturnableGP // transport, QMS dept
                        .Where(gp => gp.ASdgm == null && ((gp.DepId == 15 || gp.DepId == 20 && gp.ChApprvlId == 0) || (gp.ChApprvlId == 15 || gp.DepId == 20 && gp.AShod == "A")) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date)
                        .ToListAsync();

                    var query2 = await _db.NonReturnableGP  // stores dept
                        .Where(gp => gp.ASdgm == null && (gp.DepId == 23 && gp.ChApprvlId == 0) && gp.AShod == "A" && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date)
                        .ToListAsync();

                    NonReturnableGPs = query1.Union(query2).ToList();

                }
                else if (roleId == "1")
                {
                    NonReturnableGPs = await _db.NonReturnableGP.Where(gp => gp.ASmd == null && ((gp.DepId == deptId && gp.ChApprvlId == 0) || gp.ChApprvlId == 10 && gp.AShod == "A") && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (roleId == "3")
                {
                    NonReturnableGPs = await _db.NonReturnableGP.Where(gp => (gp.DepId == deptId && gp.ChApprvlId == 0) && gp.ASgm == null && gp.ASdgm == "A" && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (roleId == "4")
                {
                    NonReturnableGPs = await _db.NonReturnableGP.Where(gp => (gp.DepId == deptId && gp.ChApprvlId == 0) && gp.ASdgm == null && gp.AShod == "A" && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (roleId == "5")
                {
                    NonReturnableGPs = await _db.NonReturnableGP.Where(gp => gp.DepId == deptId && gp.AShod == null && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewPendingGPModel OnGet method :" + ex.Message);
            }
        }

    }
}
