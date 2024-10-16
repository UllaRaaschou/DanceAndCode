using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SimoneMaui.Navigation;
using System.Collections.ObjectModel;
using System.Text.Json;

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
        private string? dayOfWeek;

        [ObservableProperty]
        private string? startAndEndTime;

        [ObservableProperty]
        private TeamDto? selectedTeam;

        [ObservableProperty]
        private DancerDto? selectedDancer;

        [ObservableProperty]
        private ObservableCollection<TeamDto> teams;

        public AsyncRelayCommand WannaAddDancerCommand { get; }

       
        public AsyncRelayCommand AddDancerToTeamCommand;

        public UpdateTeamViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            WannaAddDancerCommand = new AsyncRelayCommand(WannaAddDancer, CanWannaAddDancer);
            AddDancerToTeamCommand = new AsyncRelayCommand(AddDancerToTeam, CanAddDancerToTeam);
          
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

            var returnedStatus = await client.ExecutePutAsync(request, CancellationToken.None);

            if (!returnedStatus.IsSuccessStatusCode)
            {
                JsonSerializerOptions _options = new();
                _options.PropertyNameCaseInsensitive = true;

                ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedStatus.Content ?? "{}", _options)!;

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
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}
