using OfficeOpenXml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using samp.Data;
using samp.Models;

namespace samp.Controllers
{
    public class DownloadReportController : Controller
    {
        private readonly ApplicationDbContext db;

        // Constructor to initialize ApplicationDbContext
        public DownloadReportController(ApplicationDbContext _db)
        {
            db = _db;
        }

        // GET: DownloadReport
        public ActionResult Index(string status = "all")
        {
            
            var meetings = GetFilteredMeetings(status);
            return View(meetings);
        }

        private List<Meeting> GetFilteredMeetings(string status)
        {
            IQueryable<Meeting> meetingsQuery = db.Meeting;

            if (status != "all")
            {
                meetingsQuery = meetingsQuery.Where(m => m.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            return meetingsQuery.ToList();
        }

        // Action to export to Excel
        public ActionResult ExportToExcel(string status = "all")
        {
            var meetings = GetFilteredMeetings(status);
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Meetings");

                worksheet.Cells[1, 1].Value = "Employee Id";
                worksheet.Cells[1, 2].Value = "Employee";
                worksheet.Cells[1, 3].Value = "Status";

                int row = 2;
                foreach (var meeting in meetings)
                {
                    worksheet.Cells[row, 1].Value = meeting.EmployeeId;  
                    worksheet.Cells[row, 2].Value = meeting.Employee;   
                    worksheet.Cells[row, 3].Value = meeting.Status;
                    row++;
                }

                package.Save();
            }

            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Meetings_Report.xlsx");
        }

      
        [HttpPost]
        public ActionResult UpdateStatus(string employeeId, string employee, string status)
        {
           
            var meeting = db.Meeting.Include(m => m.Employee).FirstOrDefault(m => m.EmployeeId == employeeId && m.Employee.Name == employee);

            if (meeting != null)
            {
                meeting.Status = status;  
                db.SaveChanges();  
            }

            return RedirectToAction("Index");  
        }
    }
}
