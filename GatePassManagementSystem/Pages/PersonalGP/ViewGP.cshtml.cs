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
    public class ViewGPModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private Common cm;
        public static List<Model.PersonalGP> PersonalGP { get; set; }
        public static List<Model.WorkerGP> WorkerGP { get; set; }
        public ViewGPModel(ApplicationDbContext db)
        {
            _db = db;
            cm = new Common();
        }
        public ApplicationDbContext Db => _db;
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public IEnumerable<Model.WorkerGP> WorkerGPs { get; set; }
        public List<ApprovalChange> Aprvlist { get; set; }
        public Model.Workers Workers { get; set; }
        public Model.PersonalGP PersonalGPB { get; set; }
        
        public WorkerGP WorkerGPB { get; set; }
        public int Uid { get; set; }
        public string WUid { get; set; }
        public string Userrole { get; set; }

        public async Task OnGet()
        {
            try
            {
                Aprvlist = GetDropdownDataApprovalchange();

                Userrole = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                WUid = HttpContext.Session.GetString("UserId");

                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                PersonalGPs = await _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                WorkerGPs = await _db.WorkerGP.Where(gp => gp.CreateUser == WUid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewGPModel OnGet method :" + ex.Message);
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
                    cm.Logwrite("Error in OnPostApproveChange method: if()");
                }
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in OnPostApproveChange method: " + ex.Message);
            }

            return RedirectToPage("ViewGP");
        }


        public IActionResult OnPostApproveChangeWorkers(int id)
        {
            try
            {
                WorkerGP updatedpgpw = _db.WorkerGP.FirstOrDefault(c => c.Id == id);
                if (updatedpgpw != null)
                {
                    updatedpgpw.ChAprlId = WorkerGPB.ChAprlId;
                    _db.SaveChanges();
                }
                else
                {
                    cm.Logwrite("Error in OnPostApproveChangew method: if()");
                }
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in OnPostApproveChangeWorkers method: " + ex.Message);
            }

            return RedirectToPage("ViewGP");
        }

    }
}
