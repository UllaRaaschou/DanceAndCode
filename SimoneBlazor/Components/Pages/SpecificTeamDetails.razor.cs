﻿using Microsoft.AspNetCore.Components;
using RestSharp;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneBlazor.Components.Services;
using SimoneBlazor.Domain;

namespace SimoneBlazor.Components.Pages
{
    public partial class SpecificTeamDetails
    {

        [Parameter]
        public Guid TeamId { get; set; }
        public TeamBlazor? Team { get; set; } = new TeamBlazor();

        public List<RelationBlazor>? Relations = new List<RelationBlazor>();

        public List<DateOnly>? TeamDanceDates = new List<DateOnly>();
        

       

        protected async override void OnInitialized()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);

            var request1 = new RestRequest("/Teams/TeamId}", Method.Get);
            Team = await client.GetAsync<TeamBlazor>(request1, CancellationToken.None);

            var request2 = new RestRequest("/Relations/{TeamId}", Method.Get);
            Relations = await client.GetAsync<List<RelationBlazor>>(request2, CancellationToken.None);

            var request3 = new RestRequest("/danceDates/{TeamId}", Method.Get);
            TeamDanceDates = await client.GetAsync<List<DateOnly>>(request3, CancellationToken.None);

          
        }


        public void ChangeAttendanceStatus (Guid dancerId, DateOnly date) 
        {
            var relation = Relations.FirstOrDefault(r => r.DancerId == dancerId);
            if(relation != null && relation.Attendances.ContainsKey(date)) 
            {
                relation.Attendances[date] = !relation.Attendances[date];
            }
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
