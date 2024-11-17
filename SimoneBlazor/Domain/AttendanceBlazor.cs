namespace SimoneBlazor.Domain
{
    public class AttendanceBlazor
    {
        public Guid AttendanceId { get; set; }
        public DateOnly Date { get; set; }
        public bool IsPresent { get; set; } = false;
        public string Note { get; set; } = string.Empty;
    }
}