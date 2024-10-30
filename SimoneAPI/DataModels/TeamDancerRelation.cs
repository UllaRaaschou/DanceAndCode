using SimoneAPI.Profiles;
using System.ComponentModel.DataAnnotations;
namespace SimoneAPI.DataModels

{
    public class TeamDancerRelation
    {
        //[Key]
        //public Guid? TeamDancerRelationId { get; set; }
        public Guid TeamId { get; set; }
        public Guid DancerId { get; set; }
        public TeamDataModel TeamDataModel { get; set; } = null!;
        public DancerDataModel DancerDataModel { get; set; } = null!;
        public bool IsTrialLesson { get; set; } = false;
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

        public TeamDancerRelation() { }
        

    }
}
