using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GatePassManagementSystem.Pages.ReturnableGP
{
    public class CreateRGPModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly Common cm;
        private readonly INotyfService _notify;

        public CreateRGPModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            cm = new Common();
            _notify = notyf;
        }

        [BindProperty]
        public Model.PersonalGP PersonalGP { get; set; }
        public Department Department { get; set; }
      
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public List<ApprovalChange> Aprvlist { get; set; }
        public string DeptHead { get; set; }
        public string DeptGm { get; set; }
        public string Fullname { get; set; }
        public string departName { get; set; }
        public string EPFno { get; set; }
        public string GPId { get; set; }

        public void OnGet()
        {
            Aprvlist = GetDropdownDataApprovalchange();
            GetId();
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

        public void GetId()
        {
            try
            {
                string ddd = AppContext.BaseDirectory;
                var result = _db.PersonalGP.OrderBy(gp => gp.PersonalGPId).Select(gp => gp.PersonalGPId).LastOrDefault();
                if (result == null)
                {
                    GPId = "RP000001";
                }
                else
                {
                    //GPId = "GP" + (Convert.ToInt32(result.Substring(4, result.Length - 4)) + 1).ToString("D4");
                    GPId = GenerateNextId(result);
                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreatePGPModel GetId method :" + ex.Message);
            }
        }


        private string GenerateNextId(string lastId)
        {
            try
            {
                string NextInvNo = "";
                string LastInsertedId = lastId;
                int number = Convert.ToInt32(LastInsertedId.Substring(2));
                int nextNo = number + 1;

                if (nextNo.ToString().Length == 1)
                {
                    NextInvNo = "RP00000" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 2)
                {
                    NextInvNo = "RP0000" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 3)
                {
                    NextInvNo = "RP000" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 4)
                {
                    NextInvNo = "RP00" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 5)
                {
                    NextInvNo = "RP0" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 6)
                {
                    NextInvNo = nextNo.ToString();
                }

                return NextInvNo;
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreatePGPModel GenerateNextId method :" + ex.Message);
                return null;
            }
        }
    }
}
