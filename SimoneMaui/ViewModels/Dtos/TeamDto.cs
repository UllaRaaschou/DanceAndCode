using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimoneMaui.ViewModels.Dtos
{
    public class TeamDto
    {
        public Guid TeamId { get; set; } = Guid.Empty;
        public int Number { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string SceduledTime { get; set; } = string.Empty;
        public string TeamDetailsString => $"Hold {Number} '{Name}' - {SceduledTime}";

        public ObservableCollection<DancerDto> DancersOnTeam = new();

    }
}
