using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace GatePassManagementSystem.Pages.PersonalGP
{
    public class ViewPendingGPModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private Common cm;
        private readonly INotyfService _notify;
        public static List<Model.PersonalGP> PersonalGP { get; set; }
        public static List<Model.WorkerGP> WorkerGP { get; set; }
        [BindProperty]
        public Model.PersonalGP PersonalGPB { get; set; }
        [BindProperty]
        public Model.WorkerGP WorkerGPB { get; set; }
        public ViewPendingGPModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            cm = new Common();
            _notify = notyf;
        }
        public ApplicationDbContext Db => _db;
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public IEnumerable<Model.WorkerGP> WorkerGPs { get; set; }
        public int Uid { get; set; }
        public string roleId { get; set; }
        public int deptId { get; set; }
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

                deptName = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.DeptName).FirstOrDefault();
                DeptHead = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Hod).FirstOrDefault();
                DeptGm = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Gm).FirstOrDefault();

                DateTime utcNow = DateTime.UtcNow;
                    TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                    DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                if (Uid == 4) //Mr.Darmapriya
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date && gp.AShod != null && gp.ASdgm == null && (((gp.DepId == 1 || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 4 || gp.DepId == 5 || gp.DepId == deptId || gp.DepId == 6  || gp.DepId == 11 || gp.DepId == 13 || gp.DepId == 24 || gp.DepId == 27 || gp.DepId == 28) && gp.ChApprvlId == 0) || (gp.ChApprvlId == 6 && gp.AShod != null))).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date && gp.AShod != null && gp.ASdgm == null && (((gp.DepId == 1 || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 4 || gp.DepId == 5 || gp.DepId == deptId || gp.DepId == 6 || gp.DepId == 11 || gp.DepId == 13 || gp.DepId == 24) && gp.ChAprlId == 0) || (gp.ChAprlId == 6 && gp.AShod != null))).ToListAsync();
                    //PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && (gp.DepId == deptId || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 5 || gp.DepId == 6) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync(); 
                }
                else if (Uid == 31) //Mr. Ruwan
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && (((gp.DepId == 8 || gp.DepId == 9 || gp.DepId == 25 ) && gp.ChApprvlId == 0) || gp.ChApprvlId == 8 && gp.AShod != null) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.ASdgm == null && (((gp.DepId == 8 || gp.DepId == 9 || gp.DepId == 25) && gp.ChAprlId == 0) || (gp.ChAprlId == 8 && gp.AShod != null)) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    //PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod == null && gp.UserId != Uid && gp.DepId == 8 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Where(gp => gp.ASdgm == null && gp.AShod == null && gp.UserId != Uid && gp.DepId == 9 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 172) //Mr. Thusitha
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && ((gp.DepId == 7 && gp.ChApprvlId == 0) || (gp.ChApprvlId == 10 && gp.AShod != null)) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.ASdgm == null && ((gp.DepId == deptId && gp.ChAprlId == 0) || (gp.ChAprlId == 10 && gp.AShod != null)) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    //PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod == null && gp.UserId != Uid && gp.DepId == 8 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Where(gp => gp.ASdgm == null && gp.AShod == null && gp.UserId != Uid && gp.DepId == 9 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 145) // Mr. Damith
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && (((gp.DepId == 14 || gp.DepId == 16 || gp.DepId == 17 || gp.DepId == 18 || gp.DepId == 19 || gp.DepId == 21 || gp.DepId == 22) && gp.ChApprvlId == 0) || ((gp.ChApprvlId == 26 && gp.AShod != null)))  && gp.AShod != null  && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.ASdgm == null && ((gp.DepId == 14 || gp.DepId == 16 || gp.DepId == 17 || gp.DepId == 18 || gp.DepId == 19 || gp.DepId == 21 || gp.DepId == 22) || (gp.ChAprlId == 26 && gp.AShod != null)) && gp.AShod != null  && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 92) // Mr. Vajira
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.AShod == null && (gp.DepId == deptId || gp.DepId == 27) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.AShod == null && (gp.DepId == deptId || gp.DepId == 27) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 90) // Mr. Rohan
                {
                    var query1 = await _db.PersonalGP // transport, QMS dept
                        .Where(gp => gp.ASdgm == null && (((gp.DepId == 15 || gp.DepId == 20) && gp.ChApprvlId == 0) || (gp.ChApprvlId == 15 && gp.AShod != null)) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date)
                        .ToListAsync();

                    var query2 = await _db.PersonalGP  // stores dept
                        .Where(gp => ((gp.DepId == 23 || gp.ChApprvlId == 15) && gp.ChApprvlId == 0) && gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date)
                        .ToListAsync();

                    PersonalGPs = query1.Union(query2).ToList();
                }
                else if (roleId == "1")
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASmd == null && ((gp.DepId == deptId && gp.ChApprvlId == 0) || gp.ChApprvlId == 10 && gp.AShod != null)  && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.ASmd == null && ((gp.DepId == deptId && gp.ChAprlId == 0) || gp.ChAprlId == 10 && gp.AShod != null) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (roleId == "3")
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => (gp.DepId == deptId && gp.ChApprvlId == 0) && gp.ASgm == null && gp.ASdgm != null && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => (gp.DepId == deptId && gp.ChAprlId == 0) && gp.ASgm == null && gp.ASdgm != null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (roleId == "4")
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => (gp.DepId == deptId && gp.ChApprvlId == 0) && gp.ASdgm == null && gp.AShod != null  && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => (gp.DepId == deptId && gp.ChAprlId == 0) && gp.ASdgm == null && gp.AShod != null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (roleId == "5")
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => (gp.DepId == deptId && gp.ChApprvlId == 0) && gp.AShod == null && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.DepId == deptId && gp.AShod == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }

                //// Fetch data from WorkerGP table
                //workerGPs = await _db.WorkerGP
                //    .Where(gp => gp.DepId == deptId && gp.ASmd == null && gp.ASgm != null)
                //    .ToListAsync();

                //// Combine the lists
                //PersonalGPs = personalGPs.Cast<Model.PersonalGP>().Union(workerGPs.Cast<Model.PersonalGP>()).ToList();
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewPendingGPModel OnGet method :" + ex.Message);
            }
        }
        
        public async Task<IActionResult> OnPostApprove(int Id)
        {
            try
            {
                roleId = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                if (roleId == "1")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASmd = 'A', ASdgm = 'A' , ASmdtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "3")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASgm = 'A' , ASgmtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "4")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASdgm = 'A' , ASdgmtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "5")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET AShod = 'A' , AShodtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewPendingGPModel OnPostApprove: " + ex.Message);
            }
            return RedirectToPage("ViewPendingGP");
        }

        public async Task<IActionResult> OnPostReject(int Id, string RejectReason)
        {
            try
            {
                roleId = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                if (PersonalGPB.RejctReason == null)
                {
                    _notify.Error("Please Insert Reason!!", 5);
                }
                else
                {
                    if (roleId == "1")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASmd = 'R' , ASmdtime = {2} ,RejctReason = {0} WHERE Id = {1}", PersonalGPB.RejctReason, Id, targetLocalTime);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "3")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASgm = 'R' , ASgmtime = {2} ,RejctReason = {0} WHERE Id = {1}", PersonalGPB.RejctReason, Id, targetLocalTime);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "4")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASdgm = 'R' , ASdgmtime = {2} ,RejctReason = {0} WHERE Id = {1}", PersonalGPB.RejctReason, Id, targetLocalTime);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "5")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET AShod = 'R' , AShodtime = {2} ,RejctReason = {0} WHERE Id = {1}", PersonalGPB.RejctReason, Id, targetLocalTime);
                        _notify.Error("Rejected!!", 5);
                    }
                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewPendingGPModel OnPostReject: " + ex.Message);
            }
            return RedirectToPage("ViewPendingGP");
        }

        public async Task<IActionResult> OnPostApproveWGP(int Id)
        {
            try
            {
                roleId = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                if (roleId == "1")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET ASmd = 'A' , ASmdtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "3")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET ASgm = 'A' , ASgmtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "4")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET ASdgm = 'A' , ASdgmtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "5")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET AShod = 'A' , AShodtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewPendingGPModel OnPostApproveWGP: " + ex.Message);
            }
            return RedirectToPage("ViewPendingGP");
        }

        public async Task<IActionResult> OnPostRejectWGP(int Id)
        {
            try
            {
                roleId = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                if (WorkerGPB.RejctReason == null)
                {
                    _notify.Error("Please Insert Reason!!", 5);
                }
                else
                {
                    if (roleId == "1")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET ASmd = 'R' , ASmdtime = {2},RejctReason = {0} WHERE Id = {1}", WorkerGPB.RejctReason, Id, targetLocalTime);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "3")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET ASgm = 'R' , ASgmtime = {2} ,RejctReason = {0} WHERE Id = {1}", WorkerGPB.RejctReason, Id, targetLocalTime);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "4")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET ASdgm = 'R' , ASdgmtime = {2} ,RejctReason = {0} WHERE Id = {1}", WorkerGPB.RejctReason, Id, targetLocalTime);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "5")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET AShod = 'R' , AShodtime = {2} ,RejctReason = {0} WHERE Id = {1}", WorkerGPB.RejctReason, Id, targetLocalTime);
                        _notify.Error("Rejected!!", 5);
                    }
                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewPendingGPModel OnPostRejectWGP: " + ex.Message);
            }
            return RedirectToPage("ViewPendingGP");
        }

    }
}
