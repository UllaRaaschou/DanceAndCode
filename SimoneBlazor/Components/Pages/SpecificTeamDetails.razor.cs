using Microsoft.AspNetCore.Components;
using SimoneBlazor.Components.Services;
using SimoneBlazor.Domain;

namespace SimoneBlazor.Components.Pages
{
    public partial class SpecificTeamDetails
    {
        [Parameter]
        public Guid TeamId { get; set; }

        public TeamBlazor Team { get; set; } = new TeamBlazor();

        protected override void OnInitialized()
        {
            Team = MockDataService.TeamBlazors.Single(t => t.TeamId == TeamId);
        }
    }
}
