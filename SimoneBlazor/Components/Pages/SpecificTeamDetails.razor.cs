using Microsoft.AspNetCore.Components;
using SimoneBlazor.Components.Services;
using SimoneBlazor.Domain;

namespace SimoneBlazor.Components.Pages
{
    public partial class SpecificTeamDetails
    {
       
        public Guid TeamId { get; set; }

        [Parameter]
        public int Number { get; set; }

        public TeamBlazor Team { get; set; } = new TeamBlazor();
        public DancerBlazor Dancer { get; set; } = new DancerBlazor();

        public List<DancerBlazor> DancersOnTeam { get; set; } = new List<DancerBlazor>();

        protected override void OnInitialized()
        {
            Team = MockDataService.Teams.Single(t => t.Number == Number);
        }

        private void ChangeAttendanceStatus(Guid dancerId)
        {
            var dancer = Team.DancersOnTeam.SingleOrDefault(d => d.DancerId == dancerId);
            if (dancer != null)
            {
                dancer.IsAttending = !dancer.IsAttending;
            }
        }
    }
}
