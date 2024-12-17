namespace samp.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            this.ScheduledMeetings = new List<Meeting>();
            this.OverdueMeetings = new List<Meeting>();
            this.RecentlyCoveredMeetings = new List<Meeting>();
            this.CoveredMeetings = new List<Meeting>();
            this.Meeting = new List<Meeting>();
            this.DueNextMonthMeetings = new List<Meeting>();
        }
        public List<Meeting> ScheduledMeetings { get; set; }
        public List<Meeting> OverdueMeetings { get; set; }
        public List<Meeting> RecentlyCoveredMeetings { get; set; }
        public List<Meeting> CoveredMeetings { get; set; }
        public List<Meeting> Meeting { get; set; }
        public List<Meeting> DueNextMonthMeetings { get; set; }
    }
}
