using Microsoft.AspNetCore.Mvc.TagHelpers;
using SimoneBlazor.Components.Services;
using SimoneBlazor.Domain;
using System.Runtime.CompilerServices;

namespace SimoneBlazor.Components.Pages
{
    public partial class TeamOverview
    {
        public List<TeamBlazor> Teams { get; set; } = default!;

        private string Title = "DCT Dans - UnderviserSide";
        protected async override Task OnInitializedAsync()
        {
            Teams = MockDataService.Teams;
        }
    }
}
