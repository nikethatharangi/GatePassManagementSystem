using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GatePassManagementSystem.Pages.NonreturnableGP
{
    public class CreateNGPModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly Common cm;
        private readonly INotyfService _notify;

        public CreateNGPModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            cm = new Common();
            _notify = notyf;

           
        }

        [BindProperty]
        public Model.NonReturnableGP NonReturnableGP { get; set; }
        [BindProperty]
        public Model.NonReturnItemDsc NonReturnItemDsc { get; set; }

        public Department Department { get; set; }
        public IEnumerable<Model.NonReturnableGP> NonReturnableGPs { get; set; }
        public IEnumerable<Model.NonReturnItemDsc> NonReturnItemDscs { get; set; }

        public List<ApprovalChange> Aprvlist { get; set; }
        public List<NonReturnItemDsc> NonReturnItemDscsl { get; set; }
        public string DeptHead { get; set; }
        public string DeptGm { get; set; }
        public string Fullname { get; set; }
        public string departName { get; set; }
        public int deptId { get; set; }
        public int role { get; set; }
        public string GPId { get; set; }


        public void OnGet()
        {
            Aprvlist = GetDropdownDataApprovalchange();

            Fullname = HttpContext.Session.GetString("FullName");
            deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
            role = Convert.ToInt32(HttpContext.Session.GetString("Roleid"));

            DeptHead = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Hod).FirstOrDefault();
            DeptGm = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Gm).FirstOrDefault();

            departName = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.DeptName).FirstOrDefault();

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

        public void GetId()
        {
            try
            {
                string ddd = AppContext.BaseDirectory;
                var result = _db.NonReturnableGP.OrderBy(gp => gp.NonReturnableGPId).Select(gp => gp.NonReturnableGPId).LastOrDefault();
                if (result == null)
                {
                    GPId = "NP000001";
                }
                else
                {
                    //GPId = "GP" + (Convert.ToInt32(result.Substring(4, result.Length - 4)) + 1).ToString("D4");
                    GPId = GenerateNextId(result);
                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreateNGPModel GetId method :" + ex.Message);
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
                    NextInvNo = "NP00000" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 2)
                {
                    NextInvNo = "NP0000" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 3)
                {
                    NextInvNo = "NP000" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 4)
                {
                    NextInvNo = "NP00" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 5)
                {
                    NextInvNo = "NP0" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 6)
                {
                    NextInvNo = nextNo.ToString();
                }

                return NextInvNo;
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreateNGPModel GenerateNextId method :" + ex.Message);
                return null;
            }
        }

        public async Task<IActionResult> OnPost(List<NonReturnItemDsc> items, string chkPamunuFrom, string chkMaduFrom, string chkSinthaFrom, string chkPamunuTo, string chkMaduTo, string chkSinthaTo)
        {
            try
            {
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                role = Convert.ToInt32(HttpContext.Session.GetString("Roleid"));
                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                //assigns year, month, day, hour, min, seconds
                DateTime start = new DateTime(targetLocalTime.Year, targetLocalTime.Month, targetLocalTime.Day, 8, 1, 1);

                DateTime end = new DateTime(targetLocalTime.Year, targetLocalTime.Month, targetLocalTime.Day, 17, 59, 59);

                //if (targetLocalTime < start || targetLocalTime > end)
                //{
                //    _notify.Error("You Cannot Create GatePasses", 5);
                //    Aprvlist = GetDropdownDataApprovalchange();
                //    GetId();
                //    return RedirectToPage("CreateNGP");
                //}
                if (chkPamunuFrom == "false" && chkMaduFrom == "false" && chkSinthaFrom == "false")
                {
                    _notify.Error("Please Select From Location", 5);
                    GetId();
                    Aprvlist = GetDropdownDataApprovalchange();
                    return RedirectToPage("CreateNGP");
                }
                else if (chkPamunuTo == "false" && chkMaduTo == "false" && chkSinthaTo == "false")
                {
                    _notify.Error("Please Select To Location", 5);
                    GetId();
                    Aprvlist = GetDropdownDataApprovalchange();
                    return RedirectToPage("CreateNGP");
                }
                else
                {

                    if (chkPamunuFrom == "true")
                    {
                        NonReturnableGP.FromLocation = 1;
                    }
                    else if (chkMaduFrom == "true")
                    {
                        NonReturnableGP.FromLocation = 2;
                    }
                    else if (chkSinthaFrom == "true")
                    {
                        NonReturnableGP.FromLocation = 3;
                    }
                    else
                    {
                        NonReturnableGP.FromLocation = 0;
                    }


                    if (chkPamunuTo == "true")
                    {
                        NonReturnableGP.ToLocation = 1;
                    }
                    else if (chkMaduTo == "true")
                    {
                        NonReturnableGP.ToLocation = 2;
                    }
                    else if (chkSinthaTo == "true")
                    {
                        NonReturnableGP.ToLocation = 3;
                    }
                    else
                    {
                        NonReturnableGP.ToLocation = 0;
                    }


                    NonReturnableGP.DepId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                    NonReturnableGP.CreateDate = targetLocalTime;
                    NonReturnableGP.CreateUser = HttpContext.Session.GetString("FullName");
                    NonReturnableGP.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));


                    //await _db.NonReturnItemDsc.AddAsync(NonReturnItemDsc);
                    //await _db.SaveChangesAsync();


                    foreach (var item in items)
                    {
                        item.NonGPId = NonReturnableGP.NonReturnableGPId;
                        _db.NonReturnItemDsc.Add(item);
                        Aprvlist = GetDropdownDataApprovalchange();
                        deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                        role = Convert.ToInt32(HttpContext.Session.GetString("Roleid"));
                    }

                    _db.SaveChanges();
                     await _db.NonReturnableGP.AddAsync(NonReturnableGP);
                    await _db.SaveChangesAsync();

                    _notify.Success("Non-Returnable Gate Pass Successfully Created", 3);
                    //return new JsonResult("Data saved successfully");
                    Aprvlist = GetDropdownDataApprovalchange();
                    return RedirectToPage("CreateNGP");

                }
            }
            catch (Exception ex)
            {
                cm.Logwrite($"Error in CreateNGPModel OnPost method : {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
                return null;
            }
            
        }
    }
}
