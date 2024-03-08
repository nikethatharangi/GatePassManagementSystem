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
    public class ViewNGPModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        public readonly Common cm;

        public ViewNGPModel(ApplicationDbContext db)
        {
            _db = db;
            cm = new Common();
        }

        public ApplicationDbContext Db => _db;

        public IEnumerable<Model.NonReturnableGP> NonReturnableGPs;
        public IEnumerable<NonReturnItemDsc> NonReturnItemDscs;

        public Model.NonReturnableGP NonReturnableGP;
        public Model.NonReturnItemDsc NonReturnItemDsc;
        public List<ApprovalChange> Aprvlist { get; set; }

        public int deptId { get; set; }
        public string Fullname { get; set; }
        public string DeptHead { get; set; }
        public string deptName { get; set; }
        public string DeptGm { get; set; }
        public string Userrole { get; set; }
        public int Uid { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                Userrole = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                Aprvlist = GetDropdownDataApprovalchange();
                Fullname = HttpContext.Session.GetString("FullName");

                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                deptName = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.DeptName).FirstOrDefault();
                DeptHead = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Hod).FirstOrDefault();

                DeptGm = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Gm).FirstOrDefault();

                TempData["hod"] = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Hod).FirstOrDefault();
                TempData["gm"] = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Gm).FirstOrDefault();

                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                NonReturnableGPs = await _db.NonReturnableGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month).ToListAsync();
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewNGPModel OnGet method :" + ex.Message);
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
                    new ApprovalChange { deptId = 7, FullName = "Mr. Thusitha" },
                    new ApprovalChange { deptId = 8, FullName = "Mr. Ruwan" },
                    new ApprovalChange { deptId = 15, FullName = "Mr. Rohan" },
                    new ApprovalChange { deptId = 26, FullName = "Mr. Damith" },

                };

                dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 0, FullName = "Select" });

                return dropdownDataAprCh;
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreateNGPModel GetDropdownDataApprovalchange method :" + ex.Message);
                return new List<ApprovalChange>();
            }
        }

        public IActionResult OnPostApproveChange(int id)
        {
            try
            {
                Model.NonReturnableGP updatedpgp = _db.NonReturnableGP.FirstOrDefault(c => c.Id == id);
                if (updatedpgp != null)
                {
                    updatedpgp.ChApprvlId = NonReturnableGP.ChApprvlId;
                    _db.SaveChanges();
                }
                else
                {
                    cm.Logwrite("Error in ViewNGPModel OnPostApproveChange method: if()");
                }
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewNGPModel OnPostApproveChange method: " + ex.Message);
            }

            return RedirectToPage("ViewNGP");
        }
    }
}
