using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimoneMaui.ViewModels.Dtos
{
    public class UpdateDancerDto
    {
        public Guid DancerId { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string TimeOfBirth { get; set; } = string.Empty;
        public ObservableCollection<TeamDto> Teams { get; set; } = new ObservableCollection<TeamDto>();
    }
}
