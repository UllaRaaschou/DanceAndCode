using Microsoft.AspNetCore.Mvc.TagHelpers;
using RestSharp;
using SimoneBlazor.Components.Services;
using SimoneBlazor.Domain;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimoneBlazor.Components.Pages
{
    public partial class TeamOverview
    {
        public List<TeamBlazor> Teams { get; set; } = new();

        private string Title = "DCT Dans - UnderviserSide";
        protected async override Task OnInitializedAsync()
        {
            //Teams = MockDataService.Teams;

            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            var request = new RestRequest("/teams/all", Method.Get);           

            var teamCollection = await client.GetAsync<List<TeamBlazor>>(request, CancellationToken.None);
            Teams = teamCollection;
        }   

    }
}
