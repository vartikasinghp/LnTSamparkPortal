using samp.Models;
using System;
using System.Collections.Generic;

namespace samp.Models
{
    public class HomePageViewModel
    {
        public List<Meeting> ScheduledMeetings { get; set; }
        public List<Meeting> OverdueMeetings { get; set; }
        public List<Meeting> DueNextMonthMeetings { get; set; }
        public List<Meeting> RecentlyCoveredMeetings { get; set; }
    }
}
