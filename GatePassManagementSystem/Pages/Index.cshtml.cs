using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GatePassManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace GatePassManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private Common cm;
        private readonly INotyfService _notify;
        public ApplicationDbContext Db => _db;
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public Model.PersonalGP PersonalGPB { get; set; }
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public IndexModel(ILogger<IndexModel> logger,ApplicationDbContext db, INotyfService notyf)
        {
            _logger = logger;
            _db = db;
            cm = new Common();
            _notify = notyf;
        }


        public IEnumerable<Model.PersonalGP> PersonalGPd { get; set; }
        public int Uid { get; set; }
        public string Userrole { get; set; }
        public string roleId { get; set; }
        public string UserName { get; set; }
        public int deptId { get; set; }

        public async Task OnGet()
        {
            try
            {
                UserName = HttpContext.Session.GetString("Username");

                if (UserName == null || UserName == "")
                {
                    Response.Redirect("Login");
                }
                else
                {
                    Userrole = HttpContext.Session.GetString("Roleid");
                    Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                    deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));
                    //PersonalGPd = await _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate >= DateTime.Now.AddDays(-30)).ToListAsync();

                    DateTime refDate = new DateTime(2023, 10, 1);      // (2017, 07, 11, h, m, s, 0, 0); 

                    DateTime start = await _db.PersonalGP.Select(gp => gp.OutTime).FirstOrDefaultAsync();
                    DateTime end = start.AddHours(1);

                    DateTime utcNow = DateTime.UtcNow;
                    TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                    DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                    DateTime today = DateTime.Today;

                    if(Uid == 4) //Mr. Dharampriya
                    {
                        int Completedgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm != null && gp.AShod != null && gp.ASguard != null && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int Totalgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        double percentage = ((double)Completedgp / (double)Totalgp) * 100;
                        percentage = (double)System.Math.Round(percentage, 2);

                        //ViewData["Pecentage"] = percentage;

                        DateTime mondayDate = today.Date.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

                        // Check if the calculated date is before the minimum valid DateTime (January 1, 0001)
                        if (mondayDate.Year <= 1)
                        {
                            // Adjust the calculation to get the Monday date of the previous week
                            mondayDate = today.Date.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday - 7);
                        }

                        
                        int ItDhar = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && (gp.DepId == 2 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int ComDhar = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && (gp.DepId == deptId && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int ExpDhar = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && (gp.DepId == 3 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int GrnDhar = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && (gp.DepId == 4 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int FinDhar = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && (gp.DepId == 1 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int storDhar = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && (gp.DepId == 24 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int FintaxDhar = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && (gp.DepId == 5 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int westDhar = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && (gp.DepId == 28 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int costDhar = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && (gp.DepId == 11 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        //int pnbDhar = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.DepId == 7 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        //int pnbDharw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.DepId == 7 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int turDhar = _db.PersonalGP.Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date && gp.AShod != null && gp.ASdgm == null && (gp.DepId == 27 && gp.ChApprvlId == 0)).Count();
                        int tapcDhar = _db.PersonalGP.Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date && gp.AShod != null && gp.ASdgm == null && (gp.DepId == 28 && gp.ChApprvlId == 0)).Count();
                        //ViewData["Pending"] = _db.PersonalGP.Where(gp => gp.AShod == null && (gp.DepId == deptId || gp.DepId == 1 || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 4 || gp.DepId == 5 ) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int chgArpl = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.ChApprvlId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int chgArplw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.ChAprlId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["PendingDharm"] = costDhar + chgArplw + chgArpl + ItDhar + ComDhar + ExpDhar + GrnDhar + FinDhar + FintaxDhar + turDhar + tapcDhar + storDhar + westDhar;

                        ViewData["OutITcount"] = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveITcount"] = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinITcount"] = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchITcount"] = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["OutExportcount"] = _db.PersonalGP.Where(gp => gp.DepId == 3 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveExportcount"] = _db.PersonalGP.Where(gp => gp.DepId == 3 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinExportcount"] = _db.PersonalGP.Where(gp => gp.DepId == 3 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchExportcount"] = _db.PersonalGP.Where(gp => gp.DepId == 3 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["OutCommcount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveCommcount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinCommcount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchCommcount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["OutFincount"] = _db.PersonalGP.Where(gp => gp.DepId == 1 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveFincount"] = _db.PersonalGP.Where(gp => gp.DepId == 1 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinFincount"] = _db.PersonalGP.Where(gp => gp.DepId == 1 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchFincount"] = _db.PersonalGP.Where(gp => gp.DepId == 1 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["OutGrncount"] = _db.PersonalGP.Where(gp => gp.DepId == 4 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveGrncount"] = _db.PersonalGP.Where(gp => gp.DepId == 4 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinGrncount"] = _db.PersonalGP.Where(gp => gp.DepId == 4 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchGrncount"] = _db.PersonalGP.Where(gp => gp.DepId == 4 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        int lunchIT = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortlIT = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int halfIT = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sinIT = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int maduIT = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int otherIT = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["LunchIT"] = lunchIT;
                        ViewData["ShortLeaveIT"] = shortlIT;
                        ViewData["HalfDayIT"] = halfIT;
                        ViewData["SinthawaIT"] = sinIT;
                        ViewData["MadupitiyaIT"] = maduIT;
                        ViewData["OtherIT"] = otherIT;

                        //int lunchExp = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int shortlExp = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int halfExp = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int sinExp = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int maduExp = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int otherExp = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //ViewData["LunchExp"] = lunchExp;
                        //ViewData["ShortLeaveExp"] = shortlExp;
                        //ViewData["HalfDayExp"] = halfExp;
                        //ViewData["SinthawaExp"] = sinExp;
                        //ViewData["MadupitiyaExp"] = maduExp;
                        //ViewData["OtherExp"] = otherExp;

                        //int lunchCom = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int shortlCom = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int halfCom = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int sinCom = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int maduCom = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int otherCom = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //ViewData["LunchCom"] = lunchCom;
                        //ViewData["ShortLeaveCom"] = shortlCom;
                        //ViewData["HalfDayCom"] = halfCom;
                        //ViewData["SinthawaCom"] = sinCom;
                        //ViewData["MadupitiyaCom"] = maduCom;
                        //ViewData["OtherCom"] = otherCom;



                        //int lunchFin = _db.PersonalGP.Where(gp => gp.DepId == 1 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int shortlFin = _db.PersonalGP.Where(gp => gp.DepId == 1 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int halfFin = _db.PersonalGP.Where(gp => gp.DepId == 1 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int sinFin = _db.PersonalGP.Where(gp => gp.DepId == 1 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int maduFin = _db.PersonalGP.Where(gp => gp.DepId == 1 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int otherFin = _db.PersonalGP.Where(gp => gp.DepId == 1 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int lunchFint = _db.PersonalGP.Where(gp => gp.DepId == 5 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int shortlFint = _db.PersonalGP.Where(gp => gp.DepId == 5 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int halfFint = _db.PersonalGP.Where(gp => gp.DepId == 5 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int sinFint = _db.PersonalGP.Where(gp => gp.DepId == 5 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int maduFint = _db.PersonalGP.Where(gp => gp.DepId == 5 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int otherFint = _db.PersonalGP.Where(gp => gp.DepId == 5 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int a = lunchFin + lunchFint;
                        //int b = shortlFin + shortlFint;
                        //int c = halfFin + halfFint;
                        //int d = sinFin + sinFint;
                        //int e = maduFin + maduFint;
                        //int f = otherFin + otherFint;
                        //ViewData["LunchFin"] = lunchFin;
                        //ViewData["ShortLeaveFin"] = shortlFin;
                        //ViewData["HalfDayFin"] = halfFin;
                        //ViewData["SinthawaFin"] = sinFin;
                        //ViewData["MadupitiyaFin"] = maduFin;
                        //ViewData["OtherFin"] = otherFin;

                        //int lunchGrn = _db.PersonalGP.Where(gp => gp.DepId == 4 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int shortlGrn = _db.PersonalGP.Where(gp => gp.DepId == 4 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int halfGrn = _db.PersonalGP.Where(gp => gp.DepId == 4 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int sinGrn = _db.PersonalGP.Where(gp => gp.DepId == 4 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int maduGrn = _db.PersonalGP.Where(gp => gp.DepId == 4 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //int otherGrn = _db.PersonalGP.Where(gp => gp.DepId == 4 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        //ViewData["LunchGrn"] = lunchGrn;
                        //ViewData["ShortLeaveGrn"] = shortlGrn;
                        //ViewData["HalfDayGrn"] = halfGrn;
                        //ViewData["SinthawaGrn"] = sinGrn;
                        //ViewData["MadupitiyaGrn"] = maduGrn;
                        //ViewData["OtherGrn"] = otherGrn;

                        int lunchmon = _db.PersonalGP.Where(gp => gp.ChkLunch == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int shortlmon = _db.PersonalGP.Where(gp => gp.ChkShrt == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int halfmon = _db.PersonalGP.Where(gp => gp.ChkHalfd == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int sinmon = _db.PersonalGP.Where(gp => gp.ChkSinthawatta == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int madumon = _db.PersonalGP.Where(gp => gp.ChkMadu == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int othermon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int cusvisimon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        ViewData["Lunchmon"] = lunchmon;
                        ViewData["ShortLeavemon"] = shortlmon;
                        ViewData["HalfDaymon"] = halfmon;
                        ViewData["Sinthawamon"] = sinmon;
                        ViewData["Madupitiyamon"] = madumon;
                        ViewData["Othermon"] = othermon;
                        ViewData["cusvisimon"] = cusvisimon;
                    }
                    else if(Uid == 31) //Mr. Ruwan
                    {
                        int Completedgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm != null && gp.AShod != null && gp.ASguard != null && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int Totalgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        double percentage = ((double)Completedgp / (double)Totalgp) * 100;
                        percentage = (double)System.Math.Round(percentage, 2);

                        //ViewData["Pecentage"] = percentage;

                        DateTime mondayDate = today.Date.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

                        // Check if the calculated date is before the minimum valid DateTime (January 1, 0001)
                        if (mondayDate.Year <= 1)
                        {
                            // Adjust the calculation to get the Monday date of the previous week
                            mondayDate = today.Date.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday - 7);
                        }

                        int RuwMark = _db.PersonalGP.Where(gp => gp.ASdgm == null  && gp.UserId != Uid && (gp.DepId == 8 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int RuwCus = _db.PersonalGP.Where(gp => gp.ASdgm == null  && gp.UserId != Uid && (gp.DepId == 9 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int RuwInvo = _db.PersonalGP.Where(gp => gp.ASdgm == null  && gp.UserId != Uid && (gp.DepId == 25 && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int chgArpl = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.ChApprvlId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int chgArplw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.ChAprlId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        //ViewData["Pending"] = _db.PersonalGP.Where(gp => gp.AShod == null && (gp.DepId == deptId || gp.DepId == 1 || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 4 || gp.DepId == 5 ) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["PendingRuw"] = RuwCus + RuwMark + RuwInvo + chgArpl + chgArplw;

                        ViewData["Outmarkcount"] = _db.PersonalGP.Where(gp => gp.DepId == 8 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Leavemarkcount"] = _db.PersonalGP.Where(gp => gp.DepId == 8 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Sinmarkcount"] = _db.PersonalGP.Where(gp => gp.DepId == 8 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Lunchmarkcount"] = _db.PersonalGP.Where(gp => gp.DepId == 8 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["OutCuscount"] = _db.PersonalGP.Where(gp => gp.DepId == 9 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveCuscount"] = _db.PersonalGP.Where(gp => gp.DepId == 9 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinCuscount"] = _db.PersonalGP.Where(gp => gp.DepId == 9 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchCuscount"] = _db.PersonalGP.Where(gp => gp.DepId == 9 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        int lunchmark = _db.PersonalGP.Where(gp => gp.DepId == 8 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortlmark = _db.PersonalGP.Where(gp => gp.DepId == 8 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int halfmark = _db.PersonalGP.Where(gp => gp.DepId == 8 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sinmark = _db.PersonalGP.Where(gp => gp.DepId == 8 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int madumark = _db.PersonalGP.Where(gp => gp.DepId == 8 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int othermark = _db.PersonalGP.Where(gp => gp.DepId == 8 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["Lunchmark"] = lunchmark;
                        ViewData["ShortLeavemark"] = shortlmark;
                        ViewData["HalfDaymark"] = halfmark;
                        ViewData["Sinthawamark"] = sinmark;
                        ViewData["Madupitiyamark"] = madumark;
                        ViewData["Othermark"] = othermark;

                        int lunchCus = _db.PersonalGP.Where(gp => gp.DepId == 9 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortlCus = _db.PersonalGP.Where(gp => gp.DepId == 9 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int halfCus = _db.PersonalGP.Where(gp => gp.DepId == 9 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sinCus = _db.PersonalGP.Where(gp => gp.DepId == 9 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int maduCus = _db.PersonalGP.Where(gp => gp.DepId == 9 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int otherCus = _db.PersonalGP.Where(gp => gp.DepId == 9 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["LunchCus"] = lunchCus;
                        ViewData["ShortLeaveCus"] = shortlCus;
                        ViewData["HalfDayCus"] = halfCus;
                        ViewData["SinthawaCus"] = sinCus;
                        ViewData["MadupitiyaCus"] = maduCus;
                        ViewData["OtherCus"] = otherCus;

                        int lunchmon = _db.PersonalGP.Where(gp => gp.ChkLunch == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int shortlmon = _db.PersonalGP.Where(gp => gp.ChkShrt == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int halfmon = _db.PersonalGP.Where(gp => gp.ChkHalfd == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int sinmon = _db.PersonalGP.Where(gp => gp.ChkSinthawatta == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int madumon = _db.PersonalGP.Where(gp => gp.ChkMadu == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int othermon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int cusvisimon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        ViewData["Lunchmon"] = lunchmon;
                        ViewData["ShortLeavemon"] = shortlmon;
                        ViewData["HalfDaymon"] = halfmon;
                        ViewData["Sinthawamon"] = sinmon;
                        ViewData["Madupitiyamon"] = madumon;
                        ViewData["Othermon"] = othermon;
                        ViewData["cusvisimon"] = cusvisimon;
                    }
                    else if (Uid == 172) //Mr. Thusitha
                    {
                        int Completedgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm != null && gp.AShod != null && gp.ASguard != null && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int Totalgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        double percentage = ((double)Completedgp / (double)Totalgp) * 100;
                        percentage = (double)System.Math.Round(percentage, 2);

                        //ViewData["Pecentage"] = percentage;

                        DateTime mondayDate = today.Date.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

                        // Check if the calculated date is before the minimum valid DateTime (January 1, 0001)
                        if (mondayDate.Year <= 1)
                        {
                            // Adjust the calculation to get the Monday date of the previous week
                            mondayDate = today.Date.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday - 7);
                        }

                        int ThusPnb = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.UserId != Uid && gp.DepId == 7 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        //int ThusPnbw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.DepId == 7 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int chgArpl = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.ChApprvlId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int chgArplw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.ChAprlId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        //ViewData["Pending"] = _db.PersonalGP.Where(gp => gp.AShod == null && (gp.DepId == deptId || gp.DepId == 1 || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 4 || gp.DepId == 5 ) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["PendingThus"] = ThusPnb + chgArplw + chgArpl;// + ThusPnbw ;

                        ViewData["Outpnbcount"] = _db.PersonalGP.Where(gp => gp.DepId == 7 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Leavepnbcount"] = _db.PersonalGP.Where(gp => gp.DepId == 7 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Sinpnbcount"] = _db.PersonalGP.Where(gp => gp.DepId == 7 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Lunchpnbcount"] = _db.PersonalGP.Where(gp => gp.DepId == 7 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["Outpnbwcount"] = _db.WorkerGP.Where(gp => gp.DepId == 7 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Leavepnbwcount"] = _db.WorkerGP.Where(gp => gp.DepId == 7 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Sinpnbwcount"] = _db.WorkerGP.Where(gp => gp.DepId == 7 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Lunchpnbwcount"] = _db.WorkerGP.Where(gp => gp.DepId == 7 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        int lunchpnb = _db.PersonalGP.Where(gp => gp.DepId == 7 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortlpnb = _db.PersonalGP.Where(gp => gp.DepId == 7 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int halfpnb = _db.PersonalGP.Where(gp => gp.DepId == 7 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sinpnb = _db.PersonalGP.Where(gp => gp.DepId == 7 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int madupnb = _db.PersonalGP.Where(gp => gp.DepId == 7 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int otherpnb = _db.PersonalGP.Where(gp => gp.DepId == 7 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["lunchpnb"] = lunchpnb;
                        ViewData["shortlpnb"] = shortlpnb;
                        ViewData["halfpnb"] = halfpnb;
                        ViewData["sinpnb"] = sinpnb;
                        ViewData["madupnb"] = madupnb;
                        ViewData["otherpnb"] = otherpnb;

                        int lunchpnbw = _db.WorkerGP.Where(gp => gp.DepId == 7 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortlpnbw = _db.WorkerGP.Where(gp => gp.DepId == 7 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int halfpnbw = _db.WorkerGP.Where(gp => gp.DepId == 7 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sinpnbw = _db.WorkerGP.Where(gp => gp.DepId == 7 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int madupnbw = _db.WorkerGP.Where(gp => gp.DepId == 7 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int otherpnbw = _db.WorkerGP.Where(gp => gp.DepId == 7 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["lunchpnbw"] = lunchpnbw;
                        ViewData["shortlpnbw"] = shortlpnbw;
                        ViewData["halfpnbw"] = halfpnbw;
                        ViewData["sinpnbw"] = sinpnbw;
                        ViewData["madupnbw"] = madupnbw;
                        ViewData["otherpnbw"] = otherpnbw;

                        int lunchmon = _db.PersonalGP.Where(gp => gp.ChkLunch == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int shortlmon = _db.PersonalGP.Where(gp => gp.ChkShrt == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int halfmon = _db.PersonalGP.Where(gp => gp.ChkHalfd == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int sinmon = _db.PersonalGP.Where(gp => gp.ChkSinthawatta == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int madumon = _db.PersonalGP.Where(gp => gp.ChkMadu == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int othermon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int cusvisimon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        ViewData["Lunchmon"] = lunchmon;
                        ViewData["ShortLeavemon"] = shortlmon;
                        ViewData["HalfDaymon"] = halfmon;
                        ViewData["Sinthawamon"] = sinmon;
                        ViewData["Madupitiyamon"] = madumon;
                        ViewData["Othermon"] = othermon;
                        ViewData["cusvisimon"] = cusvisimon;
                    }
                    else if (Uid == 145) // Mr. Damith
                    {
                        int Completedgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod != null && gp.AShod != null && gp.ASguard != null && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int Totalgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        double percentage = ((double)Completedgp / (double)Totalgp) * 100;
                        percentage = (double)System.Math.Round(percentage, 2);

                        //ViewData["Pecentage"] = percentage;

                        DateTime mondayDate = today.Date.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

                        // Check if the calculated date is before the minimum valid DateTime (January 1, 0001)
                        if (mondayDate.Year <= 1)
                        {
                            // Adjust the calculation to get the Monday date of the previous week
                            mondayDate = today.Date.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday - 7);
                        }

                        int CoreCha = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null  && gp.UserId != Uid && gp.DepId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int PricCha = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null  && gp.UserId != Uid && gp.DepId == 16 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int CanCha = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.DepId == 17 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int PackCha = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.DepId == 18 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int MainCha = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.DepId == 19 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int IaCha = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.DepId == 21 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int RDCha = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.DepId == 22 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        int CoreChaw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.DepId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int PricChaw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.DepId == 16 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int CanChaw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.DepId == 17 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int PackChaw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.DepId == 18 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int MainChaw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.DepId == 19 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int IaChaw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.DepId == 21 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int RDChaw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.DepId == 22 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int chgArpl = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.ChApprvlId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int chgArplw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.ChAprlId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        //ViewData["Pending"] = _db.PersonalGP.Where(gp => gp.AShod == null && (gp.DepId == deptId || gp.DepId == 1 || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 4 || gp.DepId == 5 ) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["PendingChan"] = chgArpl+ chgArplw+ CoreCha + PricCha + CanCha + PackCha + MainCha + IaCha + RDCha + CoreChaw + PricChaw + CanChaw + PackChaw + MainChaw + IaChaw + RDChaw;

                        ViewData["OutCorecount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveCorecount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinCorecount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchCorecount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["OutPricecount"] = _db.PersonalGP.Where(gp => gp.DepId == 16 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeavePricecount"] = _db.PersonalGP.Where(gp => gp.DepId == 16 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinPricecount"] = _db.PersonalGP.Where(gp => gp.DepId == 16 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchPricecount"] = _db.PersonalGP.Where(gp => gp.DepId == 16 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["OutCancount"] = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveCancount"] = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinCancount"] = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchCancount"] = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["OutPckcount"] = _db.PersonalGP.Where(gp => gp.DepId == 18 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeavePckcount"] = _db.PersonalGP.Where(gp => gp.DepId == 18 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinPckcount"] = _db.PersonalGP.Where(gp => gp.DepId == 18 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchPckcount"] = _db.PersonalGP.Where(gp => gp.DepId == 18 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        int lunchCore = _db.PersonalGP.Where(gp => gp.DepId == 14 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortlCore = _db.PersonalGP.Where(gp => gp.DepId == 14 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int halfCore = _db.PersonalGP.Where(gp => gp.DepId == 14 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sinCore = _db.PersonalGP.Where(gp => gp.DepId == 14 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int maduCore = _db.PersonalGP.Where(gp => gp.DepId == 14 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int otherCore = _db.PersonalGP.Where(gp => gp.DepId == 14 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["LunchCore"] = lunchCore;
                        ViewData["ShortLeaveCore"] = shortlCore;
                        ViewData["HalfDayCore"] = halfCore;
                        ViewData["SinthawaCore"] = sinCore;
                        ViewData["MadupitiyaCore"] = maduCore;
                        ViewData["OtherCore"] = otherCore;

                        int lunchPrice = _db.PersonalGP.Where(gp => gp.DepId == 16 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortlPrice = _db.PersonalGP.Where(gp => gp.DepId == 16 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int halfPrice = _db.PersonalGP.Where(gp => gp.DepId == 16 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sinPrice = _db.PersonalGP.Where(gp => gp.DepId == 16 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int maduPrice = _db.PersonalGP.Where(gp => gp.DepId == 16 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int otherPrice = _db.PersonalGP.Where(gp => gp.DepId == 16 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["LunchPrice"] = lunchPrice;
                        ViewData["ShortLeavePrice"] = shortlPrice;
                        ViewData["HalfDayPrice"] = halfPrice;
                        ViewData["SinthawaPrice"] = sinPrice;
                        ViewData["MadupitiyaPrice"] = maduPrice;
                        ViewData["OtherPrice"] = otherPrice;

                        int lunchCani = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortlCani = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int halfCani = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sinCani = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int maduCani = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int otherCani = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["LunchCani"] = lunchCani;
                        ViewData["ShortLeaveCani"] = shortlCani;
                        ViewData["HalfDayCani"] = halfCani;
                        ViewData["SinthawaCani"] = sinCani;
                        ViewData["MadupitiyaCani"] = maduCani;
                        ViewData["OtherCani"] = otherCani;

                        int lunchPck = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortlPck = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int halfPck = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sinPck = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int maduPck = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int otherPck = _db.PersonalGP.Where(gp => gp.DepId == 17 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["LunchPck"] = lunchPck;
                        ViewData["ShortLeavePck"] = shortlPck;
                        ViewData["HalfDayPck"] = halfPck;
                        ViewData["SinthawaPck"] = sinPck;
                        ViewData["MadupitiyaPck"] = maduPck;
                        ViewData["OtherPck"] = otherPck;

                        int lunchmon = _db.PersonalGP.Where(gp => gp.ChkLunch == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int shortlmon = _db.PersonalGP.Where(gp => gp.ChkShrt == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int halfmon = _db.PersonalGP.Where(gp => gp.ChkHalfd == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int sinmon = _db.PersonalGP.Where(gp => gp.ChkSinthawatta == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int madumon = _db.PersonalGP.Where(gp => gp.ChkMadu == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int othermon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int cusvisimon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        ViewData["Lunchmon"] = lunchmon;
                        ViewData["ShortLeavemon"] = shortlmon;
                        ViewData["HalfDaymon"] = halfmon;
                        ViewData["Sinthawamon"] = sinmon;
                        ViewData["Madupitiyamon"] = madumon;
                        ViewData["Othermon"] = othermon;
                        ViewData["cusvisimon"] = cusvisimon;
                    }
                    else if (Uid == 90) //Mr.Rohan
                    {
                        int Completedgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm != null && gp.AShod != null && gp.ASguard != null && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int Totalgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        double percentage = ((double)Completedgp / (double)Totalgp) * 100;
                        percentage = (double)System.Math.Round(percentage, 2);

                        //ViewData["Pecentage"] = percentage;

                        DateTime mondayDate = today.Date.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

                        // Check if the calculated date is before the minimum valid DateTime (January 1, 0001)
                        if (mondayDate.Year <= 1)
                        {
                            // Adjust the calculation to get the Monday date of the previous week
                            mondayDate = today.Date.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday - 7);
                        }

                        int RohQms = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod == null && gp.UserId != Uid && gp.DepId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int RohTrans = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod == null && gp.UserId != Uid && gp.DepId == 20 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int RohStor = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod == null && gp.UserId != Uid && gp.DepId == 23 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        int RohTransw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod == null && gp.DepId == 20 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int RohStorw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod == null && gp.DepId == 23 && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int chgArpl = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.ChApprvlId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int chgArplw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.ChAprlId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        //ViewData["Pending"] = _db.PersonalGP.Where(gp => gp.AShod == null && (gp.DepId == deptId || gp.DepId == 1 || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 4 || gp.DepId == 5 ) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["PendingRoha"] = RohQms + RohTrans + RohStor + RohTransw + RohStorw + chgArplw + chgArpl;

                        ViewData["Outqmscount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Leaveqmscount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Sinqmscount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Lunchqmscount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["OutTrancount"] = _db.PersonalGP.Where(gp => gp.DepId == 20 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveTrancount"] = _db.PersonalGP.Where(gp => gp.DepId == 20 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinTrancount"] = _db.PersonalGP.Where(gp => gp.DepId == 20 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchTrancount"] = _db.PersonalGP.Where(gp => gp.DepId == 20 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["OutStorcount"] = _db.PersonalGP.Where(gp => gp.DepId == 23 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveStorcount"] = _db.PersonalGP.Where(gp => gp.DepId == 23 && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinStorcount"] = _db.PersonalGP.Where(gp => gp.DepId == 23 && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchStorcount"] = _db.PersonalGP.Where(gp => gp.DepId == 23 && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        int lunchqms = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortlqms = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int halfqms = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sinqms = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int maduqms = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int otherqms = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["Lunchqms"] = lunchqms;
                        ViewData["ShortLeaveqms"] = shortlqms;
                        ViewData["HalfDayqms"] = halfqms;
                        ViewData["Sinthawaqms"] = sinqms;
                        ViewData["Madupitiyaqms"] = maduqms;
                        ViewData["Otherqms"] = otherqms;

                        int lunchTran = _db.PersonalGP.Where(gp => gp.DepId == 20 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortlTran = _db.PersonalGP.Where(gp => gp.DepId == 20 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int halfTran = _db.PersonalGP.Where(gp => gp.DepId == 20 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sinTran = _db.PersonalGP.Where(gp => gp.DepId == 20 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int maduTran = _db.PersonalGP.Where(gp => gp.DepId == 20 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int otherTran = _db.PersonalGP.Where(gp => gp.DepId == 20 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["LunchTran"] = lunchTran;
                        ViewData["ShortLeaveTran"] = shortlTran;
                        ViewData["HalfDayTran"] = halfTran;
                        ViewData["SinthawaTran"] = sinTran;
                        ViewData["MadupitiyaTran"] = maduTran;
                        ViewData["OtherTran"] = otherTran;

                        int lunchStors = _db.PersonalGP.Where(gp => gp.DepId == 23 && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortlStors = _db.PersonalGP.Where(gp => gp.DepId == 23 && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int halfStors = _db.PersonalGP.Where(gp => gp.DepId == 23 && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sinStors = _db.PersonalGP.Where(gp => gp.DepId == 23 && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int maduStors = _db.PersonalGP.Where(gp => gp.DepId == 23 && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int otherStors = _db.PersonalGP.Where(gp => gp.DepId == 23 && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["LunchStors"] = lunchStors;
                        ViewData["ShortLeaveStors"] = shortlStors;
                        ViewData["HalfDayStors"] = halfStors;
                        ViewData["SinthawaStors"] = sinStors;
                        ViewData["MadupitiyaStors"] = maduStors;
                        ViewData["OtherStors"] = otherStors;

                        int lunchmon = _db.PersonalGP.Where(gp => gp.ChkLunch == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int shortlmon = _db.PersonalGP.Where(gp => gp.ChkShrt == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int halfmon = _db.PersonalGP.Where(gp => gp.ChkHalfd == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int sinmon = _db.PersonalGP.Where(gp => gp.ChkSinthawatta == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int madumon = _db.PersonalGP.Where(gp => gp.ChkMadu == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int othermon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int cusvisimon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        ViewData["Lunchmon"] = lunchmon;
                        ViewData["ShortLeavemon"] = shortlmon;
                        ViewData["HalfDaymon"] = halfmon;
                        ViewData["Sinthawamon"] = sinmon;
                        ViewData["Madupitiyamon"] = madumon;
                        ViewData["Othermon"] = othermon;
                        ViewData["cusvisimon"] = cusvisimon;
                    }
                    else if (Userrole == "6")
                    {
                        int Completedgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm != null && gp.AShod != null && gp.ASguard != null && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int Totalgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        double percentage = ((double)Completedgp / (double)Totalgp) * 100;
                        percentage = (double)System.Math.Round(percentage, 2);

                        if(Completedgp == 0)
                        {
                            ViewData["Pecentage"] = "0";
                        }
                        else
                        {
                            ViewData["Pecentage"] = percentage;
                        }

                        
                        if(deptId == 8 || deptId == 9)
                        {
                            ViewData["DgmAknl"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                            ViewData["GuardAknl"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm != null && gp.ASguard == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                            ViewData["Completedgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASgm != null && gp.ASguard != null && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                            ViewData["MangAppgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm == "A" && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        }
                        else if(deptId == 10)
                        {
                            ViewData["mdAknl"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASmd == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                            ViewData["GuardAknl"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASmd != null && gp.ASguard == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                            ViewData["Completedgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASmd != null && gp.ASguard != null && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                            ViewData["MangAppgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASmd == "A" && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        }
                        else
                        {
                            ViewData["HeadAknl"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                            ViewData["DgmAknl"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod != null && gp.ASdgm == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                            ViewData["GuardAknl"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod != null && gp.ASdgm != null && gp.ASguard == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                            ViewData["Completedgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod != null && gp.ASgm != null && gp.ASguard != null && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                            ViewData["MangAppgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm == "A" && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                            ViewData["HodAppgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod == "A" && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        }
                        
                        ViewData["Totalgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        
                        ViewData["Rejgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod == "R" || gp.ASdgm == "R" || gp.ASgm == "R" || gp.ASmd == "R" && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int lunch = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortl = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int half = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sin = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int madu = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int other = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["Lunch"] = lunch;
                        ViewData["ShortLeave"] = shortl;
                        ViewData["HalfDay"] = half;
                        ViewData["Sinthawa"] = sin;
                        ViewData["Madupitiya"] = madu;
                        ViewData["Other"] = other;
                    }
                    else if (Userrole == "5")
                    {

                        //int personalGpCount = _db.PersonalGP
                        //    .Where(gp => gp.UserId == Uid && gp.ASgm == null)
                        //    .Count();
                        //int workerGpCount = _db.WorkerGP
                        //    .Where(wgp => wgp.ASgm == null)
                        //    .Count();

                        //int currentAcknowledgeCount = (int)(ViewData["Acknowledge"] ?? 0);
                        //ViewData["Acknowledge"] = currentAcknowledgeCount + personalGpCount + workerGpCount;

                        int Completedgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod != null && gp.ASdgm != null && gp.ASguard != null && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int Totalgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        double percentage = ((double)Completedgp / (double)Totalgp) * 100;
                        percentage = (double)System.Math.Round(percentage, 2);

                        if (Completedgp == 0)
                        {
                            ViewData["Pecentage"] = "0";
                        }
                        else
                        {
                            ViewData["Pecentage"] = percentage;
                        }

                        int perhodpend = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.AShod == null && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int worhodpend = _db.WorkerGP.Where(gp => gp.DepId == deptId && gp.AShod == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        int tothodpen = perhodpend + worhodpend;

                        if(Uid == 92)
                        {
                           int c = _db.PersonalGP.Where(gp => gp.AShod == null && (gp.DepId == deptId || gp.DepId == 27) && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                            ViewData["HodPending"] = c;
                        }
                        else
                        {
                            ViewData["HodPending"] = tothodpen;
                        }
                        

                        ViewData["HodOutcount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["HodLeavecount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["HodSincount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["HodLunchcount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();


                        ViewData["HeadAknl"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["DgmAknl"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod != null && gp.ASdgm == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["GuardAknl"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod != null  && gp.ASdgm != null && gp.ASguard == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Completedgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod != null && gp.ASdgm != null && gp.ASguard != null && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["Totalgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["HodAppgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod == "A" && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["MangAppgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod == "A" && gp.ASdgm == "A" && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["Rejgp"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && (gp.AShod == "R" || gp.AShod == "R") && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int lunch = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ChkLunch == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int shortl = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ChkShrt == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int half = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ChkHalfd == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int sin = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ChkSinthawatta == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int madu = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ChkMadu == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int other = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ChkOther == true && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        ViewData["Lunch"] = lunch;
                        ViewData["ShortLeave"] = shortl;
                        ViewData["HalfDay"] = half;
                        ViewData["Sinthawa"] = sin;
                        ViewData["Madupitiya"] = madu;
                        ViewData["Other"] = other;


                        int lunchmon = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkLunch == true && gp.CreateDate.Month == DateTime.Now.Month && gp.CreateDate.Year == DateTime.Now.Year).Count();
                        int shortlmon = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkShrt == true && gp.CreateDate.Month == DateTime.Now.Month && gp.CreateDate.Year == DateTime.Now.Year).Count();
                        int halfmon = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkHalfd == true && gp.CreateDate.Month == DateTime.Now.Month && gp.CreateDate.Year == DateTime.Now.Year).Count();
                        int sinmon = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkSinthawatta == true && gp.CreateDate.Month == DateTime.Now.Month && gp.CreateDate.Year == DateTime.Now.Year).Count();
                        int madumon = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkMadu == true && gp.CreateDate.Month == DateTime.Now.Month && gp.CreateDate.Year == DateTime.Now.Year).Count();
                        int othermon = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month && gp.CreateDate.Year == DateTime.Now.Year).Count();
                        ViewData["Lunchmon"] = lunchmon;
                        ViewData["ShortLeavemon"] = shortlmon;
                        ViewData["HalfDaymon"] = halfmon;
                        ViewData["Sinthawamon"] = sinmon;
                        ViewData["Madupitiyamon"] = madumon;
                        ViewData["Othermon"] = othermon;


                        // Get the current week start and end dates
                        //DateTime currentDate = DateTime.Now.AddDays(-7);
                        //DateTime weekStart = currentDate.AddDays((int)DayOfWeek.Monday - (int)currentDate.DayOfWeek);
                        //DateTime weekEnd = weekStart.AddDays(6);

                        //Function to get counts for each reason for a specific day

                        //int GetReasonCountForDay(DateTime date, Func<Model.PersonalGP, bool> chkCondition)
                        //{
                        //    return _db.PersonalGP
                        //        .Where(gp => gp.DepId == 2 && chkCondition(gp) && gp.CreateDate.Date == date.Date)
                        //        .Count();
                        //}

                        //Set ViewData values for each reason for each day
                        //for (int i = 0; i < 7; i++)
                        //{
                        //    DateTime currentDay = weekStart.AddDays(i);


                        //    ViewData[$"Lunch{i}"] = GetReasonCountForDay(currentDay, gp => gp.ChkLunch == true);
                        //    ViewData[$"ShortLeave{i}"] = GetReasonCountForDay(currentDay, gp => gp.ChkShrt == true);
                        //    ViewData[$"HalfDay{i}"] = GetReasonCountForDay(currentDay, gp => gp.ChkHalfd == true);
                        //    ViewData[$"Sinthawa{i}"] = GetReasonCountForDay(currentDay, gp => gp.ChkSinthawatta == true);
                        //    ViewData[$"Madupitiya{i}"] = GetReasonCountForDay(currentDay, gp => gp.ChkMadu == true);
                        //    ViewData[$"Other{i}"] = GetReasonCountForDay(currentDay, gp => gp.ChkOther == true);
                        //}



                        //ViewData["Approved"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod == "A" && gp.ASdgm == "A").Count();
                        //ViewData["Pending"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod == null).Count();
                        //ViewData["Rejected"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod == "R" && gp.ASdgm == "R").Count();
                        //ViewData["All"] = _db.PersonalGP.Where(gp => gp.UserId == Uid).Count();
                        //ViewData["Acknowledge"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.AShod == null).Count();
                        //ViewData["Out"] = _db.PersonalGP.Where(gp => gp.OutTime > refDate && gp.InTime < gp.OutTime).Count();
                        //ViewData["SinIn"] = _db.PersonalGP.Where(gp => gp.SinIntime > refDate && gp.SinOuttime < gp.SinIntime).Count();
                        //ViewData["SinOut"] = _db.PersonalGP.Where(gp => gp.SinIntime > refDate && gp.SinOuttime > gp.SinIntime).Count();
                        //ViewData["Leave"] = _db.PersonalGP.Where(gp => gp.OutTime > refDate && (gp.ChkHalfd == true || gp.ChkShrt == true)).Count();
                        //ViewData["Overdue"] = _db.PersonalGP.Where(gp => gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.InTime > end && gp.ChkLunch == true ).Count();
                    }
                    else if (Userrole == "4")
                    {
                        int Completedgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm != null && gp.AShod != null && gp.ASguard != null && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        int Totalgp = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.CreateDate.Month == today.Month && gp.CreateDate.Year == today.Year).Count();
                        double percentage = ((double)Completedgp / (double)Totalgp) * 100;
                        percentage = (double)System.Math.Round(percentage, 2);

                        if (Completedgp == 0)
                        {
                            ViewData["Pecentage"] = "0";
                        }
                        else
                        {
                            ViewData["Pecentage"] = percentage;
                        }

                        ViewData["HodOutcount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["HodLeavecount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["HodSincount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["HodLunchcount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["Approved"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm == "A" && gp.ASdgm == "A").Count();
                        ViewData["Pending"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm == null).Count();
                        ViewData["Rejected"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm == "R" && gp.ASdgm == "R").Count();
                        ViewData["All"] = _db.PersonalGP.Where(gp => gp.UserId == Uid).Count();
                        ViewData["Acknowledge"] = _db.PersonalGP.Where(gp => gp.UserId == Uid && gp.ASdgm == null).Count();
                        ViewData["Out"] = _db.PersonalGP.Where(gp => gp.OutTime > refDate && gp.InTime < gp.OutTime).Count();
                        ViewData["SinIn"] = _db.PersonalGP.Where(gp => gp.SinIntime > refDate && gp.SinOuttime < gp.SinIntime).Count();
                        ViewData["SinOut"] = _db.PersonalGP.Where(gp => gp.SinIntime > refDate && gp.SinOuttime > gp.SinIntime).Count();
                        ViewData["Leave"] = _db.PersonalGP.Where(gp => gp.OutTime > refDate && (gp.ChkHalfd == true || gp.ChkShrt == true)).Count();
                        ViewData["Overdue"] = _db.PersonalGP.Where(gp => gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.InTime > end && gp.ChkLunch == true).Count();

                        int lunchmon = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkLunch == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int shortlmon = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkShrt == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int halfmon = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkHalfd == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int sinmon = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkSinthawatta == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int madumon = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkMadu == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int othermon = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        ViewData["Lunchmon"] = lunchmon;
                        ViewData["ShortLeavemon"] = shortlmon;
                        ViewData["HalfDaymon"] = halfmon;
                        ViewData["Sinthawamon"] = sinmon;
                        ViewData["Madupitiyamon"] = madumon;
                        ViewData["Othermon"] = othermon;

                    }
                    else if (Userrole == "3")
                    {
                        ViewData["OutAllcount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveAllcount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinAllcount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchAllcount"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        int lunchAll = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int shortlAll = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkShrt == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int halfAll = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkHalfd == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int sinAll = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkSinthawatta == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int maduAll = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkMadu == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int otherAll = _db.PersonalGP.Where(gp => gp.DepId == 2 && gp.ChkOther == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchAll"] = lunchAll;
                        ViewData["ShortLeaveAll"] = shortlAll;
                        ViewData["HalfDayAll"] = halfAll;
                        ViewData["SinthawaAll"] = sinAll;
                        ViewData["MadupitiyaAll"] = maduAll;
                        ViewData["OtherAll"] = otherAll;

                        ViewData["GmPending"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ASgm == null && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        ViewData["Outcount"] = _db.PersonalGP.Where(gp => gp.OutTime > refDate && gp.InTime < gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinIn"] = _db.PersonalGP.Where(gp => gp.SinIntime > refDate && gp.SinOuttime < gp.SinIntime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinOut"] = _db.PersonalGP.Where(gp => gp.SinIntime > refDate && gp.SinOuttime > gp.SinIntime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Leave"] = _db.PersonalGP.Where(gp => gp.OutTime > refDate && (gp.ChkHalfd == true || gp.ChkShrt == true) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Overdue"] = _db.PersonalGP.Where(gp => gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.InTime > end && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        int lunchmon = _db.PersonalGP.Where(gp => gp.ChkLunch == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int shortlmon = _db.PersonalGP.Where(gp => gp.ChkShrt == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int halfmon = _db.PersonalGP.Where(gp => gp.ChkHalfd == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int sinmon = _db.PersonalGP.Where(gp => gp.ChkSinthawatta == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int madumon = _db.PersonalGP.Where(gp => gp.ChkMadu == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int othermon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int cusvisimon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        ViewData["Lunchmon"] = lunchmon;
                        ViewData["ShortLeavemon"] = shortlmon;
                        ViewData["HalfDaymon"] = halfmon;
                        ViewData["Sinthawamon"] = sinmon;
                        ViewData["Madupitiyamon"] = madumon;
                        ViewData["Othermon"] = othermon;
                        ViewData["cusvisimon"] = cusvisimon;

                    }
                    else if (Userrole == "1")
                    {
                        int chgArpl = _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.UserId != Uid && gp.ChApprvlId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int chgArplw = _db.WorkerGP.Where(gp => gp.ASdgm == null && gp.AShod != null && gp.ChAprlId == deptId && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int Admimd = _db.PersonalGP.Where(gp => gp.ASmd == null && gp.UserId != Uid && (gp.DepId == deptId && gp.ChApprvlId == 0) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["Pendingmd"] = chgArpl + chgArplw + Admimd;

                        ViewData["OutAll"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveAll"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinAll"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.SinIntime > gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchAll"] = _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.OutTime > refDate && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        int lunchAll = _db.PersonalGP.Where(gp => gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int shortlAll = _db.PersonalGP.Where(gp => gp.ChkShrt == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int halfAll = _db.PersonalGP.Where(gp => gp.ChkHalfd == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int sinAll = _db.PersonalGP.Where(gp => gp.ChkSinthawatta == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int maduAll = _db.PersonalGP.Where(gp => gp.ChkMadu == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        int otherAll = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LunchAll"] = lunchAll;
                        ViewData["ShortLeaveAll"] = shortlAll;
                        ViewData["HalfDayAll"] = halfAll;
                        ViewData["SinthawaAll"] = sinAll;
                        ViewData["MadupitiyaAll"] = maduAll;
                        ViewData["OtherAll"] = otherAll;

                        ViewData["OutAll"] = _db.PersonalGP.Where(gp => gp.OutTime > refDate && gp.InTime < gp.OutTime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinInAll"] = _db.PersonalGP.Where(gp => gp.SinIntime > refDate && gp.SinOuttime < gp.SinIntime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["SinOutAll"] = _db.PersonalGP.Where(gp => gp.SinIntime > refDate && gp.SinOuttime > gp.SinIntime && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["LeaveAll"] = _db.PersonalGP.Where(gp => gp.OutTime > refDate && (gp.ChkHalfd == true || gp.ChkShrt == true) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();
                        ViewData["OverdueAll"] = _db.PersonalGP.Where(gp => gp.OutTime > refDate && gp.InTime > gp.OutTime && gp.InTime > end && gp.ChkLunch == true && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).Count();

                        int lunchmon = _db.PersonalGP.Where(gp => gp.ChkLunch == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int shortlmon = _db.PersonalGP.Where(gp => gp.ChkShrt == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int halfmon = _db.PersonalGP.Where(gp => gp.ChkHalfd == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int sinmon = _db.PersonalGP.Where(gp => gp.ChkSinthawatta == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int madumon = _db.PersonalGP.Where(gp => gp.ChkMadu == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int othermon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        int cusvisimon = _db.PersonalGP.Where(gp => gp.ChkOther == true && gp.CreateDate.Month == DateTime.Now.Month).Count();
                        ViewData["Lunchmon"] = lunchmon;
                        ViewData["ShortLeavemon"] = shortlmon;
                        ViewData["HalfDaymon"] = halfmon;
                        ViewData["Sinthawamon"] = sinmon;
                        ViewData["Madupitiyamon"] = madumon;
                        ViewData["Othermon"] = othermon;
                        ViewData["cusvisimon"] = cusvisimon;

                    }

                    if (Uid == 4)
                    {
                        PersonalGPs = await _db.PersonalGP.Where(gp => gp.ASdgm == null && gp.AShod == "A" && gp.UserId != Uid && (gp.DepId == deptId || gp.DepId == 2 || gp.DepId == 3 || gp.DepId == 4 || gp.DepId == 5) && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    }
                    else if (Userrole == "1")
                    {
                        PersonalGPs = await _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    }
                    else if (Userrole == "3")
                    {
                        PersonalGPs = await _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ASgm == null && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    }
                    else if (Userrole == "4")
                    {
                        PersonalGPs = await _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.ASdgm == null && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    }
                    else if (Userrole == "5")
                    {

                        ////            var personalGPResults = await _db.PersonalGP
                        ////                .Where(gp => gp.DepId == deptId && gp.ASmd == null)
                        ////                .ToListAsync();

                        ////            var workerGPResults = await _db.WorkerGP
                        ////                .Where(wgp => wgp.DepId == deptId && wgp.ASmd == null)
                        ////                .ToListAsync();

                        ////            // Combine the results from PersonalGP and WorkerGP
                        ////            PersonalGPs = personalGPResults.Cast<Model.PersonalGP>()
                        ////.Union(workerGPResults.Cast<Model.PersonalGP>());
                        ///
                        PersonalGPs = await _db.PersonalGP.Where(gp => gp.DepId == deptId && gp.AShod == null && gp.UserId != Uid && gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.CreateDate.Date == targetLocalTime.Date).ToListAsync();
                    }

                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in Index OnGet method :" + ex.Message);
            }
        }
        public async Task<IActionResult> OnPostApprove(int Id)
        {
            try
            {
                roleId = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                if (roleId == "1")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASmd = 'A' , ASmdtime = GETDATE() WHERE Id = {0}", Id);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "3")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASgm = 'A' , ASgmtime = GETDATE() WHERE Id = {0}", Id);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "4")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASdgm = 'A' , ASdgmtime = GETDATE() WHERE Id = {0}", Id);
                    _notify.Success("Approved!!", 5);
                }
                else if (roleId == "5")
                {
                    await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET AShod = 'A' , AShodtime = GETDATE() WHERE Id = {0}", Id);
                    _notify.Success("Approved!!", 5);
                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in Index OnPostApprove: " + ex.Message);
            }
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostReject(int Id)
        {
            try
            {
                roleId = HttpContext.Session.GetString("Roleid");
                Uid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                deptId = Convert.ToInt32(HttpContext.Session.GetString("DepartId"));

                if (PersonalGPB.RejctReason == null || PersonalGPB.RejctReason == " ")
                {
                    _notify.Error("Please Insert Reason!!", 5);
                }
                else
                {
                    if (roleId == "1")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASmd = 'R' , ASmdtime = GETDATE(),RejctReason = {0} WHERE Id = {1}", PersonalGPB.RejctReason, Id);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "3")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASgm = 'R' , ASgmtime = GETDATE() ,RejctReason = {0} WHERE Id = {1}", PersonalGPB.RejctReason, Id);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "4")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET ASdgm = 'R' , ASdgmtime = GETDATE() ,RejctReason = {0} WHERE Id = {1}", PersonalGPB.RejctReason, Id);
                        _notify.Error("Rejected!!", 5);
                    }
                    else if (roleId == "5")
                    {
                        await _db.Database.ExecuteSqlRawAsync("UPDATE PersonalGP SET AShod = 'R' , AShodtime = GETDATE() ,RejctReason = {0} WHERE Id = {1}", PersonalGPB.RejctReason, Id);
                        _notify.Error("Rejected!!", 5);
                    }
                }

            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in Index OnPostReject: " + ex.Message);
            }
            return RedirectToPage("Index");
        }
    }
}
