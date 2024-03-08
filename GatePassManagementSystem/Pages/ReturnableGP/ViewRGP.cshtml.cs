using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GatePassManagementSystem.Pages.ReturnableGP
{
    public class ViewRGPModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        public readonly Common cm;

        public ViewRGPModel(ApplicationDbContext db)
        {
            _db = db;
            cm = new Common();
        }

        public Model.PersonalGP PersonalGPB { get; set; }
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public List<ApprovalChange> Aprvlist { get; set; }

        public string Userrole { get; set; }
        public int Uid { get; set; }

        public async Task OnGetAsync()
        {
            Aprvlist = GetDropdownDataApprovalchange();
            Userrole = HttpContext.Session.GetString("Roleid");
            Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));


            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            PersonalGPs = await _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month).ToListAsync();
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
                cm.Logwrite("Error in GatePassListMgtPendingModel GetDropdownDataApprovalchange method :" + ex.Message);
                return new List<ApprovalChange>();
            }
        }
    }
}
