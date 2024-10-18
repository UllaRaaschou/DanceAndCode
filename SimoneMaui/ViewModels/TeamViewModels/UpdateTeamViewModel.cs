using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SimoneMaui.Navigation;
using System.Collections.ObjectModel;
using System.Text.Json;
using SimoneMaui.ViewModels.Dtos;


namespace SimoneMaui.ViewModels
{
    public partial class UpdateTeamViewModel : ObservableObject, IQueryAttributable
    {
        public INavigationService NavigationService { get; set; }

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private string? number;

        [ObservableProperty]
        private string? sceduledTime;

        [ObservableProperty]
        private TeamDto? selectedTeam;

        [ObservableProperty]
        private DancerDto? selectedDancer;

        [ObservableProperty]
        private ObservableCollection<DancerDto> dancersOnTeam;

        [ObservableProperty]
        private ObservableCollection<TeamDto> teams;

        [ObservableProperty]
        private string? teamDetailsString;

        public AsyncRelayCommand WannaAddDancerCommand { get; }
        public AsyncRelayCommand WannaDeleteDancerCommand { get; }


        public AsyncRelayCommand AddDancerToTeamCommand { get; }
        public AsyncRelayCommand DeleteDancerFromTeamCommand { get; }

        public UpdateTeamViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            WannaAddDancerCommand = new AsyncRelayCommand(WannaAddDancer, CanWannaAddDancer);
            WannaDeleteDancerCommand = new AsyncRelayCommand(WannaDeleteDancer, CanWannaDeleteDancer);
            AddDancerToTeamCommand = new AsyncRelayCommand(AddDancerToTeam, CanAddDancerToTeam);
            DeleteDancerFromTeamCommand = new AsyncRelayCommand(DeleteDancerFromTeam, CanDeleteDancerFromTeam);

        }


        private bool CanAddDancerToTeam()
        {
            if (selectedDancer != null && selectedTeam != null)
            {
                return true;
            }
            return false;
        }

        private async Task AddDancerToTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            // The cancellation token comes from the caller. You can still make a call without it.
            var request = new RestRequest($"/Teams/{SelectedTeam.TeamId}/AddToListOfDancers", Method.Put);

            request.AddJsonBody(new { dancerId = SelectedDancer.DancerId });

            var returnedStatus = await client.ExecutePutAsync<TeamDto>(request, CancellationToken.None);

            if (!returnedStatus.IsSuccessStatusCode)
            {
                JsonSerializerOptions _options = new();
                _options.PropertyNameCaseInsensitive = true;

                ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedStatus.Content ?? "{}", _options)!;
            }
            TeamDto returnedDto = returnedStatus.Data!;
            DancersOnTeam = returnedDto.DancersOnTeam;

        }

        public bool CanDeleteDancerFromTeam()
        {
            if (selectedDancer != null && selectedTeam != null)
            {
                return true;
            }
            return false;
        }

        public async Task DeleteDancerFromTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);

            var request = new RestRequest($"/Teams/{SelectedTeam.TeamId}/DeleteFromListOfDancers", Method.Delete);
            request.AddJsonBody(new { dancerId = SelectedDancer.DancerId, teamId = SelectedTeam.TeamId });

            var returnedStatus = await client.ExecuteDeleteAsync<TeamDto>(request, CancellationToken.None);
            if (returnedStatus.IsSuccessStatusCode)
            {
                TeamDto returnedDto = returnedStatus.Data!;
                DancersOnTeam = returnedDto.DancersOnTeam;

                if (!returnedStatus.IsSuccessStatusCode)
                {
                    JsonSerializerOptions _options = new();
                    _options.PropertyNameCaseInsensitive = true;

                    ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedStatus.Content ?? "{}", _options)!;
                }
            }
        }

        private bool CanWannaAddDancer()
        {
            return true;
        }

        public async Task WannaAddDancer()
        {
            await NavigationService.GoToSearchDancer(SelectedTeam);
        }

        private bool CanWannaDeleteDancer()
        {
            return true;
        }

        public async Task WannaDeleteDancer()
        {
            await NavigationService.GoToSearchDancer(SelectedTeam);
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("dancerDto") && query["dancerDto"] is DancerDto dancerDto)

            {
                SelectedDancer = dancerDto;

            }

            if (query.ContainsKey("teamDto") && query["teamDto"] is TeamDto teamDto)
            {
                SelectedTeam = teamDto;
                Name = teamDto.Name;
                Number = teamDto.Number;
                SceduledTime = teamDto.SceduledTime;
                DancersOnTeam = teamDto.DancersOnTeam;
            }
        }
    }
}