using SimoneAPI.Entities;

namespace SimoneAPI.DataModels
{
    public class DancerDataModel
    {
        public Guid DancerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly TimeOfBirth { get; set; }
        public ICollection<TeamDancerRelation>? TeamDancerRelations { get; set; } = new HashSet<TeamDancerRelation>();

       



    }
}
