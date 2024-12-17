using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace samp.Models
{
    public class Meeting
    {
        public int MeetingId { get; set; }
        public string EmployeeId { get; set; }   //foreign key
        [ValidateNever]
        public Employees Employee { get; set; } 
        

       


        public DateTime ScheduleDate { get; set; }

        public string MeetingSubject { get; set; }
        [ValidateNever]
        public string Status { get; set; }   

        public DateTime StartTime { get; set; }  
        public DateTime EndTime { get; set; }
        public string? DepartmentHeadId { get; set; }

        public string GetEmployeePhotoUrl()
        {
            return Employee?.PhotoUrl;
        }
       



    }
}





























