using Microsoft.AspNetCore.Components;
using RestSharp;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Entities;
using SimoneAPI.Tobe.Features.Dancer;
using SimoneBlazor.Components.Services;
using SimoneBlazor.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimoneBlazor.Components.Pages
{
    public partial class SpecificTeamDetails
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } 

        [Parameter]
        public Guid TeamId { get; set; }
        public TeamBlazor? Team { get; set; } = new TeamBlazor();

        public List<RelationBlazor>? Relations = new List<RelationBlazor>();

        public List<DateOnly>? TeamDanceDates = new List<DateOnly>();

        protected async override Task OnInitializedAsync()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);

            var request1 = new RestRequest($"/Teams/{TeamId}", Method.Get);
            Team = await client.GetAsync<TeamBlazor>(request1, CancellationToken.None);

            var request2 = new RestRequest($"/Relations/{TeamId}", Method.Get);
            Relations = await client.GetAsync<List<RelationBlazor>>(request2, CancellationToken.None);

            var request3 = new RestRequest($"/Teams/{TeamId}/DanceDates", Method.Get);
            TeamDanceDates = await client.GetAsync<List<DateOnly>>(request3, CancellationToken.None);

            // Filter dates to only include today and future dates
            var today = DateOnly.FromDateTime(DateTime.Today);
            TeamDanceDates = TeamDanceDates.Where(date => date >= today).ToList();

            foreach (var relation in Relations)
            {
                if (!relation.Attendances.Any())
                {
                    relation.Attendances = new List<Attendance>();
                    foreach (var date in TeamDanceDates)
                    {
                        relation.Attendances.Add(new Attendance
                        {
                            AttendanceId = Guid.NewGuid(),
                            Date = date,
                            IsPresent = false,
                            Note = string.Empty,
                            DancerId = relation.DancerId,
                            TeamId = relation.TeamId,
                        });
                    }
                }
                else
                {
                    // Filter attendances to only include today and future dates
                    relation.Attendances = relation.Attendances.Where(att => att.Date >= today).ToList();
                }
            }
        }

        public async Task<AttendanceBlazor> GetAttendance(Guid dancerId, Guid teamId, DateOnly date)
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);

            var request = new RestRequest($"/Attendances/{teamId}/{dancerId}/{date}", Method.Get);
            var att = client.GetAsync<AttendanceBlazor>(request, CancellationToken.None).Result;
            return att;
        }

        public async Task ChangeAttendanceStatus(Guid dancerId, DateOnly date, bool isPresent)
        {
            var relation = Relations.FirstOrDefault(r => r.DancerId == dancerId && r.TeamId == TeamId);
            if (relation != null)
            {
                var attendance = relation.Attendances.FirstOrDefault(a => a.Date == date);
                if (attendance != null)
                {
                    attendance.IsPresent = !attendance.IsPresent;
                    StateHasChanged();
                }
            }
        }

        public void SaveChanges()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);

            var teamDancerRelations = Relations.Select(r => ToTeamDancerRelation(r)).ToList();

            var request = new RestRequest($"/Relations/{TeamId}", Method.Put);
            request.AddJsonBody(teamDancerRelations);

            client.PutAsync(request, CancellationToken.None);

            // Navigate to the home page after saving changes
            NavigationManager.NavigateTo("/");
        }

        private TeamDancerRelation ToTeamDancerRelation(RelationBlazor relationBlazor)
        {
            return new TeamDancerRelation
            {
                TeamId = relationBlazor.TeamId,
                DancerId = relationBlazor.DancerId,
                IsTrialLesson = relationBlazor.IsTrialLesson,
                LastDanceDate = relationBlazor.DancersLastDanceDate,
                Attendances = relationBlazor.Attendances
            };
        }
    }
}
