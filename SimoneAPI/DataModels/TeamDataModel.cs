﻿
using SimoneAPI.DbContexts;

namespace SimoneAPI.DataModels
{
    public class TeamDataModel
    {
        private CalendarDataModel _calendarDataModel;

        public Guid TeamId { get; set; }
        public int Number { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string ScheduledTime { get; set; } = string.Empty;
        public DayOfWeek DayOfWeek { get; set; } = default;    

        public ICollection<TeamDancerRelation> TeamDancerRelations { get; set; } = new HashSet<TeamDancerRelation>();

        public TeamDataModel() { }

        public TeamDataModel(CalendarDataModel calendarDataModel) 
        {
            _calendarDataModel = calendarDataModel;
        }   
    }
}
    