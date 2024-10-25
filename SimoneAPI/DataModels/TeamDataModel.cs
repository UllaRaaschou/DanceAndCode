using SimoneAPI.DbContexts;
using SimoneAPI.Entities;

namespace SimoneAPI.DataModels
{
    public class TeamDataModel
    {
        public Guid TeamId { get; set; }
        public int Number { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string ScheduledTime { get; set; } = string.Empty;

        public ICollection<TeamDancerRelation> TeamDancerRelations { get; set; } = new HashSet<TeamDancerRelation>();
        
    }



}
