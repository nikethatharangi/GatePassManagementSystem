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
    public class GatePassListHodPendingModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private Common cm;
        public static List<Model.PersonalGP> PersonalGP { get; set; }

        public GatePassListHodPendingModel(ApplicationDbContext db)
        {
            _db = db;
            cm = new Common();
        }

        [BindProperty]
        public Model.PersonalGP PersonalGPB { get; set; }
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public List<ApprovalChange> Aprvlist { get; set; }
        public int Uid { get; set; }
        public string Userrole { get; set; }
        public string Fullname { get; set; }
        public string DeptHead { get; set; }
        public string deptName { get; set; }
        public string DeptGm { get; set; }
        public int deptId { get; set; }
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

                PersonalGPs = await _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                
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
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in GatePassListHodPendingModel OnGet method :" + ex.Message);
            }
        }

        public IActionResult UpdateReason(Model.PersonalGP viewModel)
        {
            var recordToUpdate = _db.PersonalGP.Find(viewModel.Id);
            if (recordToUpdate != null)
            {
               // recordToUpdate.Reason = viewModel.SelectedReason;
                _db.SaveChanges();
                
            }

            return RedirectToAction("GatePassListHodPending");
        }

        public List<ApprovalChange> GetDropdownDataApprovalchange()
        {
            try
            {
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                //var dropdownDataWorker = _db.Workers.FromSqlRaw<Workers>("SELECT * FROM Workers where DeptId=@depId", DeptId).ToList();
                var dropdownDataAprCh = _db.ApprovalChange.FromSqlRaw<ApprovalChange>("SELECT * FROM ApprovalChange").ToList();

                //mr. dharmapriya
                if (deptId == 1 || deptId == 2 || deptId == 3 || deptId == 4 || deptId == 5 || deptId == 6 || deptId == 7 || deptId == 11 || deptId == 24 || deptId == 13)
                {
                    dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 6, FullName = "Mr. Dharmapriya" });
                }
                else if (deptId == 8 || deptId == 9 || deptId == 25) //Mr. ruwan
                {
                    dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 8, FullName = "Mr. Ruwan" });
                }
                else if (deptId == 14 || deptId == 16 || deptId == 17 || deptId == 18 || deptId == 19 || deptId == 21 || deptId == 22 || deptId == 26)  // mr.damith
                {
                    dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 26, FullName = "Mr. Damith" });
                }
                else if (deptId == 15 || deptId == 20 || deptId == 23) // mr. rohan
                {
                    dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 15, FullName = "Mr. Rohan" });
                }
                else if (deptId == 10) // md
                {
                    dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 10, FullName = "Mr. Sugath" });
                }

                return dropdownDataAprCh;
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreateWGPModel GetDropdownDataWorkerEPF method :" + ex.Message);
                return new List<ApprovalChange>();
            }
        }

        public IActionResult OnPostUpdateRecord(int id)
        {
            try
            {
                Model.PersonalGP updatedpgp = _db.PersonalGP.FirstOrDefault(c => c.Id == id);
                if (updatedpgp != null)
                {
                    //if (chkLunch == "true")
                    //{
                    //    updatedpgp.ChkLunch = true;
                    //}
                    //else
                    //{
                    //    updatedpgp.ChkLunch = false;
                    //}

                    //if (chkSinthawatta == "true")
                    //{
                    //    updatedpgp.ChkSinthawatta = true;
                    //}
                    //else
                    //{
                    //    updatedpgp.ChkSinthawatta = false;
                    //}

                    //if (chkHalfd == "true")
                    //{
                    //    updatedpgp.ChkHalfd = true;
                    //}
                    //else
                    //{
                    //    updatedpgp.ChkHalfd = false;
                    //}

                    //if (chkMadu == "true")
                    //{
                    //    updatedpgp.ChkMadu = true;
                    //}
                    //else
                    //{
                    //    updatedpgp.ChkMadu = false;
                    //}

                    //if (chkShrt == "true")
                    //{
                    //    updatedpgp.ChkShrt = true;
                    //}
                    //else
                    //{
                    //    updatedpgp.ChkShrt = false;
                    //}
                    //if (chkCusVisit == "true")
                    //{
                    //    updatedpgp.ChkCusVisit = true;
                    //}
                    //else
                    //{
                    //    updatedpgp.ChkCusVisit = false;
                    //}
                    //if (chkOther == "true")
                    //{
                    //    updatedpgp.ChkOther = true;
                    //}
                    //else
                    //{
                    //    updatedpgp.ChkOther = false;
                    //}

                    updatedpgp.ChkCusVisit = PersonalGPB.ChkCusVisit;
                    updatedpgp.ChkHalfd = PersonalGPB.ChkHalfd;
                    updatedpgp.ChkLunch = PersonalGPB.ChkLunch;
                    updatedpgp.ChkMadu = PersonalGPB.ChkMadu;
                    updatedpgp.ChkSinthawatta = PersonalGPB.ChkSinthawatta;
                    updatedpgp.ChkShrt = PersonalGPB.ChkShrt;
                    updatedpgp.ChkOther = PersonalGPB.ChkOther;
                    updatedpgp.Description = PersonalGPB.Description;

                    _db.SaveChanges();

                }
                else
                {
                    cm.Logwrite("Error in OnPostUpdateRecord method: if()");
                }
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in OnPostUpdateRecord method: " + ex.Message);
            }

            return RedirectToPage("GatePassListHodPending");
        }
    }
}
