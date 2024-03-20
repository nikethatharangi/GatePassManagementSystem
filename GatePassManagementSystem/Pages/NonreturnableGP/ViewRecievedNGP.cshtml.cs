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

namespace GatePassManagementSystem.Pages.NonreturnableGP
{
    public class ViewRecievedNGPModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        private readonly INotyfService _notify;
        public readonly Common cm;

        public ViewRecievedNGPModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            cm = new Common();
            _notify = notyf;
        }

        public ApplicationDbContext Db => _db;
        [BindProperty]
        public Model.NonReturnableGP NonReturnableGPB { get; set; }
        public IEnumerable<Model.NonReturnableGP> NonReturnableGPs;
        public IEnumerable<NonReturnItemDsc> NonReturnItemDscs;

        public Model.NonReturnableGP NonReturnableGP;
        public Model.NonReturnItemDsc NonReturnItemDsc;

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

                NonReturnableGPs = await _db.NonReturnableGP.Where(gp => gp.ToDept == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.AddMonths(3).Month && (gp.Satisfied == true || gp.Satisfy == true)).ToListAsync();

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewRecievedNGPModel OnGet method :" + ex.Message);
            }
        }


        public IActionResult OnPostHODSatis(int id,string chkNotSatis, string txtHODRemarks, string chkSatis)
        {
            try
            {
                Model.NonReturnableGP updatedpgp = _db.NonReturnableGP.FirstOrDefault(c => c.Id == id);
                if (updatedpgp != null)
                {
                    if(chkNotSatis == "true")
                    {
                        updatedpgp.Satisfy = true;
                    }
                    else
                    {
                        updatedpgp.Satisfy = false;
                    }

                    if(chkSatis == "true")
                    {
                        updatedpgp.Satisfied = true;
                    }
                    else
                    {
                        updatedpgp.Satisfied = false;
                    }


                    updatedpgp.HODRemarks = txtHODRemarks;
                    _db.SaveChanges();
                    _notify.Success("Updated!!", 3);
                    return RedirectToPage("ViewRecievedNGP");
                }
                else
                {
                    cm.Logwrite("Error in OnPostApproveChange method: if()");
                }
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in OnPostApproveChange method in ViewRecievedNGPModel: " + ex.Message);
            }

            return RedirectToPage("ViewRecievedNGP");
        }
    }
}
