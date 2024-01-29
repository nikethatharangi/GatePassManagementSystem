using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GatePassManagementSystem.Pages.Reports
{
    public class HalfDReportModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private Common cm;
        public static List<Model.PersonalGP> PersonalGP { get; set; }
        public static List<Model.WorkerGP> WorkerGP { get; set; }
        public HalfDReportModel(ApplicationDbContext db)
        {
            _db = db;
            cm = new Common();
        }
        public ApplicationDbContext Db => _db;
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public IEnumerable<Model.WorkerGP> WorkerGPs { get; set; }
        public Model.Workers Workers { get; set; }

        public async Task OnGet()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            PersonalGPs = await _db.PersonalGP.Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.ChkHalfd == true).ToListAsync();
            WorkerGPs = await _db.WorkerGP.Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.ChkHalfd == true).ToListAsync();

        }

        public IActionResult OnGetGenerateExcel()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);
           
            PersonalGPs = _db.PersonalGP
                .Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.ChkHalfd == true)
                .ToList();

            var personalGPsList = PersonalGPs.ToList();

            return GenerateExcel(personalGPsList);
        }

        public IActionResult GenerateExcel(List<Model.PersonalGP> personalGPs)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("HalfDayReportStaff");

                worksheet.Cell(1, 1).Value = "PersonalGP ID";
                worksheet.Cell(1, 2).Value = "EPF Number";
                worksheet.Cell(1, 3).Value = "Name";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Created Date";
                worksheet.Cell(1, 6).Value = "Out Time";

                var headerCells = worksheet.Range(1, 1, 1, 6);
                headerCells.Style.Fill.BackgroundColor = XLColor.LightBlue; 
                headerCells.Style.Font.Bold = true;

                int row = 2;
                foreach (var personalGP in personalGPs)
                {
                    string depname = _db.Department.Where(gp => gp.Id == personalGP.DepId).Select(gp => gp.DeptName).FirstOrDefault();
                    string epfno = _db.User.Where(gp => gp.Id == personalGP.UserId).Select(gp => gp.EPFNumber).FirstOrDefault();

                    worksheet.Cell(row, 1).Value = personalGP.PersonalGPId;
                    worksheet.Cell(row, 2).Value = epfno;
                    worksheet.Cell(row, 3).Value = personalGP.CreateUser;
                    worksheet.Cell(row, 4).Value = depname;
                    worksheet.Cell(row, 5).Value = personalGP.CreateDate;
                    worksheet.Cell(row, 6).Value = personalGP.OutTime.ToString();

                    row++;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HalfDayReportStaff.xlsx");
            }

        }


        public IActionResult OnGetGenerateExcelcsv()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            PersonalGPs = _db.PersonalGP
                .Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.ChkHalfd == true)
                .ToList();

            var personalGPsList = PersonalGPs.ToList();
            
            return GenerateExcelcsv(personalGPsList);
        }

        public IActionResult GenerateExcelcsv(List<Model.PersonalGP> personalGPs)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("HalfDayReportStaff");

                worksheet.Cell(1, 1).Value = "PersonalGP ID";
                worksheet.Cell(1, 2).Value = "EPF Number";
                worksheet.Cell(1, 3).Value = "Name";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Created Date";
                worksheet.Cell(1, 6).Value = "Out Time";

                var headerCells = worksheet.Range(1, 1, 1, 6);
                headerCells.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerCells.Style.Font.Bold = true;

                int row = 2;
                foreach (var personalGP in personalGPs)
                {
                    string depname = _db.Department.Where(gp => gp.Id == personalGP.DepId).Select(gp => gp.DeptName).FirstOrDefault();
                    string epfno = _db.User.Where(gp => gp.Id == personalGP.UserId).Select(gp => gp.EPFNumber).FirstOrDefault();

                    worksheet.Cell(row, 1).Value = personalGP.PersonalGPId;
                    worksheet.Cell(row, 2).Value = epfno;
                    worksheet.Cell(row, 3).Value = personalGP.CreateUser;
                    worksheet.Cell(row, 4).Value = depname;
                    worksheet.Cell(row, 5).Value = personalGP.CreateDate;
                    worksheet.Cell(row, 6).Value = personalGP.OutTime.ToString();

                    row++;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HalfDayReportStaff.csv");
            }

        }

        public IActionResult OnGetGenerateExcelworker()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            WorkerGPs = _db.WorkerGP
                .Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.ChkHalfd == true)
                .ToList();

            var workerGPsList = WorkerGPs.ToList();

            return GenerateExcelworker(workerGPsList);
        }

        public IActionResult GenerateExcelworker(List<WorkerGP> workerGPs)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("HalfDayReportWorkers");

                worksheet.Cell(1, 1).Value = "WorkerGP ID";
                worksheet.Cell(1, 2).Value = "EPF Number";
                worksheet.Cell(1, 3).Value = "Worker Name";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Created Date";
                worksheet.Cell(1, 6).Value = "Out Time";

                var headerCells = worksheet.Range(1, 1, 1, 6);
                headerCells.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerCells.Style.Font.Bold = true;

                int row = 2;
                foreach (var workerGP in workerGPs)
                {
                    string depname = _db.Department.Where(gp => gp.Id == workerGP.DepId).Select(gp => gp.DeptName).FirstOrDefault();
                    string epfno = _db.Workers.Where(gp => gp.Id == workerGP.WrkId).Select(gp => gp.EPFNo).FirstOrDefault();
                    string workerName = _db.Workers.Where(gp => gp.Id == workerGP.WrkId).Select(gp => gp.Name).FirstOrDefault();

                    worksheet.Cell(row, 1).Value = workerGP.WorkerGPId;
                    worksheet.Cell(row, 2).Value = epfno;
                    worksheet.Cell(row, 3).Value = workerName;
                    worksheet.Cell(row, 4).Value = depname;
                    worksheet.Cell(row, 5).Value = workerGP.CreateDate;
                    worksheet.Cell(row, 6).Value = workerGP.OutTime.ToString();

                    row++;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HalfDayReportWorkers.xlsx");
            }

        }


        public IActionResult OnGetGenerateExcelcsvworker()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            WorkerGPs = _db.WorkerGP
                .Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.ChkHalfd == true)
                .ToList();

            var workerGPsList = WorkerGPs.ToList();

            return GenerateExcelcsvworker(workerGPsList);
        }

        public IActionResult GenerateExcelcsvworker(List<WorkerGP> workerGPs)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("HalfDayReportWorkers");

                worksheet.Cell(1, 1).Value = "PersonalGP ID";
                worksheet.Cell(1, 2).Value = "EPF Number";
                worksheet.Cell(1, 3).Value = "Worker Name";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Created Date";
                worksheet.Cell(1, 6).Value = "Out Time";

                var headerCells = worksheet.Range(1, 1, 1, 6);
                headerCells.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerCells.Style.Font.Bold = true;

                int row = 2;
                foreach (var workerGP in workerGPs)
                {
                    string depname = _db.Department.Where(gp => gp.Id == workerGP.DepId).Select(gp => gp.DeptName).FirstOrDefault();
                    string epfno = _db.User.Where(gp => gp.Id == workerGP.WrkId).Select(gp => gp.EPFNumber).FirstOrDefault();
                    string workerName = _db.Workers.Where(gp => gp.Id == workerGP.WrkId).Select(gp => gp.Name).FirstOrDefault();

                    worksheet.Cell(row, 1).Value = workerGP.WorkerGPId;
                    worksheet.Cell(row, 2).Value = epfno;
                    worksheet.Cell(row, 3).Value = workerName;
                    worksheet.Cell(row, 4).Value = depname;
                    worksheet.Cell(row, 5).Value = workerGP.CreateDate;
                    worksheet.Cell(row, 6).Value = workerGP.OutTime.ToString();

                    row++;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HalfDayReportWorkers.csv");
            }

        }


        //public IActionResult GenerateWord()
        //{
        //    using (var document = DocX.Create("ShortLeaveReport"))
        //    {

        //        //  document.InsertParagraph("Paragraph 1");

        //        var stream = new MemoryStream();
        //        document.SaveAs(stream);

        //        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ShortLeaveReport.docx");
        //    }
        //}
    }
}
