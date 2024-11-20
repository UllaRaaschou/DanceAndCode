using SimoneAPI.Profiles;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace SimoneAPI.DataModels

{
    public class TeamDancerRelation
    {
        //[Key]
        //public Guid? TeamDancerRelationId { get; set; }
        public Guid TeamId { get; set; }
        public Guid DancerId { get; set; }


        [JsonIgnore]
        public TeamDataModel TeamDataModel { get; set; } = null!;

        [JsonIgnore]
        public DancerDataModel DancerDataModel { get; set; } = null!;


        public bool IsTrialLesson { get; set; } = false;
        public DateOnly LastDanceDate { get; set; } = DateOnly.MinValue;
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

        public TeamDancerRelation() { }
        

    }
}
