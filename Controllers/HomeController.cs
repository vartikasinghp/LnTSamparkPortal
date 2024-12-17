using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using samp.Data;
using samp.Models;
using System.Threading.Tasks;


namespace samp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string specificDepartmentHeadId;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        //controller for pie chart
        public IActionResult GetMeetingData()
        {
            var data = _context.Meeting
                               .GroupBy(m => m.Status)
                               .Select(g => new
                               {
                                   Status = g.Key,
                                   Count = g.Count()
                               })
                               .ToList();

            return Json(data);  // Return the data as JSON
        }


        public IActionResult Index() 
        
        {
            DateTime currentDate = DateTime.Now;
            string specificDepartmentHeadId = Convert.ToString(User.Identity.Name);

            // Fetch employees under this department head

            var employees = _context.Employees
                .Where(e => e.DepartmentHeadId.ToString()== specificDepartmentHeadId)
                .ToList();

            // Lists for different types of meetings
            var scheduledMeetings = new List<Meeting>();
            var overdueMeetings = new List<Meeting>();
            var recentlyCoveredMeetings = new List<Meeting>();
            var dueNextMonthMeetings = new List<Meeting>();

            // Fetch meetings for this department head
            var meetings = _context.Meeting
                .Include(m => m.Employee)  
                .Where(m => m.Employee.DepartmentHeadId == specificDepartmentHeadId)
                .ToList();


            // GroupBy , OrderBy => date closest to current date
            var closestMeetings = meetings
                .Where(m => m.Status == "Scheduled")
                .GroupBy(m => m.EmployeeId)
                .Select(g => g
                    .OrderBy(m => Math.Abs((m.ScheduleDate - currentDate).Days))
                    .First()
                )
                .ToList();

            
            foreach (var meeting in closestMeetings)
            {
                // Add to scheduled meetings if not overdue
                if (meeting.ScheduleDate >= currentDate)
                {
                    scheduledMeetings.Add(meeting);
                }

                // Check if the meeting is due next month
                if (meeting.ScheduleDate.Month == currentDate.AddMonths(1).Month &&
                    meeting.ScheduleDate.Year == currentDate.Year)
                {
                    dueNextMonthMeetings.Add(meeting);
                }
            }

            // Process other meeting types
            foreach (var meeting in meetings)
            {
                // Process completed meetings
                if (meeting.Status == "Completed")
                {
                    if (meeting.ScheduleDate < currentDate.AddMonths(-6))
                    {
                        overdueMeetings.Add(meeting);
                    }
                    else if (meeting.ScheduleDate >= currentDate.AddMonths(-1))
                    {
                        recentlyCoveredMeetings.Add(meeting);
                    }
                }
            }

            // Reschedule overdue meetings 
            foreach (var meeting in overdueMeetings)
            {
                meeting.Status = "Rescheduled";
                _context.Meeting.Update(meeting);
            }
            _context.SaveChanges();

            // Move recently completed overdue meetings to recently covered
            foreach (var meeting in overdueMeetings)
            {
                if (meeting.Status == "Completed" && meeting.ScheduleDate >= currentDate.AddMonths(-1))
                {
                    recentlyCoveredMeetings.Add(meeting);
                }
            }

            // filtered meeting
            var viewModel = new DashboardViewModel
            {
                ScheduledMeetings = scheduledMeetings,
                OverdueMeetings = overdueMeetings,
                RecentlyCoveredMeetings = recentlyCoveredMeetings,
                DueNextMonthMeetings = dueNextMonthMeetings
            };
            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult DownloadReport(string status = "all")
        {
                var meetingsQuery = _context.Meeting.AsQueryable();

                // Filter based on the status passed from the query string
                if (status != "all")
                {
                    meetingsQuery = meetingsQuery.Where(m => m.Status.ToLower() == status.ToLower());
                }

                // Execute the query and get the list of meetings, or an empty list if null
                var meetings = meetingsQuery.ToList() ?? new List<Meeting>();

                // Pass the filtered meetings and the selected status to the view
                ViewBag.Status = status;
                return View(meetings);
            




        }



        public IActionResult UpdateRecords()
        {
            return View();  



        }

        private class JsonRequestBehavior
        {
        }
    }

}