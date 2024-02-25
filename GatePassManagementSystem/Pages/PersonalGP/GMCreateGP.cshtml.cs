using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GatePassManagementSystem.Pages.PersonalGP
{
    public class GMCreateGPModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly Common cm;
        private readonly INotyfService _notify;

        public GMCreateGPModel(ApplicationDbContext db, INotyfService notyf)//,IHttpContextAccessor httpContextAccessor, ISession session)
        {
            _db = db;
            cm = new Common();
            _notify = notyf;
        }

        [BindProperty]
        public Model.PersonalGP PersonalGP { get; set; }
        public Department Department { get; set; }
        public Model.User User { get; set; }
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public List<User> StaffList { get; set; }
        public List<Department> deptList { get; set; }
        public string DeptHead { get; set; }
        public string DeptGm { get; set; }
        public string GPId { get; set; }
        public int currentUserId { get; set; }
        public string Fullname { get; set; }
        public string departName { get; set; }
        public int deptId { get; set; }
        public int role { get; set; }
       

        public void OnGet()
        {
            StaffList = GetDropdownStaffList();
            deptList = GetDropdownDeptList();
            GetId();

            Fullname = HttpContext.Session.GetString("FullName");
            role = Convert.ToInt32(HttpContext.Session.GetString("Roleid"));
        }

        public List<User> GetDropdownStaffList()
        {
            try
            {
                // Fetch both Id, Name, and EPFNo
                var dropdownDataStaff = _db.User
                    .Select(w => new User { Id = w.Id, FullName = w.FullName, EPFNumber = w.EPFNumber })
                    .ToList();

                // Insert a default option
                dropdownDataStaff.Insert(0, new User { Id = 0, FullName = "Select", EPFNumber = "" });

                return dropdownDataStaff;
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in GMCreateGPModel GetDropdownStaffList method :" + ex.Message);
                return new List<User>();
            }
        }

        public List<Department> GetDropdownDeptList()
        {
            try
            {
                var dropdownDataDept = _db.Department.ToList();

                // Insert a default option
                dropdownDataDept.Insert(0, new Department { Id = 0, DeptName = "Select"});

                return dropdownDataDept;
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in GMCreateGPModel GetDropdownStaffList method :" + ex.Message);
                return new List<Department>();
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
                    GPId = "GP000001";
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
                    NextInvNo = "GP00000" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 2)
                {
                    NextInvNo = "GP0000" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 3)
                {
                    NextInvNo = "GP000" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 4)
                {
                    NextInvNo = "GP00" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 5)
                {
                    NextInvNo = "GP0" + nextNo.ToString();
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


        public async Task<IActionResult> OnPost(string chkPam, string chkOfficial, string chkCusVisit, string chkLunch, string chkSinthawatta, string chkHalfd, string chkMadu, string chkShrt, string chkOther)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                    role = Convert.ToInt32(HttpContext.Session.GetString("Roleid"));
                    DateTime utcNow = DateTime.UtcNow;
                    TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                    DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                    //assigns year, month, day, hour, min, seconds
                    DateTime start = new DateTime(targetLocalTime.Year, targetLocalTime.Month, targetLocalTime.Day, 8, 1, 1);

                    DateTime end = new DateTime(targetLocalTime.Year, targetLocalTime.Month, targetLocalTime.Day, 17, 59, 59);


                    if (targetLocalTime < start || targetLocalTime > end)
                    {
                        _notify.Error("You Cannot Create GatePasses", 5);
                        StaffList = GetDropdownStaffList();
                        deptList = GetDropdownDeptList();
                        GetId();
                        return RedirectToPage("CreatePGP");
                    }
                    else if (chkLunch == "false" && chkSinthawatta == "false" && chkHalfd == "false" && chkMadu == "false" && chkShrt == "false" && chkOther == "false" && chkCusVisit == "false" && chkPam == "false" && chkOfficial == "false")
                    {
                        _notify.Error("Please Select Reason", 5);
                        GetId();
                        StaffList = GetDropdownStaffList();
                        deptList = GetDropdownDeptList();
                        return RedirectToPage("CreatePGP");
                    }
                    else if (chkOther == "true" && PersonalGP.Description == null)
                    {
                        _notify.Error("Please give the Reason in the Description field", 5);
                        GetId();
                        StaffList = GetDropdownStaffList();
                        deptList = GetDropdownDeptList();
                        return RedirectToPage("CreatePGP");
                    }
                    else
                    {
                        if (chkOfficial == "true")
                        {
                            PersonalGP.ChkOfficialwork = true;
                        }
                        else
                        {
                            PersonalGP.ChkOfficialwork = false;
                        }

                        if (chkPam == "true")
                        {
                            PersonalGP.ChkPamunugama = true;
                        }
                        else
                        {
                            PersonalGP.ChkPamunugama = false;
                        }

                        if (chkLunch == "true")
                        {
                            PersonalGP.ChkLunch = true;
                        }
                        else
                        {
                            PersonalGP.ChkLunch = false;
                        }

                        if (chkSinthawatta == "true")
                        {
                            PersonalGP.ChkSinthawatta = true;
                        }
                        else
                        {
                            PersonalGP.ChkSinthawatta = false;
                        }

                        if (chkHalfd == "true")
                        {
                            PersonalGP.ChkHalfd = true;
                        }
                        else
                        {
                            PersonalGP.ChkHalfd = false;
                        }

                        if (chkMadu == "true")
                        {
                            PersonalGP.ChkMadu = true;
                        }
                        else
                        {
                            PersonalGP.ChkMadu = false;
                        }

                        if (chkShrt == "true")
                        {
                            PersonalGP.ChkShrt = true;
                        }
                        else
                        {
                            PersonalGP.ChkShrt = false;
                        }
                        if (chkCusVisit == "true")
                        {
                            PersonalGP.ChkCusVisit = true;
                        }
                        else
                        {
                            PersonalGP.ChkCusVisit = false;
                        }
                        if (chkOther == "true")
                        {
                            PersonalGP.ChkOther = true;
                        }
                        else
                        {
                            PersonalGP.ChkOther = false;
                        }

                        PersonalGP.Reason = "NULL";
                        PersonalGP.CreateDate = targetLocalTime;
                        PersonalGP.CreateUser = HttpContext.Session.GetString("FullName");
                        PersonalGP.AShod = "A";
                        PersonalGP.ASdgm = "A";
                        PersonalGP.ChkifDeptHeadUn = true;
                        

                        await _db.PersonalGP.AddAsync(PersonalGP);
                        await _db.SaveChangesAsync();

                        _notify.Success("Gate Pass Successfully Created", 3);
                        return RedirectToPage("GMCreateGP");

                    }
                }
            }
            catch (Exception ex)
            {
                cm.Logwrite($"Error in CreateWGPModel OnPost method: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
            return RedirectToPage("GMCreateGP");
        }
    }
}
