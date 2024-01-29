using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GatePassManagementSystem.Pages.PersonalGP
{
    public class GatePassListMgtPendingModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private Common cm;
        private readonly INotyfService _notify;
        public static List<Model.PersonalGP> PersonalGP { get; set; }

        public GatePassListMgtPendingModel(ApplicationDbContext db)
        {
            _db = db;
            cm = new Common();
        }
        public ApplicationDbContext Db => _db;
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        [BindProperty]
        public Model.PersonalGP PersonalGPB { get; set; }
        public int Uid { get; set; }
        public string Userrole { get; set; }
        public List<ApprovalChange> Aprvlist { get; set; }
        public string DeptHead { get; set; }
        public string Fullname { get; set; }
        public string deptName { get; set; }
        public string DeptGm { get; set; }
        public int deptId { get; set; }

        public bool chkifDeptHeadUn { get; set; }

        //[BindProperty]
        //public int ChApprvlId { get; set; }

        public async Task OnGet()
        {
            try
            {
                Aprvlist = GetDropdownDataApprovalchange();
                
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
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if(deptId == 10)
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASmd == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod != null && gp.ASdgm == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in GatePassListMgtPendingModel OnGet method :" + ex.Message);
            }
        }

        public List<ApprovalChange> GetDropdownDataApprovalchange()
        {
            try
            {
                var dropdownDataAprCh = new List<ApprovalChange>
                {
                    new ApprovalChange { deptId = 10, FullName = "Mr. Sugath(MD)" },
                    new ApprovalChange { deptId = 6, FullName = "Mr. Dharmapriya" },
                    new ApprovalChange { deptId = 10, FullName = "Mr. Thusitha" },
                    new ApprovalChange { deptId = 8, FullName = "Mr. Ruwan" },
                    new ApprovalChange { deptId = 15, FullName = "Mr. Rohan" },
                    new ApprovalChange { deptId = 26, FullName = "Mr. Damith" },

                };

                dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 0, FullName = "Select" });

                return dropdownDataAprCh;
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in GatePassListMgtPendingModel GetDropdownDataApprovalchange method :" + ex.Message);
                return new List<ApprovalChange>();
            }
        }



        //public List<ApprovalChange> GetDropdownDataApprovalchange()
        //{
        //    try
        //    {
        //        deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

        //        //var dropdownDataWorker = _db.Workers.FromSqlRaw<Workers>("SELECT * FROM Workers where DeptId=@depId", DeptId).ToList();
        //        var dropdownDataAprCh = _db.ApprovalChange.ToList();
        //        dropdownDataAprCh.Insert(0, new ApprovalChange { ChId = 0, FullName = "Select" });

        //        //mr. dharmapriya
        //        //if (deptId == 1 || deptId == 2 || deptId == 3 || deptId == 4 || deptId == 5 || deptId == 6 || deptId == 7 || deptId == 11 || deptId == 24 || deptId == 13)
        //        //{
        //        //    dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 6, FullName = "Mr. Dharmapriya" });
        //        //}
        //        //else if(deptId == 8 || deptId == 9 || deptId == 25) //Mr. ruwan
        //        //{
        //        //    dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 8, FullName = "Mr. Ruwan" });
        //        //}
        //        //else if(deptId == 14 || deptId == 16 || deptId == 17 || deptId == 18 || deptId == 19 || deptId == 21 || deptId == 22 || deptId == 26)  // mr.damith
        //        //{
        //        //    dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 26, FullName = "Mr. Damith" });
        //        //}
        //        //else if(deptId == 15 || deptId == 20 || deptId == 23) // mr. rohan
        //        //{
        //        //    dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 15, FullName = "Mr. Rohan" });
        //        //}
        //        //else if (deptId == 10) // md
        //        //{
        //        //    dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 10, FullName = "Mr. Sugath" });
        //        //}

        //        return dropdownDataAprCh;
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite("Error in GatePassListMgtPendingModel GetDropdownDataApprovalchange method :" + ex.Message);
        //        return new List<ApprovalChange>();
        //    }
        //}






        //public async Task<IActionResult> OnPost(string chkifDeptHeadUn,int id)//string chkCusVisit, string chkLunch, string chkSinthawatta, string chkHalfd, string chkMadu, string chkShrt, string chkOther)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var personalGP = _db.PersonalGP.Find(id);
        //            personalGP.ChApprvlId = 1;
        //            //if (chkifDeptHeadUn == "true")
        //            //{
        //            //    personalGP.ChApprvlId = 1;
        //            //}
        //            //else
        //            //{
        //            //    personalGP.ChApprvlId = 0;
        //            //}

        //            await _db.PersonalGP.AddAsync(PersonalGPB);
        //            await _db.SaveChangesAsync();

        //            return RedirectToPage("GatePassListMgtPending");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite($"Error in CreateWGPModel OnPost method: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
        //    }
        //    return RedirectToPage("GatePassListMgtPending");
        //}

        //public async Task<IActionResult> OnPostApproveChange(string id)//Model.PersonalGP personalGP)
        //{
        //    try
        //    {
        //        ////await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASmd = 'A' , ASmdtime = GETDATE() WHERE Id = {0}", Id);
        //        //_db.Update(personalGP);
        //        //_notify.Success("You Changed Approver", 5);

        //        await _db.PersonalGP.Where(gp => gp.PersonalGPId == id).AnyAsync();
        //        await _db.SaveChangesAsync();
        //        //_notify.Success("You Changed Approver", 3);
        //        Aprvlist = GetDropdownDataApprovalchange();
        //        return RedirectToPage("GatePassListMgtPending");
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite("Error in ViewPendingGPModel OnPostApprove: " + ex.Message);
        //    }
        //    return RedirectToPage("GatePassListMgtPending");
        //}

        //public async Task<IActionResult> OnPostApproveChangeAsync()
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var submittedId = Request.Form["Id"];
        //            //var existingEntity = 
        //            await _db.PersonalGP.FirstOrDefaultAsync(e => e.PersonalGPId == submittedId);

        //            //if (existingEntity == null)
        //            //{
        //            //    return NotFound();
        //            //}

        //            PersonalGPB.ChApprvlId = ChApprvlId;
        //            await _db.PersonalGP.AddAsync(PersonalGPB);

        //            await _db.SaveChangesAsync();
        //            return Page();
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite("Error in OnPostApproveChangeAsync method :" + ex.Message);
        //    }
        //    return RedirectToPage("GatePassListMgtPending");
        //}

        //public async Task<IActionResult> OnPostApproveChangeAsync()
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var subtdId = Request.Form["Id"];

        //            if (int.TryParse(subtdId, out int submittedId))
        //            {
        //                if (PersonalGPB != null)
        //                {
        //                    await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ChApprvlId = {1} WHERE Id = {0}", ChApprvlId, submittedId);
        //                    //_notify.Success("You Changed Management Approver!!", 5);
        //                }
        //                else
        //                {
        //                    cm.Logwrite("PersonalGPB is null.");
        //                }
        //            }
        //            else
        //            {
        //                cm.Logwrite("Invalid Id format.");
        //            }

        //            return Page();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite("Error in OnPostApproveChangeAsync method: " + ex.Message);
        //    }
        //    return RedirectToPage("GatePassListMgtPending");
        //}

        //public IActionResult OnPostApproveChange(int id)
        //{
        //    try
        //    {
        //        var personalGP = _db.PersonalGP.Find(id);

        //        if (personalGP != null)
        //        {
        //            personalGP.ChApprvlId = 1;
        //            _db.SaveChanges();
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite("Error in OnPostApproveChangeAsync method :" + ex.Message);
        //    }

        //    return RedirectToPage("GatePassListMgtPending");
        //}


        //public async Task<IActionResult> OnPostApproveChangeAsync(int id)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            int chApprvlId = PersonalGPB.ChApprvlId;
        //            var gatePass = await _db.PersonalGP.FindAsync(id);

        //            if (gatePass != null)
        //            {
        //                gatePass.ChApprvlId = chApprvlId;
        //                await _db.SaveChangesAsync();

        //                return RedirectToPage("GatePassListMgtPending");
        //            }
        //            else
        //            {
        //                cm.Logwrite("Error in OnPostApproveChangeAsync method");
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite("Error in OnPostApproveChangeAsync method :" + ex.Message);
        //    }

        //    return RedirectToPage("GatePassListMgtPending");
        //}




        //try
        //{

        //    //PersonalGPB.ChApprvlId = PersonalGPB.ChApprvlId;
        //    //_db.Entry(PersonalGP).State = EntityState.Modified;
        //    //_db.SaveChanges();
        //    var studentbyid = _db.PersonalGP.Where(x => x.Id == id).FirstOrDefault();
        //    if (studentbyid != null)
        //    {
        //        _db.Entry(studentbyid).State = EntityState.Modified;
        //        _db.SaveChanges();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    cm.Logwrite("Error in OnPostApproveChangeAsync method :" + ex.Message);
        //}


        //public IActionResult OnPostApproveChange(string id)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var errors = ModelState.Values
        //                .SelectMany(v => v.Errors)
        //                .Select(e => e.ErrorMessage);

        //            var errorMessage = string.Join(" | ", errors);
        //            cm.Logwrite("Error in OnPostApproveChange method: " + errorMessage);
        //        }

        //        Model.PersonalGP updatedpgp = _db.PersonalGP.FirstOrDefault(c => c.PersonalGPId == id);
        //        if (updatedpgp != null)
        //        {
        //            updatedpgp.PersonalGPId = id;

        //            updatedpgp.ChApprvlId = PersonalGPB.ChApprvlId;

        //            _db.SaveChanges();
        //        }
        //        else
        //        {
        //            return RedirectToPage("GatePassListMgtPending");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite("Error in OnPostApproveChange method: " + ex.Message);
        //    }

        //    return RedirectToPage("GatePassListMgtPending");
        //}

        //////////////////////////////////////////////////////////////////// stable function
        public IActionResult OnPostApproveChange(int id)
        {
            try
            {
                Model.PersonalGP updatedpgp = _db.PersonalGP.FirstOrDefault(c => c.Id == id);
                if (updatedpgp != null)
                {
                    
                    updatedpgp.ChApprvlId = PersonalGPB.ChApprvlId;
                    _db.SaveChanges();
                   
                }
                else
                {
                    cm.Logwrite("Error in OnPostApproveChangeAsync method: if()");
                }
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in OnPostApproveChangeAsync method: " + ex.Message);
            }

            return RedirectToPage("GatePassListMgtPending");
        }

    }
}
