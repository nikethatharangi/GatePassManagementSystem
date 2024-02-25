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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace GatePassManagementSystem.Pages.PersonalGP
{
    public class ViewPendingGPModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private Common cm;
        private readonly INotyfService _notify;
        public static List<Model.PersonalGP> PersonalGP { get; set; }
        public static List<Model.WorkerGP> WorkerGP { get; set; }
        [BindProperty]
        public Model.PersonalGP PersonalGPB { get; set; }
        [BindProperty]
        public Model.WorkerGP WorkerGPB { get; set; }
        public ViewPendingGPModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            cm = new Common();
            _notify = notyf;
        }
        public ApplicationDbContext Db => _db;
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public IEnumerable<Model.WorkerGP> WorkerGPs { get; set; }
        public int Uid { get; set; }
        public string roleId { get; set; }
        public int deptId { get; set; }
        public string Fullname { get; set; }
        public string DeptHead { get; set; }
        public string deptName { get; set; }
        public string DeptGm { get; set; }
        public int currentUserId { get; set; }
        public string GPNumber { get; set; }
        public int ChngAprl { get; set; }
        public string Createuser { get; set; }

        [BindProperty]
        public string RejctReason { get; set; }

        public async Task OnGet()
        {
            try
            {
                roleId = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                deptName = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.DeptName).FirstOrDefault();
                DeptHead = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Hod).FirstOrDefault();
                DeptGm = _db.Department.Where(gp => gp.Id == deptId).Select(gp => gp.Gm).FirstOrDefault();

                DateTime utcNow = DateTime.UtcNow;
                    TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                    DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                if (Uid == 4) //Mr.Darmapriya
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date && gp.AShod == "A" && gp.ASdgm == null && (((gp.DepId == 1 || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 4 || gp.DepId == 5 || gp.DepId == deptId || gp.DepId == 6  || gp.DepId == 11 || gp.DepId == 13 || gp.DepId == 24 || gp.DepId == 27 || gp.DepId == 28) && gp.ChApprvlId == 0) || (gp.ChApprvlId == 6 && gp.AShod == "A"))).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date && gp.AShod == "A" && gp.ASdgm == null && gp.ChkifDeptHeadUn == true && (((gp.DepId == 1 || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 4 || gp.DepId == 5 || gp.DepId == deptId || gp.DepId == 6 || gp.DepId == 11 || gp.DepId == 13 || gp.DepId == 24) && gp.ChAprlId == 0) || (gp.ChAprlId == 6 && gp.AShod == "A"))).ToListAsync();
                    //PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod == "A" && gp.UserId != Uid && (gp.DepId == deptId || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 5 || gp.DepId == 6) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync(); 
                }
                else if (Uid == 31) //Mr. Ruwan
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && (((gp.DepId == 8 || gp.DepId == 9 || gp.DepId == 25) && gp.ChApprvlId == 0) || (gp.ChApprvlId == 8 && gp.AShod == "A")) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.ChkifDeptHeadUn == true &&  (((gp.DepId == 8 || gp.DepId == 9 || gp.DepId == 25) && gp.ChAprlId == 0) || (gp.ChAprlId == 8 && gp.AShod == "A")) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    //PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod == null && gp.UserId != Uid && gp.DepId == 8 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Where(gp => gp.ASdgm == null && gp.AShod == null && gp.UserId != Uid && gp.DepId == 9 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 172) //Mr. Thusitha
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && ((gp.DepId == 7 && gp.ChApprvlId == 0) || (gp.ChApprvlId == 7 && gp.AShod == "A")) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.ChkifDeptHeadUn == true && ((gp.DepId == 7 && gp.ChAprlId == 0) || (gp.ChAprlId == 7 && gp.AShod == "A")) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    //PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod == null && gp.UserId != Uid && gp.DepId == 8 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Where(gp => gp.ASdgm == null && gp.AShod == null && gp.UserId != Uid && gp.DepId == 9 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 145) // Mr. Damith
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && ((gp.DepId == deptId || gp.DepId == 14 || gp.DepId == 16 || gp.DepId == 17 || gp.DepId == 18 || gp.DepId == 21 && gp.ChApprvlId == 0) || (gp.ChApprvlId == 26))  && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.ChkifDeptHeadUn == true && ((gp.DepId == deptId || gp.DepId == 14 || gp.DepId == 16 || gp.DepId == 17 || gp.DepId == 18 || gp.DepId == 21) || (gp.ChAprlId == 26) && gp.ChAprlId == 0)  && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 92) // Mr. Vajira
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.AShod == null && (gp.DepId == deptId || gp.DepId == 27 && gp.ChApprvlId == 0) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.AShod == null  && (gp.DepId == deptId || gp.DepId == 27 && gp.ChAprlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if(Uid == 96) // mr. Wijekoon
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && (gp.DepId == deptId || gp.DepId == 19 && gp.ChApprvlId == 0) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.ASdgm == null && (gp.DepId == deptId || gp.DepId == 19 && gp.ChAprlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 68) // mr. Geethanga
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && (gp.DepId == 12 && gp.ChApprvlId == 0) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.ASdgm == null && (gp.DepId == 12 && gp.ChAprlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (Uid == 109) // mrs. Kumudu
                {
                    var query1 = await _db.PersonalGP // packing
                        .Where(gp => gp.AShod == "A" && gp.ASdgm == null && (gp.DepId == deptId || gp.DepId == 18 && gp.ChApprvlId == 0) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();

                    var query2 = await _db.PersonalGP  // QA dept
                        .Where(gp => gp.ASdgm == null && (gp.DepId == deptId || gp.DepId == 22 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();

                    PersonalGPs = query1.Union(query2).ToList();

                    var query3 = await _db.WorkerGP // packing
                        .Where(gp => gp.AShod == "A" && gp.ASdgm == null && (gp.DepId == deptId || gp.DepId == 18 && gp.ChAprlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();

                    var query4 = await _db.WorkerGP  // QA dept
                        .Where(gp => gp.ASdgm == null && (gp.DepId == deptId || gp.DepId == 22 && gp.ChAprlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();

                    WorkerGPs = query3.Union(query4).ToList();

                }
                else if (Uid == 90) // Mr. Rohan
                {
                    var query1 = await _db.PersonalGP // transport, QMS dept
                        .Where(gp => gp.ASdgm == null && ((gp.DepId == 15 || gp.DepId == 20 && gp.ChApprvlId == 0) || (gp.ChApprvlId == 15 || gp.DepId == 20 && gp.AShod == "A")) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date)
                        .ToListAsync();

                    var query2 = await _db.PersonalGP  // stores dept
                        .Where(gp => gp.ASdgm == null && (gp.DepId == 23  && gp.ChApprvlId == 0) && gp.AShod == "A" && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date)
                        .ToListAsync();

                    PersonalGPs = query1.Union(query2).ToList();

                    var query3 = await _db.WorkerGP // transport, QMS dept workers
                        .Where(gp => gp.ASdgm == null && gp.ChkifDeptHeadUn == true && ((gp.DepId == 15 || gp.DepId == 20 && gp.ChAprlId == 0) || (gp.ChAprlId == 15 || gp.DepId == 20 && gp.AShod == "A")) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date)
                        .ToListAsync();

                    var query4 = await _db.WorkerGP  // stores dept workers
                        .Where(gp => gp.ASdgm == null && gp.ChkifDeptHeadUn == true && (gp.DepId == 23 || gp.ChAprlId == 15 && gp.ChAprlId == 0) && gp.AShod == "A" && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date)
                        .ToListAsync();

                    WorkerGPs = query3.Union(query4).ToList();
                }
                else if (roleId == "1")
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASmd == null && ((gp.DepId == deptId && gp.ChApprvlId == 0) || gp.ChApprvlId == 10 && gp.AShod == "A")  && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.ASmd == null && ((gp.DepId == deptId && gp.ChAprlId == 0) || gp.ChAprlId == 10 && gp.AShod == "A") && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (roleId == "3")
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => (gp.DepId == deptId && gp.ChApprvlId == 0) && gp.ASgm == null && gp.ASdgm == "A" && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => (gp.DepId == deptId && gp.ChAprlId == 0) && gp.ASgm == null && gp.ASdgm == "A" && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (roleId == "4")
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => (gp.DepId == deptId && gp.ChApprvlId == 0) && gp.ASdgm == null && gp.AShod == "A"  && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => (gp.DepId == deptId && gp.ChAprlId == 0) && gp.ASdgm == null && gp.AShod == "A" && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }
                else if (roleId == "5")
                {
                    PersonalGPs = await _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.AShod == null && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    WorkerGPs = await _db.WorkerGP.Where(gp => gp.DepId == deptId && gp.AShod == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                }

                //// Fetch data from WorkerGP table
                //workerGPs = await _db.WorkerGP
                //    .Where(gp => gp.DepId == deptId && gp.ASmd == null && gp.ASgm != null)
                //    .ToListAsync();

                //// Combine the lists
                //PersonalGPs = personalGPs.Cast<Model.PersonalGP>().Union(workerGPs.Cast<Model.PersonalGP>()).ToList();
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewPendingGPModel OnGet method :" + ex.Message);
            }
        }

        public ActionResult EmailSendToManagApprover(int Id)
        {
            int uid = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.UserId).FirstOrDefault();
            int deptid = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.DepId).FirstOrDefault();

            ChngAprl = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.ChApprvlId).FirstOrDefault();
            GPNumber = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.PersonalGPId).FirstOrDefault();
            Createuser = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.CreateUser).FirstOrDefault();
            

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

            //Cc list
            //message.CC.Add("yohan@westernpapersl.com");

            if (ChngAprl == 0)
            {
                if (deptId == 27 || deptId == 28 || deptId == 13 || deptId == 11 || deptId == 1 || deptId == 2 || deptId == 3 || deptId == 4 || deptId == 5 || deptId == 6) //mr. Dharmapriya
                {
                    //To list
                    message.To.Add("dharmapriya@westernpapersl.com");
                    message.Body = "Dear Mr.Dharmapriya," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + Createuser + "</b>" + " Gate Pass No. :" + "<b>" + GPNumber + "</b>" + "<br />" + "Thank you.";
                }
                //else if (deptId == 7) //mr. Thusitha
                //{
                //    //To list
                //    message.To.Add("niketha@westernpapersl.com");
                //    message.Body = "Dear Mr.Thusitha," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + Createuser + "</b>" + " Gate Pass No. :" + "<b>" + GPNumber + "</b>" + "<br />" + "Thank you.";
                //}
                else if (deptId == 14 || deptId == 16 || deptId == 21 || deptId == 26) //mr.Damith 
                {
                    //To list
                    message.To.Add("damith@westernpapersl.com");
                    message.Body = "Dear Mr.Damith," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + Createuser + "</b>" + " Gate Pass No. :" + "<b>" + GPNumber + "</b>" + "<br />" + "Thank you.";
                }
                else if (deptId == 18) // mrs. kumudu
                {
                    //To list
                    message.To.Add("managerr&d@westernpapersl.com");
                    message.Body = "Dear Mr.Kumudu," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + Createuser + "</b>" + " Gate Pass No. :" + "<b>" + GPNumber + "</b>" + "<br />" + "Thank you.";
                }
                else if (deptId == 23) // mr. Rohan
                {
                    //To list
                    message.To.Add("chaminda@wesrernpapersl.com");
                    message.Body = "Dear Mr.Rohan," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + Createuser + "</b>" + " Gate Pass No. :" + "<b>" + GPNumber + "</b>" + "<br />" + "Thank you.";
                }
            }
            else if (ChngAprl == 10) //mr.Sugath
            {
                //To list
                message.To.Add("sugath@weternpapersl.com");
                message.Body = "Dear Mr.Sugath," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + Createuser + "</b>" + " Gate Pass No. :" + "<b>" + GPNumber + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 6) //mr. dharampriya
            {
                //To list
                message.To.Add("dharmapriya@westernpapersl.com");
                message.Body = "Dear Mr.Dharmapriya," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + Createuser + "</b>" + " Gate Pass No. :" + "<b>" + GPNumber + "</b>" + "<br />" + "Thank you.";
            }
            //else if (ChngAprl == 7) // mr. thusitha
            //{
            //    //To list
            //    message.To.Add("niketha@westernpapersl.com");
            //    message.Body = "Dear Mr.Thusitha," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + Createuser + "</b>" + " Gate Pass No. :" + "<b>" + GPNumber + "</b>" + "<br />" + "Thank you.";
            //}
            else if (ChngAprl == 8) //mr. ruwan
            {
                //To list
                message.To.Add("ruwan@westernpapersl.com");
                message.Body = "Dear Mr.Ruwan," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + Createuser + "</b>" + " Gate Pass No. :" + "<b>" + GPNumber + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 15) //mr. rohan
            {
                //To list
                message.To.Add("chaminda@wesrernpapersl.com");
                message.Body = "Dear Mr.Rohan," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + Createuser + "</b>" + " Gate Pass No. :" + "<b>" + GPNumber + "</b>" + "<br />" + "Thank you.";
            }
            else if (ChngAprl == 26) //mr.damith
            {
                //To list
                message.To.Add("damith@westernpapersl.com");
                message.Body = "Dear Mr.Damith," + "<br />You recieved a new Gate Pass Request" + " from :" + "<b>" + Createuser + "</b>" + " Gate Pass No. :" + "<b>" + GPNumber + "</b>" + "<br />" + "Thank you.";
            }

            client.Send(message);

            return RedirectToPage("CreatePGP");
        }

        public ActionResult EmailSendFirstApproved(int Id)
        {
            currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int uid = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.UserId).FirstOrDefault();
            int deptid = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.DepId).FirstOrDefault();
            string hodname = _db.Department.Where(u => u.Id == deptid).Select(u => u.Hod).FirstOrDefault();
            string Email = _db.User.Where(u => u.Id == uid).Select(u => u.Email).FirstOrDefault();
            string fullname = _db.User.Where(u => u.Id == uid).Select(u => u.FullName).FirstOrDefault();

            GPNumber = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.PersonalGPId).FirstOrDefault();

            if (Email == "None")
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
                message.To.Add(Email);
                //message.To.Add("niketha@westernpapersl.com");

                //Cc list
                //message.CC.Add("yohan@westernpapersl.com");

                message.From = new MailAddress("gatepass@westernpapersl.com"); // Sender address  
                message.Subject = "WPI GATE PASS SYSTEM";

                message.IsBodyHtml = true; // HTML email  

                message.Body = "Dear " + fullname + "," + "<br />Your Gate Pass is Approved by First Approver. Gate Pass No :" + "<b>" + GPNumber + "</b>" + " Approved by " + "<b>" + hodname + "</b>" + "<br />" + "Thank you.";

                client.Send(message);

            }

            return RedirectToPage("ViewPendingGP");
        }

        public ActionResult EmailSendSecondApproved(int Id)
        {
            currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int uid = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.UserId).FirstOrDefault();

            int Aprchng = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.ChApprvlId).FirstOrDefault();

            if(Aprchng == 0)
            {
                int deptid = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.DepId).FirstOrDefault();
                string gmname = _db.Department.Where(u => u.Id == deptid).Select(u => u.Gm).FirstOrDefault();

                string Email = _db.User.Where(u => u.Id == uid).Select(u => u.Email).FirstOrDefault();
                string fullname = _db.User.Where(u => u.Id == uid).Select(u => u.FullName).FirstOrDefault();

                GPNumber = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.PersonalGPId).FirstOrDefault();


                if (Email == "None")
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
                    message.To.Add(Email);
                    //message.To.Add("niketha@westernpapersl.com");

                    //Cc list
                    //message.CC.Add("yohan@westernpapersl.com");

                    message.From = new MailAddress("gatepass@westernpapersl.com"); // Sender address  
                    message.Subject = "WPI GATE PASS SYSTEM";

                    message.IsBodyHtml = true; // HTML email  

                    message.Body = "Dear " + fullname + "," + "<br />Your Gate Pass is Approved by Management Approver. Gate Pass No :" + "<b>" + GPNumber + "</b>" + " Approved By " + "<b>" + gmname + "</b>" + "<br />" + "Thank you.";

                    client.Send(message);

                }
            }
            else
            {
                int deptid = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.ChApprvlId).FirstOrDefault();
                string gmname = _db.Department.Where(u => u.Id == deptid).Select(u => u.Gm).FirstOrDefault();

                string Email = _db.User.Where(u => u.Id == uid).Select(u => u.Email).FirstOrDefault();
                string fullname = _db.User.Where(u => u.Id == uid).Select(u => u.FullName).FirstOrDefault();

                GPNumber = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.PersonalGPId).FirstOrDefault();


                if (Email == "None")
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
                    message.To.Add(Email);
                    //message.To.Add("niketha@westernpapersl.com");

                    //Cc list
                    //message.CC.Add("yohan@westernpapersl.com");

                    message.From = new MailAddress("gatepass@westernpapersl.com"); // Sender address  
                    message.Subject = "WPI GATE PASS SYSTEM";

                    message.IsBodyHtml = true; // HTML email  

                    message.Body = "Dear " + fullname + "," + "<br />Your Gate Pass is Approved by Management Approver. Gate Pass No :" + "<b>" + GPNumber + "</b>" + " Approved By " + "<b>" + gmname + "</b>" + "<br />" + "Thank you.";

                    client.Send(message);

                }
            }

            return RedirectToPage("ViewPendingGP");
        }

        public ActionResult EmailSendFirstRejected(int Id)
        {
            currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int uid = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.UserId).FirstOrDefault();
            int deptid = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.DepId).FirstOrDefault();
            string hodname = _db.Department.Where(u => u.Id == deptid).Select(u => u.Hod).FirstOrDefault();
            string Email = _db.User.Where(u => u.Id == uid).Select(u => u.Email).FirstOrDefault();
            string fullname = _db.User.Where(u => u.Id == uid).Select(u => u.FullName).FirstOrDefault();

            GPNumber = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.PersonalGPId).FirstOrDefault();

            if (Email == "None")
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
                message.To.Add(Email);
                //message.To.Add("niketha@westernpapersl.com");

                //Cc list
                //message.CC.Add("yohan@westernpapersl.com");

                message.From = new MailAddress("gatepass@westernpapersl.com"); // Sender address  
                message.Subject = "WPI GATE PASS SYSTEM";

                message.IsBodyHtml = true; // HTML email  

                message.Body = "Dear " + fullname + "," + "<br />Your Gate Pass is Rejected by First Approver. Gate Pass No :" + "<b>" + GPNumber + "</b>" + " Rejected by " + "<b>" + hodname + "</b>" + "<br />" + "Thank you.";

                client.Send(message);

            }

            return RedirectToPage("ViewPendingGP");
        }

        public ActionResult EmailSendSecondRejected(int Id)
        {
            currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            int uid = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.UserId).FirstOrDefault();

            int Aprchng = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.ChApprvlId).FirstOrDefault();

            if (Aprchng == 0)
            {
                int deptid = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.DepId).FirstOrDefault();
                string gmname = _db.Department.Where(u => u.Id == deptid).Select(u => u.Gm).FirstOrDefault();

                string Email = _db.User.Where(u => u.Id == uid).Select(u => u.Email).FirstOrDefault();
                string fullname = _db.User.Where(u => u.Id == uid).Select(u => u.FullName).FirstOrDefault();

                GPNumber = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.PersonalGPId).FirstOrDefault();


                if (Email == "None")
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
                    message.To.Add(Email);
                    //message.To.Add("niketha@westernpapersl.com");

                    //Cc list
                    //message.CC.Add("yohan@westernpapersl.com");

                    message.From = new MailAddress("gatepass@westernpapersl.com"); // Sender address  
                    message.Subject = "WPI GATE PASS SYSTEM";

                    message.IsBodyHtml = true; // HTML email  

                    message.Body = "Dear " + fullname + "," + "<br />Your Gate Pass is Rejected by Management Approver. Gate Pass No :" + "<b>" + GPNumber + "</b>" + " Rejected By " + "<b>" + gmname + "</b>" + "<br />" + "Thank you.";

                    client.Send(message);

                }
            }
            else
            {
                int deptid = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.ChApprvlId).FirstOrDefault();
                string gmname = _db.Department.Where(u => u.Id == deptid).Select(u => u.Gm).FirstOrDefault();

                string Email = _db.User.Where(u => u.Id == uid).Select(u => u.Email).FirstOrDefault();
                string fullname = _db.User.Where(u => u.Id == uid).Select(u => u.FullName).FirstOrDefault();

                GPNumber = _db.PersonalGP.Where(u => u.Id == Id).Select(u => u.PersonalGPId).FirstOrDefault();


                if (Email == "None")
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
                    message.To.Add(Email);
                    //message.To.Add("niketha@westernpapersl.com");

                    //Cc list
                    //message.CC.Add("yohan@westernpapersl.com");

                    message.From = new MailAddress("gatepass@westernpapersl.com"); // Sender address  
                    message.Subject = "WPI GATE PASS SYSTEM";

                    message.IsBodyHtml = true; // HTML email  

                    message.Body = "Dear " + fullname + "," + "<br />Your Gate Pass is Rejected by Management Approver. Gate Pass No :" + "<b>" + GPNumber + "</b>" + " Rejected By " + "<b>" + gmname + "</b>" + "<br />" + "Thank you.";

                    client.Send(message);

                }
            }

            return RedirectToPage("ViewPendingGP");
        }

        public async Task<IActionResult> OnPostApprove(int Id)
        {
            try
            {
                roleId = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                
                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                if (roleId == "1")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASmd = 'A', ASdgm = 'A' , ASmdtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                    EmailSendSecondApproved(Id);
                }
                else if (roleId == "3")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASgm = 'A' , ASgmtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "4")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASdgm = 'A' , ASdgmtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                    EmailSendSecondApproved(Id);
                }
                else if (roleId == "5")
                {
                    if(deptId == 12)
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET AShod = 'A' ,ASdgm = 'A' , AShodtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                        _notify.Success("Approved!!", 5);
                    }
                    else
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET AShod = 'A' , AShodtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                        _notify.Success("Approved!!", 5);
                    }
                    EmailSendFirstApproved(Id);
                    EmailSendToManagApprover(Id);
                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewPendingGPModel OnPostApprove: " + ex.Message);
            }
            return RedirectToPage("ViewPendingGP");
        }

        public async Task<IActionResult> OnPostReject(int Id)
        {
            try
            {
                
                roleId = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                if (PersonalGPB.RejctReason == null)
                {
                    _notify.Error("Please Insert Reason!!", 5);
                }
                else
                {
                    if (roleId == "1")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASmd = 'R' ,RejctReason = {2}, ASmdtime = {1} WHERE Id = {0}", Id, targetLocalTime, PersonalGPB.RejctReason);
                        _notify.Error("Rejected!!", 5);
                        EmailSendSecondRejected(Id);
                    }
                    else if (roleId == "3")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASgm = 'R' ,RejctReason = {2}, ASgmtime = {1}  WHERE Id = {0}", Id, targetLocalTime, PersonalGPB.RejctReason);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "4")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASdgm = 'R' ,RejctReason = {2}, ASdgmtime = {1}  WHERE Id = {0}", Id, targetLocalTime, PersonalGPB.RejctReason);
                        _notify.Error("Rejected!!", 5);
                        EmailSendSecondRejected(Id);
                    }
                    else if (roleId == "5")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET AShod = 'R' ,RejctReason = {2}, AShodtime = {1}  WHERE Id = {0}", Id, targetLocalTime, PersonalGPB.RejctReason);
                        _notify.Error("Rejected!!", 5);
                        EmailSendFirstRejected(Id);
                    }

                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewPendingGPModel OnPostReject: " + ex.Message);
            }
            return RedirectToPage("ViewPendingGP");
        }

        public async Task<IActionResult> OnPostApproveWGP(int Id)
        {
            try
            {
                roleId = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                if (roleId == "1")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET ASmd = 'A' ,ASdgm = 'A', ASmdtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "3")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET ASgm = 'A' , ASgmtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "4")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET ASdgm = 'A' , ASdgmtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "5")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET AShod = 'A' , AShodtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                    _notify.Success("Approved!!", 5);

                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewPendingGPModel OnPostApproveWGP: " + ex.Message);
            }
            return RedirectToPage("ViewPendingGP");
        }

        public async Task<IActionResult> OnPostRejectWGP(int Id)
        {
            try
            {
                roleId = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                //if (WorkerGPB.RejctReason == null)
                //{
                //    _notify.Error("Please Insert Reason!!", 5);
                //}
                //else
                //{
                    if (roleId == "1")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET ASmd = 'R' , ASmdtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "3")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET ASgm = 'R' , ASgmtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "4")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET ASdgm = 'R' , ASdgmtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "5")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE WorkerGP SET AShod = 'R' , AShodtime = {1} WHERE Id = {0}", Id, targetLocalTime);
                        _notify.Error("Rejected!!", 5);
                    }
                //}
                    Model.WorkerGP updatedpwgp = _db.WorkerGP.FirstOrDefault(c => c.Id == Id);
                    if (updatedpwgp != null)
                    {
                        updatedpwgp.RejctReason = WorkerGPB.RejctReason;
                        _db.SaveChanges();
                    }
                    else
                    {
                        cm.Logwrite("Error in OnPostApproveChange method: if()");
                    }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewPendingGPModel OnPostRejectWGP: " + ex.Message);
            }
            return RedirectToPage("ViewPendingGP");
        }

    }
}
