using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using ClosedXML.Excel;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GatePassManagementSystem.Pages.Reports
{
    public class AllReportModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notify;
        private Common cm;
        public static List<Model.PersonalGP> PersonalGP { get; set; }
        public static List<Model.WorkerGP> WorkerGP { get; set; }
        public AllReportModel(ApplicationDbContext db)
        {
            _db = db;
            cm = new Common();
        }
        [BindProperty]
        public Model.PersonalGP PersonalGPB { get; set; }
        public ApplicationDbContext Db => _db;
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public IEnumerable<Model.WorkerGP> WorkerGPs { get; set; }
        public Model.Workers Workers { get; set; }

        [BindProperty]
        public DateTime? FromDate { get; set; }

        [BindProperty]
        public DateTime? ToDate { get; set; }

        public void OnGet()
        {
            try
            {
                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                PersonalGPs = _db.PersonalGP.AsEnumerable().Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month).ToList();
                WorkerGPs = _db.WorkerGP.AsEnumerable().Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month).ToList();
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewGPModel OnGet method :" + ex.Message);
            }
        }


        public IActionResult OnPost()
        {
            try
            {
                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                if (DateTime.TryParse(Request.Form["FromDate"], out DateTime fromDate))
                {
                    FromDate = fromDate;
                }

                if (DateTime.TryParse(Request.Form["ToDate"], out DateTime toDate))
                {
                    ToDate = toDate;
                }

                PersonalGPs = _db.PersonalGP.AsEnumerable()
                    .Where(gp => gp.CreateDate >= FromDate && gp.CreateDate <= ToDate)
                    .ToList();

                WorkerGPs = _db.WorkerGP.AsEnumerable()
                    .Where(gp => gp.CreateDate >= FromDate && gp.CreateDate <= ToDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in AllReportModel OnPost method :" + ex.Message);
            }
            return Page();
            //return RedirectToPage("AllReport");
        }


        //public async Task<IActionResult> OnPost(string chkCusVisit, string chkLunch, string chkSinthawatta, string chkHalfd, string chkMadu, string chkShrt, string chkOther)
        //{
        //    try
        //    {
        //        DateTime utcNow = DateTime.UtcNow;
        //        TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
        //        DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

        //        //assigns year, month, day, hour, min, seconds
        //        DateTime start = new DateTime(targetLocalTime.Year, targetLocalTime.Month, targetLocalTime.Day, 8, 1, 1);

        //        DateTime end = new DateTime(targetLocalTime.Year, targetLocalTime.Month, targetLocalTime.Day, 17, 59, 59);

        //        if (ModelState.IsValid)
        //        {
        //            if (chkLunch == "false" && chkSinthawatta == "false" && chkHalfd == "false" && chkMadu == "false" && chkShrt == "false" && chkOther == "false" && chkCusVisit == "false")
        //            {
        //                _notify.Error("Please Select Reason", 5);
        //                return RedirectToPage("AllReport");
        //            }

        //            else
        //            {
        //                //PersonalGPs = _db.PersonalGP.Where(item =>(ChkLunch && item.ChkLunch) || (ChkSinthawatta && item.ChkSinthawatta)).ToList();
        //                PersonalGPs = _db.PersonalGP.AsEnumerable().Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month).ToList();
        //                //WorkerGPs = _db.WorkerGP.AsEnumerable().Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && (gp.ChkLunch == true || )).ToList();

        //                return RedirectToPage("AllReport");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cm.Logwrite($"Error in AllReportModel OnPost method: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
        //    }
        //    return RedirectToPage("AllReport");
        //}

        public IActionResult OnGetGenerateExcel()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            PersonalGPs = _db.PersonalGP
                .Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month).ToList();

            var personalGPsList = PersonalGPs.ToList();

            return GenerateExcel(personalGPsList);
        }

        public IActionResult GenerateExcel(List<Model.PersonalGP> personalGPs)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AllReportStaff");

                worksheet.Cell(1, 1).Value = "PersonalGP ID";
                worksheet.Cell(1, 2).Value = "EPF Number";
                worksheet.Cell(1, 3).Value = "Name";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Reason";
                worksheet.Cell(1, 6).Value = "Description";
                worksheet.Cell(1, 7).Value = "Created Date";
                worksheet.Cell(1, 8).Value = "HOD Approval";
                worksheet.Cell(1, 9).Value = "Management Approval";
                worksheet.Cell(1, 10).Value = "Out Time";
                worksheet.Cell(1, 11).Value = "In Time";

                var headerCells = worksheet.Range(1, 1, 1, 11);
                headerCells.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerCells.Style.Font.Bold = true;

                int row = 2;
                foreach (var personalGP in personalGPs)
                {
                    string depname = _db.Department.Where(gp => gp.Id == personalGP.DepId).Select(gp => gp.DeptName).FirstOrDefault();
                    string epfno = _db.User.Where(gp => gp.Id == personalGP.UserId).Select(gp => gp.EPFNumber).FirstOrDefault();
                    string a = _db.PersonalGP.Where(gp => gp.Id == personalGP.Id).Select(gp => gp.AShod).FirstOrDefault();
                    string m = _db.PersonalGP.Where(gp => gp.Id == personalGP.Id).Select(gp => gp.ASdgm).FirstOrDefault();

                    bool chklunch = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkLunch).FirstOrDefault();
                    bool chkSinthw = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkSinthawatta).FirstOrDefault();
                    bool chkMadu = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkMadu).FirstOrDefault();
                    bool chkshort = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkShrt).FirstOrDefault();
                    bool chkhalf = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkHalfd).FirstOrDefault();
                    bool chkothr = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkOther).FirstOrDefault();
                    bool chkcusv = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkCusVisit).FirstOrDefault();


                    worksheet.Cell(row, 1).Value = personalGP.PersonalGPId;
                    worksheet.Cell(row, 2).Value = epfno;
                    worksheet.Cell(row, 3).Value = personalGP.CreateUser;
                    worksheet.Cell(row, 4).Value = depname;

                    if (chklunch == true)
                    {
                        worksheet.Cell(row, 5).Value = "Lunch";
                    }
                    else if(chkSinthw == true)
                    {
                        worksheet.Cell(row, 5).Value = "Sinthawatta";
                    }
                    else if (chkMadu == true)
                    {
                        worksheet.Cell(row, 5).Value = "Madupitiya";
                    }
                    else if (chkshort == true)
                    {
                        worksheet.Cell(row, 5).Value = "Short Leave";
                    }
                    else if (chkhalf == true)
                    {
                        worksheet.Cell(row, 5).Value = "Half Day";
                    }
                    else if (chkothr == true)
                    {
                        worksheet.Cell(row, 5).Value = "Other";
                    }
                    else
                    {
                        worksheet.Cell(row, 5).Value = "Customer Visit";
                    }
                    worksheet.Cell(row, 6).Value = personalGP.Description;
                    worksheet.Cell(row, 7).Value = personalGP.CreateDate;

                    if(a != null)
                    {
                        worksheet.Cell(row, 8).Value = "Approved";
                    }
                    else
                    {
                        worksheet.Cell(row, 8).Value = "-";
                    }

                    if (m != null)
                    {
                        worksheet.Cell(row, 9).Value = "Approved";
                    }
                    else
                    {
                        worksheet.Cell(row, 9).Value = "-";
                    }

                    worksheet.Cell(row, 10).Value = personalGP.OutTime.ToString();
                    worksheet.Cell(row, 11).Value = personalGP.InTime.ToString();

                    row++;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllReportStaff.xlsx");
            }

        }


        public IActionResult OnGetGenerateExcelcsv()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            PersonalGPs = _db.PersonalGP
                .Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month)
                .ToList();

            var personalGPsList = PersonalGPs.ToList();

            return GenerateExcelcsv(personalGPsList);
        }

        public IActionResult GenerateExcelcsv(List<Model.PersonalGP> personalGPs)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AllReportStaff");

                worksheet.Cell(1, 1).Value = "PersonalGP ID";
                worksheet.Cell(1, 2).Value = "EPF Number";
                worksheet.Cell(1, 3).Value = "Name";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Reason";
                worksheet.Cell(1, 6).Value = "Description";
                worksheet.Cell(1, 7).Value = "Created Date";
                worksheet.Cell(1, 8).Value = "HOD Approval";
                worksheet.Cell(1, 9).Value = "Management Approval";
                worksheet.Cell(1, 10).Value = "Out Time";
                worksheet.Cell(1, 11).Value = "In Time";

                var headerCells = worksheet.Range(1, 1, 1, 11);
                headerCells.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerCells.Style.Font.Bold = true;

                int row = 2;
                foreach (var personalGP in personalGPs)
                {
                    string depname = _db.Department.Where(gp => gp.Id == personalGP.DepId).Select(gp => gp.DeptName).FirstOrDefault();
                    string epfno = _db.User.Where(gp => gp.Id == personalGP.UserId).Select(gp => gp.EPFNumber).FirstOrDefault();
                    string a = _db.PersonalGP.Where(gp => gp.Id == personalGP.Id).Select(gp => gp.AShod).FirstOrDefault();
                    string m = _db.PersonalGP.Where(gp => gp.Id == personalGP.Id).Select(gp => gp.ASdgm).FirstOrDefault();

                    bool chklunch = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkLunch).FirstOrDefault();
                    bool chkSinthw = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkSinthawatta).FirstOrDefault();
                    bool chkMadu = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkMadu).FirstOrDefault();
                    bool chkshort = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkShrt).FirstOrDefault();
                    bool chkhalf = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkHalfd).FirstOrDefault();
                    bool chkothr = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkOther).FirstOrDefault();
                    bool chkcusv = _db.PersonalGP.Where(gp => gp.PersonalGPId == personalGP.PersonalGPId).Select(gp => gp.ChkCusVisit).FirstOrDefault();


                    worksheet.Cell(row, 1).Value = personalGP.PersonalGPId;
                    worksheet.Cell(row, 2).Value = epfno;
                    worksheet.Cell(row, 3).Value = personalGP.CreateUser;
                    worksheet.Cell(row, 4).Value = depname;

                    if (chklunch == true)
                    {
                        worksheet.Cell(row, 5).Value = "Lunch";
                    }
                    else if (chkSinthw == true)
                    {
                        worksheet.Cell(row, 5).Value = "Sinthawatta";
                    }
                    else if (chkMadu == true)
                    {
                        worksheet.Cell(row, 5).Value = "Madupitiya";
                    }
                    else if (chkshort == true)
                    {
                        worksheet.Cell(row, 5).Value = "Short Leave";
                    }
                    else if (chkhalf == true)
                    {
                        worksheet.Cell(row, 5).Value = "Half Day";
                    }
                    else if (chkothr == true)
                    {
                        worksheet.Cell(row, 5).Value = "Other";
                    }
                    else
                    {
                        worksheet.Cell(row, 5).Value = "Customer Visit";
                    }

                    worksheet.Cell(row, 6).Value = personalGP.Description;
                    worksheet.Cell(row, 7).Value = personalGP.CreateDate;

                    if (a != null)
                    {
                        worksheet.Cell(row, 8).Value = "Approved";
                    }
                    else
                    {
                        worksheet.Cell(row, 8).Value = "-";
                    }

                    if (m != null)
                    {
                        worksheet.Cell(row, 9).Value = "Approved";
                    }
                    else
                    {
                        worksheet.Cell(row, 9).Value = "-";
                    }

                    worksheet.Cell(row, 10).Value = personalGP.OutTime.ToString();
                    worksheet.Cell(row, 11).Value = personalGP.InTime.ToString();

                    row++;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllReportStaff.csv");
            }

        }

        public IActionResult OnGetGenerateExcelworker()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            WorkerGPs = _db.WorkerGP
                .Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month).ToList();

            var workerGPsList = WorkerGPs.ToList();

            return GenerateExcelworker(workerGPsList);
        }

        public IActionResult GenerateExcelworker(List<WorkerGP> workerGPs)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AllReportWorkers");

                worksheet.Cell(1, 1).Value = "WorkerGP ID";
                worksheet.Cell(1, 2).Value = "EPF Number";
                worksheet.Cell(1, 3).Value = "Worker Name";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Reason";
                worksheet.Cell(1, 6).Value = "Created Date";
                worksheet.Cell(1, 7).Value = "HOD Approval";
                worksheet.Cell(1, 8).Value = "Management Approval";
                worksheet.Cell(1, 9).Value = "Out Time";
                worksheet.Cell(1, 10).Value = "In Time";

                var headerCells = worksheet.Range(1, 1, 1, 10);
                headerCells.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerCells.Style.Font.Bold = true;

                int row = 2;
                foreach (var workerGP in workerGPs)
                {
                    string depname = _db.Department.Where(gp => gp.Id == workerGP.DepId).Select(gp => gp.DeptName).FirstOrDefault();
                    string epfno = _db.Workers.Where(gp => gp.Id == workerGP.WrkId).Select(gp => gp.EPFNo).FirstOrDefault();
                    string workerName = _db.Workers.Where(gp => gp.Id == workerGP.WrkId).Select(gp => gp.Name).FirstOrDefault();
                    string a = _db.WorkerGP.Where(gp => gp.Id == workerGP.Id).Select(gp => gp.AShod).FirstOrDefault();
                    string m = _db.WorkerGP.Where(gp => gp.Id == workerGP.Id).Select(gp => gp.ASdgm).FirstOrDefault();

                    bool chklunch = _db.WorkerGP.Where(gp => gp.WorkerGPId == workerGP.WorkerGPId).Select(gp => gp.ChkLunch).FirstOrDefault();
                    bool chkSinthw = _db.WorkerGP.Where(gp => gp.WorkerGPId == workerGP.WorkerGPId).Select(gp => gp.ChkSinthawatta).FirstOrDefault();
                    bool chkMadu = _db.WorkerGP.Where(gp => gp.WorkerGPId == workerGP.WorkerGPId).Select(gp => gp.ChkMadu).FirstOrDefault();
                    bool chkshort = _db.WorkerGP.Where(gp => gp.WorkerGPId == workerGP.WorkerGPId).Select(gp => gp.ChkShrt).FirstOrDefault();
                    bool chkhalf = _db.WorkerGP.Where(gp => gp.WorkerGPId == workerGP.WorkerGPId).Select(gp => gp.ChkHalfd).FirstOrDefault();
                    bool chkothr = _db.WorkerGP.Where(gp => gp.WorkerGPId == workerGP.WorkerGPId).Select(gp => gp.ChkOther).FirstOrDefault();
                   

                    worksheet.Cell(row, 1).Value = workerGP.WorkerGPId;
                    worksheet.Cell(row, 2).Value = epfno;
                    worksheet.Cell(row, 3).Value = workerName;
                    worksheet.Cell(row, 4).Value = depname;

                    if (chklunch == true)
                    {
                        worksheet.Cell(row, 5).Value = "Lunch";
                    }
                    else if (chkSinthw == true)
                    {
                        worksheet.Cell(row, 5).Value = "Sinthawatta";
                    }
                    else if (chkMadu == true)
                    {
                        worksheet.Cell(row, 5).Value = "Madupitiya";
                    }
                    else if (chkshort == true)
                    {
                        worksheet.Cell(row, 5).Value = "Short Leave";
                    }
                    else if (chkhalf == true)
                    {
                        worksheet.Cell(row, 5).Value = "Half Day";
                    }
                    else
                    {
                        worksheet.Cell(row, 5).Value = "Other";
                    }

                    worksheet.Cell(row, 6).Value = workerGP.CreateDate;

                    if (a != null)
                    {
                        worksheet.Cell(row, 7).Value = "Approved";
                    }
                    else
                    {
                        worksheet.Cell(row, 7).Value = "-";
                    }

                    if (m != null)
                    {
                        worksheet.Cell(row, 8).Value = "Approved";
                    }
                    else
                    {
                        worksheet.Cell(row, 8).Value = "-";
                    }

                    worksheet.Cell(row, 9).Value = workerGP.OutTime.ToString();
                    worksheet.Cell(row, 10).Value = workerGP.InTime.ToString();

                    row++;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllReportWorkers.xlsx");
            }

        }


        public IActionResult OnGetGenerateExcelcsvworker()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            WorkerGPs = _db.WorkerGP
                .Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month)
                .ToList();

            var workerGPsList = WorkerGPs.ToList();

            return GenerateExcelcsvworker(workerGPsList);
        }

        public IActionResult GenerateExcelcsvworker(List<WorkerGP> workerGPs)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AllReportWorkers");

                worksheet.Cell(1, 1).Value = "PersonalGP ID";
                worksheet.Cell(1, 2).Value = "EPF Number";
                worksheet.Cell(1, 3).Value = "Worker Name";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Reason";
                worksheet.Cell(1, 6).Value = "Created Date";
                worksheet.Cell(1, 7).Value = "HOD Approval";
                worksheet.Cell(1, 8).Value = "Management Approval";
                worksheet.Cell(1, 9).Value = "Out Time";
                worksheet.Cell(1, 10).Value = "In Time";

                var headerCells = worksheet.Range(1, 1, 1, 10);
                headerCells.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerCells.Style.Font.Bold = true;

                int row = 2;
                foreach (var workerGP in workerGPs)
                {
                    string depname = _db.Department.Where(gp => gp.Id == workerGP.DepId).Select(gp => gp.DeptName).FirstOrDefault();
                    string epfno = _db.Workers.Where(gp => gp.Id == workerGP.WrkId).Select(gp => gp.EPFNo).FirstOrDefault();
                    string workerName = _db.Workers.Where(gp => gp.Id == workerGP.WrkId).Select(gp => gp.Name).FirstOrDefault();
                    string a = _db.WorkerGP.Where(gp => gp.Id == workerGP.Id).Select(gp => gp.AShod).FirstOrDefault();
                    string m = _db.WorkerGP.Where(gp => gp.Id == workerGP.Id).Select(gp => gp.ASdgm).FirstOrDefault();

                    bool chklunch = _db.WorkerGP.Where(gp => gp.WorkerGPId == workerGP.WorkerGPId).Select(gp => gp.ChkLunch).FirstOrDefault();
                    bool chkSinthw = _db.WorkerGP.Where(gp => gp.WorkerGPId == workerGP.WorkerGPId).Select(gp => gp.ChkSinthawatta).FirstOrDefault();
                    bool chkMadu = _db.WorkerGP.Where(gp => gp.WorkerGPId == workerGP.WorkerGPId).Select(gp => gp.ChkMadu).FirstOrDefault();
                    bool chkshort = _db.WorkerGP.Where(gp => gp.WorkerGPId == workerGP.WorkerGPId).Select(gp => gp.ChkShrt).FirstOrDefault();
                    bool chkhalf = _db.WorkerGP.Where(gp => gp.WorkerGPId == workerGP.WorkerGPId).Select(gp => gp.ChkHalfd).FirstOrDefault();
                    bool chkothr = _db.WorkerGP.Where(gp => gp.WorkerGPId == workerGP.WorkerGPId).Select(gp => gp.ChkOther).FirstOrDefault();


                    worksheet.Cell(row, 1).Value = workerGP.WorkerGPId;
                    worksheet.Cell(row, 2).Value = epfno;
                    worksheet.Cell(row, 3).Value = workerName;
                    worksheet.Cell(row, 4).Value = depname;

                    if (chklunch == true)
                    {
                        worksheet.Cell(row, 5).Value = "Lunch";
                    }
                    else if (chkSinthw == true)
                    {
                        worksheet.Cell(row, 5).Value = "Sinthawatta";
                    }
                    else if (chkMadu == true)
                    {
                        worksheet.Cell(row, 5).Value = "Madupitiya";
                    }
                    else if (chkshort == true)
                    {
                        worksheet.Cell(row, 5).Value = "Short Leave";
                    }
                    else if (chkhalf == true)
                    {
                        worksheet.Cell(row, 5).Value = "Half Day";
                    }
                    else
                    {
                        worksheet.Cell(row, 5).Value = "Other";
                    }

                    worksheet.Cell(row, 6).Value = workerGP.CreateDate;

                    if (a != null)
                    {
                        worksheet.Cell(row, 7).Value = "Approved";
                    }
                    else
                    {
                        worksheet.Cell(row, 7).Value = "-";
                    }

                    if (m != null)
                    {
                        worksheet.Cell(row, 8).Value = "Approved";
                    }
                    else
                    {
                        worksheet.Cell(row, 8).Value = "-";
                    }

                    worksheet.Cell(row, 9).Value = workerGP.OutTime.ToString();
                    worksheet.Cell(row, 10).Value = workerGP.InTime.ToString();

                    row++;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllReportWorkers.csv");
            }

        }
    }
}
