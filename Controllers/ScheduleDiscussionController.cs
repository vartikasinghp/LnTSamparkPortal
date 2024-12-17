using Microsoft.AspNetCore.Mvc;
using samp.Data;
using samp.Models;
using System.Linq;

namespace samp.Controllers
{
    public class ScheduleDiscussionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScheduleDiscussionController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult ScheduleDiscussion()
        {
            /* var employees = _context.Employees.ToList();


              ViewBag.Employees = employees;


              return View(new Meeting());*/




            var loggedInEmployeeId = User.Identity.Name;

             

            if (loggedInEmployeeId == null)
            {

                ViewBag.Employees = new List<Employees>();
                return View(new Meeting());
            }


            var employeesUnderHead = _context.Employees
                                             .Where(e => e.DepartmentHeadId == loggedInEmployeeId)
                                             .ToList();

            // employee list -> view
            ViewBag.Employees = employeesUnderHead;

            return View(new Meeting());
        }

    


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ScheduleDiscussion(Meeting model)
        {
            if(model.Status == null)
            {
                model.Status = "Scheduled";
            }
            
            if (ModelState.IsValid)
            {
                
              

                _context.Meeting.Add(model);

             
                _context.SaveChanges();

              
                return RedirectToAction("Index", "Home");
            }

            
            ViewBag.Employees = _context.Employees.ToList();
            return View(model);
        }
    }
}
