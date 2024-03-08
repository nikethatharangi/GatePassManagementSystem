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
using MailKit.Security;
using System.Net.Mail;
using System.Net;

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
        public int ChngAprl { get; set; }
        public string hodmail { get; set; }
        public string hodName { get; set; }

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
                DeptGm = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Gm).FirstOrDefault();

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

        public ActionResult EmailSendToHodApprover(Model.PersonalGP personalGP)
        {
            currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
            role = Convert.ToInt32(HttpContext.Session.GetString("Roleid"));

            string userEmail = _db.User.Where(u => u.Id == currentUserId).Select(u => u.Email).FirstOrDefault();
            string fullname = _db.User.Where(u => u.Id == currentUserId).Select(u => u.FullName).FirstOrDefault();

            bool depheadUn = _db.PersonalGP.Where(u => u.Id == currentUserId).Select(u => u.ChkifDeptHeadUn).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (deptId == 1 || deptId == 2 || deptId == 3 || deptId == 4 || deptId == 5 || deptId == 6 || deptId == 7 || deptId == 11 || deptId == 13 || deptId == 14 || deptId == 16 || deptId == 17 || deptId == 18 || deptId == 21 || deptId == 22 || deptId == 23 || deptId == 24 || deptId == 25 || deptId == 26 || deptId == 27 || deptId == 28)
            {

                if (depheadUn == true || role == 5 || role == 4)
                {

                }
                else
                {
                    //int uid = _db.User.Where(u => u.DepartId == deptId && u.RoleId == 5).Select(u => u.Id).FirstOrDefault();
                    hodmail = _db.User.Where(u => u.DepartId == deptId && u.RoleId == 5).Select(u => u.Email).FirstOrDefault();
                    hodName = _db.User.Where(u => u.DepartId == deptId && u.RoleId == 5).Select(u => u.FullName).FirstOrDefault();

                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                    client.Host = "220.247.247.28"; //Set your smtp host address  
                    client.Port = 25; //Set your smtp port address  
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new NetworkCredential("gatepass@westernpapersl.com", "Gat@#951$"); //account name and password  
                                                                                                            //client.EnableSsl = true; // Set SSL = true 
                    MailMessage message = new MailMessage();
                    //client.UseDefaultCredentials = false;

                    message.From = new MailAddress("gatepass@westernpapersl.com"); // Sender address  
                    message.Subject = "WPI GATE PASS SYSTEM";

                    message.IsBodyHtml = true; // HTML email  

                    //message.To.Add("niketha@westernpapersl.com");

                    //Cc list
                    //message.CC.Add("yohan@westernpapersl.com");

                    message.To.Add(hodmail);
                    message.Body = "Dear " + hodName + "," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + fullname + "</b>" + "<br />" + "Thank you.";

                    client.Send(message);
                }

            }

            return RedirectToPage("CreatePGP");
        }


        public ActionResult EmailSendToMangApprover(Model.PersonalGP personalGP)
        {
            currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
            ChngAprl = _db.PersonalGP.Where(u => u.PersonalGPId == PersonalGP.PersonalGPId).Select(u => u.ChApprvlId).FirstOrDefault();

            string userEmail = _db.User.Where(u => u.Id == currentUserId).Select(u => u.Email).FirstOrDefault();
            string fullname = _db.User.Where(u => u.Id == currentUserId).Select(u => u.FullName).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = "220.247.247.28"; //Set your smtp host address  
            client.Port = 25; //Set your smtp port address  
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("gatepass@westernpapersl.com", "Gat@#951$"); //account name and password  
                                                                                                    //client.EnableSsl = true; // Set SSL = true 
            MailMessage message = new MailMessage();
            //client.UseDefaultCredentials = false;

            message.From = new MailAddress("gatepass@westernpapersl.com"); // Sender address  
            message.Subject = "WPI GATE PASS SYSTEM";

            message.IsBodyHtml = true; // HTML email  

            //message.To.Add("niketha@westernpapersl.com");

            //Cc list
            //message.CC.Add("yohan@westernpapersl.com");

            if (ChngAprl == 0)
            {
                if (deptId == 8 || deptId == 9) //mr. Ruwan
                {
                    //To list
                    message.To.Add("ruwan@westernpapersl.com");
                    message.Body = "Dear Mr.Ruwan," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + PersonalGP.CreateUser + "</b>" + "<br />" + "Thank you.";
                    client.Send(message);
                }
                else if (deptId == 10) //mr. Sugath
                {
                    //To list
                    message.To.Add("sugath@weternpapersl.com");
                    message.Body = "Dear Mr.Sugath," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + PersonalGP.CreateUser + "</b>" + "<br />" + "Thank you.";
                }
                else if (deptId == 12) //mr. Geethanga
                {
                    //To list
                    message.To.Add("managerhr@westernpapersl.com");
                    message.Body = "Dear Mr.Geethanga," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + PersonalGP.CreateUser + "</b>" + "<br />" + "Thank you.";
                }
                else if (deptId == 19) //mr. Wijekoon
                {
                    //To list
                    message.To.Add("em@westernpapersl.com");
                    message.Body = "Dear Mr.Wijekoon," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + PersonalGP.CreateUser + "</b>" + "<br />" + "Thank you.";
                }
                else if (deptId == 22) // mrs. kumudu
                {
                    //To list
                    message.To.Add("managerr&d@westernpapersl.com");
                    message.Body = "Dear Mr.Kumudu," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + PersonalGP.CreateUser + "</b>" + "<br />" + "Thank you.";
                }
                else if (deptId == 15 || deptId == 20) // mr. Rohan
                {
                    //To list
                    message.To.Add("chaminda@wesrernpapersl.com");
                    message.Body = "Dear Mr.Rohan," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + PersonalGP.CreateUser + "</b>" + "<br />" + "Thank you.";
                }

            }
            else if (ChngAprl == 10) //mr.Sugath
            {
                //To list
                message.To.Add("sugath@weternpapersl.com");
                message.Body = "Dear Mr.Sugath," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + PersonalGP.CreateUser + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 6) //mr. dharampriya
            {
                //To list
                message.To.Add("dharmapriya@westernpapersl.com");
                message.Body = "Dear Mr.Dharmapriya," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + PersonalGP.CreateUser + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 7) // mr. thusitha
            {
                //To list
                message.To.Add("ceo@westernpapersl.com");
                message.Body = "Dear Mr.Thusitha," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + PersonalGP.CreateUser + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 8) //mr. ruwan
            {
                //To list
                message.To.Add("ruwan@westernpapersl.com");
                message.Body = "Dear Mr.Ruwan," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + PersonalGP.CreateUser + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 15) //mr. rohan
            {
                //To list
                message.To.Add("chaminda@wesrernpapersl.com");
                message.Body = "Dear Mr.Rohan," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + PersonalGP.CreateUser + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 26) //mr.damith
            {
                //To list
                message.To.Add("damith@westernpapersl.com");
                message.Body = "Dear Mr.Damith," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + PersonalGP.CreateUser + "</b>" + "<br />" + "Thank you.";
            }

            return RedirectToPage("CreatePGP");
        }


        public ActionResult EmailSend(Model.PersonalGP personalGP)
        {
            currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            string userEmail = _db.User.Where(u => u.Id == currentUserId).Select(u => u.Email).FirstOrDefault();
            string fullname = _db.User.Where(u => u.Id == currentUserId).Select(u => u.FullName).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(userEmail == "None")
            {

            }
            else
            {
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Host = "220.247.247.28"; //Set your smtp host address  
                client.Port = 25; //Set your smtp port address  
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("gatepass@westernpapersl.com", "Gat@#951$"); //account name and password  
                                                                                                          //client.EnableSsl = true; // Set SSL = true 
                                                                                                          //client.UseDefaultCredentials = false;
                MailMessage message = new MailMessage();

                //To list
                message.To.Add(userEmail);
                //message.To.Add("niketha@westernpapersl.com");

                //Cc list
                //message.CC.Add("yohan@westernpapersl.com");

                message.From = new MailAddress("gatepass@westernpapersl.com"); // Sender address  
                message.Subject = "WPI GATE PASS SYSTEM";

                message.IsBodyHtml = true; // HTML email  

                message.Body = "Hello " + fullname + "," + "<br /><h4>You have successfully created a Gate Pass !!</h4><br />" + "Your gate pass ID :" + "<b>" + PersonalGP.PersonalGPId + "</b>" + "<br />" + "Thank you.";

                client.Send(message);

            }

            return RedirectToPage("CreatePGP");
        }

        public async Task<IActionResult> OnPost(string chkPam, string chkOfficial, string chkCusVisit, string chkifDeptHeadUn, string chkLunch, string chkSinthawatta, string chkHalfd, string chkMadu, string chkShrt, string chkOther)
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
                    else if (chkLunch == "false" && chkSinthawatta == "false" && chkHalfd == "false" && chkMadu == "false" && chkShrt == "false" && chkOther == "false" && chkCusVisit == "false" && chkPam == "false" && chkOfficial == "false")
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
                        if(chkOfficial == "true")
                        {
                            PersonalGP.ChkOfficialwork = true;
                        }
                        else
                        {
                            PersonalGP.ChkOfficialwork = false;
                        }

                        if(chkPam == "true")
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


                        PersonalGP.DepId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                        PersonalGP.Reason = "NULL";
                        PersonalGP.CreateDate = targetLocalTime;
                        PersonalGP.CreateUser = HttpContext.Session.GetString("FullName");
                        PersonalGP.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

                        if (role == 5 || role == 4 || ((chkifDeptHeadUn == "true" && role == 6) || ((deptId == 10 || deptId == 8 || deptId == 9 || deptId == 15 || deptId == 20 || deptId == 19 || deptId == 22 || deptId == 12) && role == 6)))
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

                        _notify.Success("Gate Pass Successfully Created", 3);

                        EmailSendToMangApprover(PersonalGP);
                        EmailSendToHodApprover(PersonalGP);
                        EmailSend(PersonalGP);
                        
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
