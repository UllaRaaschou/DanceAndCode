using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace SimoneAPI.DataModels
{
    public class Attendance
    {
        public Guid AttendanceId { get; set; }
        
        public DateOnly Date { get; set; }
        public bool IsPresent { get; set; } = false;
        public string Note { get; set; } = string.Empty;



        [JsonIgnore]
        // Navigation property
        public TeamDancerRelation TeamDancerRelation { get; set; }

        // Foreign key properties
        public Guid DancerId { get; set; }
        public Guid TeamId { get; set; }

        
    }
}
