using System.ComponentModel.DataAnnotations;

namespace samp.Models
{
    public class Employees
    {
        [Key]
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
        public string Band { get; set; }
        public string Unit { get; set; }
        public string Dept { get; set; }
        public string Location { get; set; }
       
        public string PhotoUrl { get; set; }

        // Nullable DepartmentHeadId to link employees with their department head
        public string DepartmentHeadId { get; set; }
    }
}
