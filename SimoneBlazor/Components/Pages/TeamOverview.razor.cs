using SimoneBlazor.Components.Services;
using SimoneBlazor.Domain;
using System.Runtime.CompilerServices;

namespace SimoneBlazor.Components.Pages
{
    public partial class TeamOverview
    {
        public List<TeamBlazor> Teams { get; set; } = default!;
        protected async override Task OnInitializedAsync()
        {
            Teams = MockDataService.TeamBlazors;
        }
    }
}
