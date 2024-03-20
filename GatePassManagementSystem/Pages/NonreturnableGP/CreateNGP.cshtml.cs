using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        public List<Department> DeptList { get; set; }
        public List<NonReturnItemDsc> NonReturnItemDscsl { get; set; }
        public string DeptHead { get; set; }
        public string DeptGm { get; set; }
        public int currentUserId { get; set; }
        public string Fullname { get; set; }
        public string departName { get; set; }
        public int deptId { get; set; }
        public int role { get; set; }
        public string GPId { get; set; }
        public string hodmail { get; set; }
        public string hodName { get; set; }
        public int ChngAprl { get; set; }


        public void OnGet()
        {
            Aprvlist = GetDropdownDataApprovalchange();
            DeptList = GetDropdownDataDepartments();

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

        public List<Department> GetDropdownDataDepartments()
        {
            try
            {
                var dropdownDataDept = _db.Department.ToList();
                dropdownDataDept.Insert(0, new Department { Id = 0, DeptName = "Select" });

                return dropdownDataDept;
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in CreateNGPModel GetDropdownDataApprovalchange method :" + ex.Message);
                return new List<Department>();
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

        public ActionResult EmailSendToHodApprover(Model.NonReturnableGP nonReturnableGP)
        {
            currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
            role = Convert.ToInt32(HttpContext.Session.GetString("Roleid"));

            string userEmail = _db.User.Where(u => u.Id == currentUserId).Select(u => u.Email).FirstOrDefault();
            string fullname = _db.User.Where(u => u.Id == currentUserId).Select(u => u.FullName).FirstOrDefault();

            bool depheadUn = _db.NonReturnableGP.Where(u => u.Id == currentUserId).Select(u => u.ChkifDeptHeadUn).FirstOrDefault();

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
                    message.Body = "Dear " + hodName + "," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + fullname + "</b>" + "<br />" + "Thank you.";

                    client.Send(message);
                }

            }

            return RedirectToPage("CreateNGP");
        }


        public ActionResult EmailSendToMangApprover(Model.NonReturnableGP nonReturnableGP)
        {
            currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
            ChngAprl = _db.NonReturnableGP.Where(u => u.NonReturnableGPId == NonReturnableGP.NonReturnableGPId).Select(u => u.ChApprvlId).FirstOrDefault();

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
                    message.Body = "Dear Mr.Ruwan," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + NonReturnableGP.CreateUser + "</b>" + "<br />" + "Thank you.";
                    client.Send(message);
                }
                else if (deptId == 10) //mr. Sugath
                {
                    //To list
                    message.To.Add("sugath@weternpapersl.com");
                    message.Body = "Dear Mr.Sugath," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + NonReturnableGP.CreateUser + "</b>" + "<br />" + "Thank you.";
                }
                else if (deptId == 12) //mr. Geethanga
                {
                    //To list
                    message.To.Add("managerhr@westernpapersl.com");
                    message.Body = "Dear Mr.Geethanga," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + NonReturnableGP.CreateUser + "</b>" + "<br />" + "Thank you.";
                }
                else if (deptId == 19) //mr. Wijekoon
                {
                    //To list
                    message.To.Add("em@westernpapersl.com");
                    message.Body = "Dear Mr.Wijekoon," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + NonReturnableGP.CreateUser + "</b>" + "<br />" + "Thank you.";
                }
                else if (deptId == 22) // mrs. kumudu
                {
                    //To list
                    message.To.Add("managerr&d@westernpapersl.com");
                    message.Body = "Dear Mr.Kumudu," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + NonReturnableGP.CreateUser + "</b>" + "<br />" + "Thank you.";
                }
                else if (deptId == 15 || deptId == 20) // mr. Rohan
                {
                    //To list
                    message.To.Add("chaminda@wesrernpapersl.com");
                    message.Body = "Dear Mr.Rohan," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + NonReturnableGP.CreateUser + "</b>" + "<br />" + "Thank you.";
                }

            }
            else if (ChngAprl == 10) //mr.Sugath
            {
                //To list
                message.To.Add("sugath@weternpapersl.com");
                message.Body = "Dear Mr.Sugath," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + NonReturnableGP.CreateUser + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 6) //mr. dharampriya
            {
                //To list
                message.To.Add("dharmapriya@westernpapersl.com");
                message.Body = "Dear Mr.Dharmapriya," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + NonReturnableGP.CreateUser + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 7) // mr. thusitha
            {
                //To list
                message.To.Add("ceo@westernpapersl.com");
                message.Body = "Dear Mr.Thusitha," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + NonReturnableGP.CreateUser + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 8) //mr. ruwan
            {
                //To list
                message.To.Add("ruwan@westernpapersl.com");
                message.Body = "Dear Mr.Ruwan," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + NonReturnableGP.CreateUser + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 15) //mr. rohan
            {
                //To list
                message.To.Add("chaminda@wesrernpapersl.com");
                message.Body = "Dear Mr.Rohan," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + NonReturnableGP.CreateUser + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 26) //mr.damith
            {
                //To list
                message.To.Add("damith@westernpapersl.com");
                message.Body = "Dear Mr.Damith," + "<br />You recieved a new Good Gate Pass Request" + " from :" + "<b>" + NonReturnableGP.CreateUser + "</b>" + "<br />" + "Thank you.";
            }

            return RedirectToPage("CreateNGP");
        }


        public ActionResult EmailSend(Model.NonReturnableGP nonReturnableGP)
        {
            currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            string userEmail = _db.User.Where(u => u.Id == currentUserId).Select(u => u.Email).FirstOrDefault();
            string fullname = _db.User.Where(u => u.Id == currentUserId).Select(u => u.FullName).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (userEmail == "None")
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

                message.Body = "Hello " + fullname + "," + "<br /><h4>You have successfully created a Good Gate Pass !!</h4><br />" + "Your Good Gate pass ID :" + "<b>" + NonReturnableGP.NonReturnableGPId + "</b>" + "<br />" + "Thank you.";

                client.Send(message);

            }

            return RedirectToPage("CreateNGP");
        }


        public async Task<IActionResult> OnPost(List<NonReturnItemDsc> items,string chkifDeptHeadUn, string chkPamunuFrom, string chkMaduFrom, string chkSinthaFrom, string chkPamunuTo, string chkMaduTo, string chkSinthaTo)
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
                    DeptList = GetDropdownDataDepartments();
                    return RedirectToPage("CreateNGP");
                }
                if (chkPamunuFrom == "true" && chkPamunuTo == "true" || chkMaduFrom == "true" && chkMaduTo == "true" || chkSinthaFrom == "true" && chkSinthaTo == "true")
                {
                    _notify.Error("From Location and To Location are same", 5);
                    GetId();
                    Aprvlist = GetDropdownDataApprovalchange();
                    DeptList = GetDropdownDataDepartments();
                    return RedirectToPage("CreateNGP");
                }
                else if (chkPamunuTo == "false" && chkMaduTo == "false" && chkSinthaTo == "false")
                {
                    _notify.Error("Please Select To Location", 5);
                    GetId();
                    Aprvlist = GetDropdownDataApprovalchange();
                    DeptList = GetDropdownDataDepartments();
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

                    if (role == 5 || role == 4 || ((chkifDeptHeadUn == "true" && role == 6) || ((deptId == 10 || deptId == 8 || deptId == 9 || deptId == 20 || deptId == 19 || deptId == 22 || deptId == 12) && role == 6)))
                    {
                        NonReturnableGP.AShod = "A";
                        if (chkifDeptHeadUn == "true")
                        {
                            NonReturnableGP.ChkifDeptHeadUn = true;
                        }
                        else
                        {
                            NonReturnableGP.ChkifDeptHeadUn = false;
                        }
                    }
                    else
                    {
                        NonReturnableGP.AShod = null;

                    }

                    //await _db.NonReturnItemDsc.AddAsync(NonReturnItemDsc);
                    //await _db.SaveChangesAsync();


                    foreach (var item in items)
                    {
                        item.NonGPId = NonReturnableGP.NonReturnableGPId;
                        _db.NonReturnItemDsc.Add(item);
                        Aprvlist = GetDropdownDataApprovalchange();
                        DeptList = GetDropdownDataDepartments();
                        deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                        role = Convert.ToInt32(HttpContext.Session.GetString("Roleid"));
                    }

                    _db.SaveChanges();
                     await _db.NonReturnableGP.AddAsync(NonReturnableGP);
                    await _db.SaveChangesAsync();

                    _notify.Success("Non-Returnable Gate Pass Successfully Created", 3);
                    //return new JsonResult("Data saved successfully");
                    Aprvlist = GetDropdownDataApprovalchange();
                    DeptList = GetDropdownDataDepartments();

                    EmailSendToMangApprover(NonReturnableGP);
                    EmailSendToHodApprover(NonReturnableGP);
                    EmailSend(NonReturnableGP);

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
