 using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.AspNetCore.Http;

namespace GatePassManagementSystem.Pages.PersonalGP
{
    public class CreatePGPModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly Common cm;
        private readonly INotyfService _notify;

        public CreatePGPModel(ApplicationDbContext db, INotyfService notyf)//,IHttpContextAccessor httpContextAccessor, ISession session)
        {
            _db = db;
            cm = new Common();
            _notify = notyf;
        }

        [BindProperty]
        public Model.PersonalGP PersonalGP { get; set; }
        public Department Department { get; set; }
        //public List<Department> deplist { get; set; }
        public Model.User User { get; set; }
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public List<ApprovalChange> Aprvlist { get; set; }
        public string DeptHead { get; set; }
        public string DeptGm { get; set; }
        public string GPId { get; set; }
        public int currentUserId { get; set; }
        public string Fullname { get; set; }
        public string departName { get; set; } 
        public string EPFno { get; set; }
        public int deptId { get; set; }
        public int role { get; set; }
        public string time { get; set; }
        public string email { get; set; }

        public void OnGet()
        {
            try
            {
                Aprvlist = GetDropdownDataApprovalchange();

                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);
                //var userId = HttpContext.Session.GetString("UserId");
                //var userName = HttpContext.Session.GetString("UserName");
                GetId();
                //deplist = GetDropdownDataDept();
                currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                Fullname = HttpContext.Session.GetString("FullName");
                EPFno = HttpContext.Session.GetString("EPFNo");
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                email = HttpContext.Session.GetString("Email");
                role = Convert.ToInt32(HttpContext.Session.GetString("Roleid"));
                time = targetLocalTime.ToString();

                DeptHead = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Hod).FirstOrDefault();
                if(deptId == 14 || deptId == 16 || deptId == 17 || deptId == 18 || deptId == 19 || deptId == 21 || deptId == 22)
                {
                    DeptGm = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Dgm).FirstOrDefault();
                }
                else
                {
                    DeptGm = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Gm).FirstOrDefault();
                }
                
                departName = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.DeptName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreatePGPModel OnGet method :" + ex.Message);
            }
        }

        //public List<Department> GetDropdownDataDept()
        //{
        //    try
        //    {
        //        //var userDepartment = _db.User
        //        //.Where(user => user.Id == currentUserId)
        //        //.Select(user => user.DepartId)
        //        //.FirstOrDefault();

        //        //var dropdownData = _db.Department
        //        //    .Where(dept => dept.Id == userDepartment)
        //        //    .ToList();


        //        var dropdownData = _db.Department.ToList();
        //        dropdownData.Insert(0, new Department { Id = 0, DeptName = "Select" });
        //        return dropdownData;
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite("Error in CreatePGPModel GetDropdownData method :" + ex.Message);
        //        return new List<Department>();
        //    }
        //}

      
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
        //        //deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

        //        //var dropdownDataWorker = _db.Workers.FromSqlRaw<Workers>("SELECT * FROM Workers where DeptId=@depId", DeptId).ToList();
        //        var dropdownDataAprCh = _db.ApprovalChange.ToList();
        //        dropdownDataAprCh.Insert(0, new ApprovalChange { deptId = 0, FullName = "Select" });

        //        return dropdownDataAprCh;
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite("Error in GatePassListMgtPendingModel GetDropdownDataApprovalchange method :" + ex.Message);
        //        return new List<ApprovalChange>();
        //    }
        //}


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

        //public ActionResult EmailSend(Model.PersonalGP personalGP)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect("smtp.gmail.com");
        //        client.Authenticate("gatepass@westernpapersl.com", "Gat@#951$");

        //        var bodyBuilder = new BodyBuilder
        //        {
        //            HtmlBody = $"<p>{personalGP.PersonalGPId}</p><p>{personalGP.Description}</p>",
        //            TextBody = "{PersonalGP.PersonalGPId} \r\n {PersonalGP.Description}"
        //        };

        //        var message = new MimeMessage
        //        {
        //            Body = bodyBuilder.ToMessageBody()
        //        };

        //        message.From.Add(new MailboxAddress("Gate Pass System Notification", "gatepass@westernpapersl.com"));
        //        message.To.Add(new MailboxAddress("Testing01", email));//personalGP.Email
        //        message.Subject = "Gate Pass Details";
        //        client.Send(message);

        //        client.Disconnect(true);
        //    }
        //    return RedirectToPage("CreatePGP");
        //}

        public async Task<IActionResult> OnPost(string chkCusVisit, string chkifDeptHeadUn, string chkLunch, string chkSinthawatta, string chkHalfd, string chkMadu, string chkShrt, string chkOther)
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
                        Aprvlist = GetDropdownDataApprovalchange();
                        GetId();
                        return RedirectToPage("CreatePGP");
                    }
                    else if (chkLunch == "false" && chkSinthawatta == "false" && chkHalfd == "false" && chkMadu == "false" && chkShrt == "false" && chkOther == "false" && chkCusVisit == "false")
                    {
                        _notify.Error("Please Select Reason", 5);
                        GetId();
                        Aprvlist = GetDropdownDataApprovalchange();
                        return RedirectToPage("CreatePGP");
                    }
                    else if(chkOther == "true" && PersonalGP.Description == null)
                    {
                        _notify.Error("Please give the Reason in the Description field", 5);
                        GetId();
                        Aprvlist = GetDropdownDataApprovalchange();
                        return RedirectToPage("CreatePGP");
                    }
                    else
                    {
                        
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


                        PersonalGP.DepId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                        PersonalGP.Reason = "NULL";
                        PersonalGP.CreateDate = targetLocalTime;
                        PersonalGP.CreateUser = HttpContext.Session.GetString("FullName");
                        PersonalGP.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

                        if (role == 5 || role == 4 || ((chkifDeptHeadUn == "true" && role == 6) || ((deptId == 10 || deptId == 8 || deptId == 9) && role == 6)))
                        {
                            PersonalGP.AShod = "A";
                            if (chkifDeptHeadUn == "true")
                            {
                                PersonalGP.ChkifDeptHeadUn = true;
                            }
                            else
                            {
                                PersonalGP.ChkifDeptHeadUn = false;
                            }
                        }
                        else
                        {
                            PersonalGP.AShod = null;
                            
                        }

                        await _db.PersonalGP.AddAsync(PersonalGP);
                        await _db.SaveChangesAsync();

                        _notify.Success("Gate Pass  Successfully Created", 3);
                        return RedirectToPage("CreatePGP");
                    }
                }
            }
            catch (Exception ex)
            {
                cm.Logwrite($"Error in CreateWGPModel OnPost method: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
            return RedirectToPage("CreatePGP");
        }
    }
}
