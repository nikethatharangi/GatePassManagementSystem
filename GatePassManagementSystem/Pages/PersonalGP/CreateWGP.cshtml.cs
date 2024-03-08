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

namespace GatePassManagementSystem.Pages.PersonalGP
{
    public class CreateWGPModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private Common cm;
        private readonly INotyfService _notify;

        public CreateWGPModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            cm = new Common();
            _notify = notyf;
        }

        [BindProperty]
        public WorkerGP WorkerGP { get; set; }

        public Model.User User { get; set; }
        public Model.Workers Workers { get; set; }
        public List<Department> deptlist { get; set; }
        public List<Workers> workerlist { get; set; }
        public List<ApprovalChange> Aprvlist { get; set; }
        public List<Workers> workerepflist { get; set; }
        //public IEnumerable<workerlist> epfno { get; set; }
        public string WGPId { get; set; }
        public string Uid { get; set; }
        public string depId { get; set; }
        public string EPFno { get; set; }
        public string DeptHead { get; set; }
        public string DeptGm { get; set; }
        public int deptId { get; set; }
        public string departName { get; set; }

        public void OnGet()
        {
            try
            {
                GetId();
                //deptlist = GetDropdownDataDept();
                workerlist = GetDropdownDataWorker();
                Aprvlist = GetDropdownDataApprovalchange();
                //workerepflist = GetDropdownDataWorkerEPF();
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
            
                DeptHead = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Hod).FirstOrDefault();
                DeptGm = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Gm).FirstOrDefault();
                departName = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.DeptName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreateWGPModel OnGet method :" + ex.Message);
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
                cm.Logwrite("Error in GatePassListMgtPendingModel GetDropdownDataApprovalchange method :" + ex.Message);
                return new List<ApprovalChange>();
            }
        }


        //public List<ApprovalChange> GetDropdownDataApprovalchange()
        //{
        //    try
        //    {
        //        deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

        //        var dropdownDataAprCh = _db.ApprovalChange.FromSqlRaw<ApprovalChange>("SELECT * FROM ApprovalChange").ToList();

        //        //mr. dharmapriya
        //        if (deptId == 1 || deptId == 2 || deptId == 3 || deptId == 4 || deptId == 5 || deptId == 6 || deptId == 7 || deptId == 11 || deptId == 24 || deptId == 13)
        //        {
        //            dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 6, FullName = "Mr. Dharmapriya" });
        //        }
        //        else if (deptId == 8 || deptId == 9 || deptId == 25) //Mr. ruwan
        //        {
        //            dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 8, FullName = "Mr. Ruwan" });
        //        }
        //        else if (deptId == 14 || deptId == 16 || deptId == 17 || deptId == 18 || deptId == 19 || deptId == 21 || deptId == 22 || deptId == 26)  // mr.damith
        //        {
        //            dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 26, FullName = "Mr. Damith" });
        //        }
        //        else if (deptId == 15 || deptId == 20 || deptId == 23) // mr. rohan
        //        {
        //            dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 15, FullName = "Mr. Rohan" });
        //        }
        //        else if (deptId == 10) // md
        //        {
        //            dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 10, FullName = "Mr. Sugath" });
        //        }

        //        return dropdownDataAprCh;
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite("Error in CreateWGPModel GetDropdownDataWorkerEPF method :" + ex.Message);
        //        return new List<ApprovalChange>();
        //    }
        //}

        public List<Workers> GetDropdownDataWorker()
        {
            try
            {
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                // Fetch both Id, Name, and EPFNo
                var dropdownDataWorker = _db.Workers.Where(w => w.DeptId == deptId || w.DeptId == 12)
                    .Select(w => new Workers { Id = w.Id, Name = w.Name, EPFNo = w.EPFNo })
                    .ToList();

                // Insert a default option
                dropdownDataWorker.Insert(0, new Workers { Id = 0, Name = "Select", EPFNo = "" });

                return dropdownDataWorker;
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreateWGPModel GetDropdownDataWorker method :" + ex.Message);
                return new List<Workers>();
            }
        }

        //public IActionResult OnGetGetEPFNumber(int selectedIndex)
        //{
        //    // Ensure the selectedIndex is within bounds
        //    if (selectedIndex >= 0 && selectedIndex < workerlist.Count)
        //    {
        //        var selectedWorkerId = workerlist[selectedIndex].Id;
        //        var epfNumber = _db.Workers
        //            .Where(w => w.Id == selectedWorkerId)
        //            .Select(w => w.EPFNo)
        //            .FirstOrDefault();

        //        return Content(epfNumber ?? "EPF Number not found");
        //    }

        //    return Content("Invalid selected index");
        //}

        //public List<Workers> GetDropdownDataWorkerEPF()
        //{
        //    try
        //    {
        //        DeptId = HttpContext.Session.GetString("DepartId");

        //        //var dropdownDataWorker = _db.Workers.FromSqlRaw<Workers>("SELECT * FROM Workers where DeptId=@depId", DeptId).ToList();
        //        var dropdownDataWorker = _db.Workers.FromSqlRaw<Workers>("SELECT * FROM Workers").ToList();
        //        dropdownDataWorker.Insert(0, new Workers { Id = 0, Name = "Select" });
        //        return dropdownDataWorker;
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite("Error in CreateWGPModel GetDropdownDataWorkerEPF method :" + ex.Message);
        //        return new List<Workers>();
        //    }
        //}

        //public List<Department> GetDropdownDataDept()
        //{
        //    try
        //    {
        //        Uid = HttpContext.Session.GetString("UserId");

        //        //var dropdownData = _db.Department.FromSqlRaw<Department>("SELECT d.* FROM Department d INNER JOIN User u ON d.Id = u.DepartId WHERE u.Id=@UId",Uid).ToList();
        //        var dropdownData = _db.Department.FromSqlRaw<Department>("SELECT * FROM Department").ToList();
        //        dropdownData.Insert(0, new Department { Id = 0, DeptName = "Select" });
        //        return dropdownData;
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite("Error in CreateWGPModel GetDropdownDataDept method :" + ex.Message);
        //        return new List<Department>();
        //    }
        //}

        public void GetId()
        {
            try
            {
                string ddd = AppContext.BaseDirectory;
                var result = _db.WorkerGP.OrderBy(gp => gp.WorkerGPId).Select(gp => gp.WorkerGPId).LastOrDefault();
                if (result == null)
                {
                    WGPId = "WP000001";
                }
                else
                {
                    //WGPId = "WGP" + (Convert.ToInt32(result.Substring(4, result.Length - 4)) + 1).ToString("D3");
                    WGPId = GenerateNextId(result);
                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreateWGPModel GetId method :" + ex.Message);
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
                    NextInvNo = "WP00000" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 2)
                {
                    NextInvNo = "WP0000" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 3)
                {
                    NextInvNo = "WP000" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 4)
                {
                    NextInvNo = "WP00" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 5)
                {
                    NextInvNo = "WP0" + nextNo.ToString();
                }
                else if (nextNo.ToString().Length == 6)
                {
                    NextInvNo = nextNo.ToString();
                }
                return NextInvNo;
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreateWGPModel GenerateNextId method :" + ex.Message);
                return null;
            }
        }

        public async Task<IActionResult> OnPost(string chkPam, string chkifDeptHeadUn, string chkLunch, string chkSinthawatta, string chkHalfd, string chkMadu, string chkShrt, string chkOther)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    DateTime utcNow = DateTime.UtcNow;
                    TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                    DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                    //assigns year, month, day, hour, min, seconds
                    DateTime start = new DateTime(targetLocalTime.Year, targetLocalTime.Month, targetLocalTime.Day, 8, 1, 1);

                    DateTime end = new DateTime(targetLocalTime.Year, targetLocalTime.Month, targetLocalTime.Day, 17, 59, 59);


                    if (targetLocalTime < start || targetLocalTime > end)
                    {
                        _notify.Error("You Cannot Create GatePasses", 5);
                        //deptlist = GetDropdownDataDept();
                        workerlist = GetDropdownDataWorker();
                        Aprvlist = GetDropdownDataApprovalchange();
                        //workerepflist = GetDropdownDataWorkerEPF();
                        GetId();
                        return RedirectToPage("CreateWGP");
                    }
                    else if (WorkerGP.WrkId == 0)
                    {
                        _notify.Error("Please select Worker", 5);
                        workerlist = GetDropdownDataWorker();
                        Aprvlist = GetDropdownDataApprovalchange();
                        GetId();
                        return RedirectToPage("CreateWGP");
                    }
                    else if (chkOther == "true" && WorkerGP.Reason == null)
                    {
                        _notify.Error("Please give the Reason in the Description field", 5);
                        GetId();
                        Aprvlist = GetDropdownDataApprovalchange();
                        return RedirectToPage("CreateWGP");
                    }
                    else if (chkLunch == "false" && chkSinthawatta == "false" && chkHalfd == "false" && chkMadu == "false" && chkShrt == "false" && chkOther == "false" && chkPam == "false")
                    {
                        _notify.Error("Please Select Reason", 5);
                        GetId();
                        //deptlist = GetDropdownDataDept();
                        workerlist = GetDropdownDataWorker();
                        Aprvlist = GetDropdownDataApprovalchange();
                        //workerepflist = GetDropdownDataWorkerEPF();
                        return RedirectToPage("CreateWGP");
                    }
                    else
                    {
                        if(chkPam == "true")
                        {
                            WorkerGP.ChkPamunugama = true;
                        }
                        else 
                        {
                            WorkerGP.ChkPamunugama = false;
                        }

                        if (chkLunch == "true")
                        {
                            WorkerGP.ChkLunch = true;
                        }
                        else
                        {
                            WorkerGP.ChkLunch = false;
                        }

                        if (chkSinthawatta == "true")
                        {
                            WorkerGP.ChkSinthawatta = true;
                        }
                        else
                        {
                            WorkerGP.ChkSinthawatta = false;
                        }

                        if (chkHalfd == "true")
                        {
                            WorkerGP.ChkHalfd = true;
                        }
                        else
                        {
                            WorkerGP.ChkHalfd = false;
                        }

                        if (chkMadu == "true")
                        {
                            WorkerGP.ChkMadu = true;
                        }
                        else
                        {
                            WorkerGP.ChkMadu = false;
                        }

                        if (chkShrt == "true")
                        {
                            WorkerGP.ChkShrt = true;
                        }
                        else
                        {
                            WorkerGP.ChkShrt = false;
                        }

                        if (chkOther == "true")
                        {
                            WorkerGP.ChkOther = true;
                        }
                        else
                        {
                            WorkerGP.ChkOther = false;
                        }
                        if (chkifDeptHeadUn == "true")
                        {
                            WorkerGP.ChkifDeptHeadUn = true;
                            WorkerGP.AShod = "A";
                        }
                        else
                        {
                            WorkerGP.ChkifDeptHeadUn = false;
                            WorkerGP.AShod = null;
                        }

                        WorkerGP.DepId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                        WorkerGP.CreateDate = targetLocalTime;
                        WorkerGP.CreateUser = HttpContext.Session.GetString("UserId");

                        await _db.WorkerGP.AddAsync(WorkerGP);
                        await _db.SaveChangesAsync();
                        _notify.Success("Gate Pass Successfully Created", 3);
                        return RedirectToPage("CreateWGP");
                    }
                }
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreateWGPModel OnPost method :" + ex.Message);
            }
            return RedirectToPage("CreateWGP");
        }

    }
}
