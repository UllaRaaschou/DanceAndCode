namespace SimoneAPI.DataModels
{
    public class Attendance
    {
        public Guid AttendanceId { get; set; }
        public Guid TeamDancerRelationId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; } = false;
        public string Note { get; set; } = string.Empty;
        public TeamDancerRelation TeamDancerRelation { get; set; } = new TeamDancerRelation();
    }
}
