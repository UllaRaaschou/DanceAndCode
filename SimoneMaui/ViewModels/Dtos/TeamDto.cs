using System.Collections.ObjectModel;

namespace SimoneMaui.ViewModels.Dtos
{
    public class TeamDto
    {
        public Guid TeamId { get; set; } = Guid.Empty;
        public string Number { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SceduledTime { get; set; } = string.Empty;
        public string TeamDetailsString => $"Hold {Number} '{Name}' - {SceduledTime}";
        public ObservableCollection<DancerDto> DancersOnTeam { get; set; } = new();
        public int Count => DancersOnTeam?.Count ?? 0;
        
    }
}
