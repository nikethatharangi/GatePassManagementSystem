using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using GatePassManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GatePassManagementSystem.Pages.Reports
{
    public class LchReportModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private Common cm;
        public static List<Model.PersonalGP> PersonalGP { get; set; }
        public static List<Model.WorkerGP> WorkerGP { get; set; }
        public LchReportModel(ApplicationDbContext db)
        {
            _db = db;
            cm = new Common();
        }
        public ApplicationDbContext Db => _db;
        public IEnumerable<Model.PersonalGP> PersonalGPs { get; set; }
        public IEnumerable<Model.WorkerGP> WorkerGPs { get; set; }
        public Model.Workers Workers { get; set; }


        public void OnGet()
        {
            try
            {
                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
                DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

                PersonalGPs = _db.PersonalGP.Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.ChkLunch == true).ToList();
                WorkerGPs = _db.WorkerGP.Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.ChkLunch == true).ToList();
            }
            catch (Exception ex)
            {
                cm.Logwrite("Error in ViewGPModel OnGet method :" + ex.Message);
            }
        }

        public IActionResult OnGetGenerateExcel()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            PersonalGPs = _db.PersonalGP
                .Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.ChkLunch == true && ((gp.InTime - gp.OutTime).Minutes > 60) || (gp.SinIntime - gp.SinOuttime).Minutes > 60)
                .ToList();

            var personalGPsList = PersonalGPs.ToList();

            return GenerateExcel(personalGPsList);
        }

        public IActionResult GenerateExcel(List<Model.PersonalGP> personalGPs)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LunchReportStaff");

                worksheet.Cell(1, 1).Value = "PersonalGP ID";
                worksheet.Cell(1, 2).Value = "EPF Number";
                worksheet.Cell(1, 3).Value = "Name";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Created Date";
                worksheet.Cell(1, 6).Value = "Out Time";
                worksheet.Cell(1, 7).Value = "In Time";

                var headerCells = worksheet.Range(1, 1, 1, 7);
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
                    worksheet.Cell(row, 7).Value = personalGP.InTime.ToString();

                    row++;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LunchReportStaff.xlsx");
            }

        }


        public IActionResult OnGetGenerateExcelcsv()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            PersonalGPs = _db.PersonalGP
                .Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.ChkLunch == true && ((gp.InTime - gp.OutTime).Minutes > 60) || (gp.SinIntime - gp.SinOuttime).Minutes > 60)
                .ToList();

            var personalGPsList = PersonalGPs.ToList();

            return GenerateExcelcsv(personalGPsList);
        }

        public IActionResult GenerateExcelcsv(List<Model.PersonalGP> personalGPs)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LunchReportStaff");

                worksheet.Cell(1, 1).Value = "PersonalGP ID";
                worksheet.Cell(1, 2).Value = "EPF Number";
                worksheet.Cell(1, 3).Value = "Name";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Created Date";
                worksheet.Cell(1, 6).Value = "Out Time";
                worksheet.Cell(1, 7).Value = "In Time";

                var headerCells = worksheet.Range(1, 1, 1, 7);
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
                    worksheet.Cell(row, 7).Value = personalGP.InTime.ToString();

                    row++;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LunchReportStaff.csv");
            }

        }

        public IActionResult OnGetGenerateExcelworker()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            WorkerGPs = _db.WorkerGP
                .Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.ChkLunch == true && ((gp.InTime - gp.OutTime).Minutes > 60) || (gp.SinIntime - gp.SinOuttime).Minutes > 60)
                .ToList();

            var workerGPsList = WorkerGPs.ToList();

            return GenerateExcelworker(workerGPsList);
        }

        public IActionResult GenerateExcelworker(List<WorkerGP> workerGPs)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LunchReportWorkers");

                worksheet.Cell(1, 1).Value = "WorkerGP ID";
                worksheet.Cell(1, 2).Value = "EPF Number";
                worksheet.Cell(1, 3).Value = "Worker Name";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Created Date";
                worksheet.Cell(1, 6).Value = "Out Time";
                worksheet.Cell(1, 7).Value = "In Time";

                var headerCells = worksheet.Range(1, 1, 1, 7);
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
                    worksheet.Cell(row, 7).Value = workerGP.InTime.ToString();

                    row++;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LunchReportWorkers.xlsx");
            }

        }


        public IActionResult OnGetGenerateExcelcsvworker()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime targetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

            WorkerGPs = _db.WorkerGP
                .Where(gp => gp.CreateDate.Year == targetLocalTime.Year && gp.CreateDate.Month == targetLocalTime.Month && gp.ChkLunch == true && ((gp.InTime - gp.OutTime).Minutes > 60) || (gp.SinIntime - gp.SinOuttime).Minutes > 60)
                .ToList();

            var workerGPsList = WorkerGPs.ToList();

            return GenerateExcelcsvworker(workerGPsList);
        }

        public IActionResult GenerateExcelcsvworker(List<WorkerGP> workerGPs)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LunchReportWorkers");

                worksheet.Cell(1, 1).Value = "PersonalGP ID";
                worksheet.Cell(1, 2).Value = "EPF Number";
                worksheet.Cell(1, 3).Value = "Worker Name";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Created Date";
                worksheet.Cell(1, 6).Value = "Out Time";
                worksheet.Cell(1, 7).Value = "In Time";

                var headerCells = worksheet.Range(1, 1, 1, 7);
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
                    worksheet.Cell(row, 7).Value = workerGP.InTime.ToString();

                    row++;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LunchReportWorkers.csv");
            }

        }
    }
}
