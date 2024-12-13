using System.Collections.ObjectModel;

namespace SimoneMaui.Models
{
    public class TeamDto
    {
        public Guid TeamId { get; set; } = Guid.Empty;
        public string Number { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ScheduledTime { get; set; } = string.Empty;
        public DayOfWeek DayOfWeek { get; set; } = default;
        public DateOnly LastDancedate { get; set; } = default;

        public bool IsTrialLesson { get; set; } = false;
        public string IfTrialLessonIsTrue => IsTrialLesson == true ? " - PRØVETIME" : "";
        public string TeamDetailsString => $"Hold {Number} '{Name}' - {ScheduledTime} {IfTrialLessonIsTrue}";
        public ObservableCollection<DancerDto> DancersOnTeam { get; set; } = new();
        public int Count => DancersOnTeam?.Count ?? 0;



    }
}
