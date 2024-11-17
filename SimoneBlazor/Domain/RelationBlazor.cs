using SimoneAPI.DataModels;

namespace SimoneBlazor.Domain
{
    public class RelationBlazor
    {    
        public Guid TeamId { get; set; }
        public Guid DancerId { get; set; }
        public string DancerName { get; set; } = string.Empty;
        public DateOnly DancersLastDanceDate { get; set; }
        
        public bool IsChecked { get; set; }

        public List<Attendance> Attendances { get; set; } = new List<Attendance>();

        public Dictionary<DateOnly, AttendanceBlazor> GetADateToAttendanceDictionary()
        {
            return Attendances.ToDictionary(a => a.Date, a => new AttendanceBlazor
            {
                AttendanceId = a.AttendanceId,
                Date = a.Date,
                IsPresent = a.IsPresent,
                Note = a.Note
            });
        }



    }
}
