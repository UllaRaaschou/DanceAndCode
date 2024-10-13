using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SimoneMaui.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimoneMaui.ViewModels
{
    public partial class SearchTeamViewModel: ObservableObject
    {
        public INavigationService NavigationService { get; set; }

        [ObservableProperty]
        private DancerDto? selectedDancer = null;

        public RelayCommand SearchTeamCommand { get; }

        [ObservableProperty]
        private ObservableCollection<TeamDto> teamDtoCollection = new();

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private int number = 0;

        [ObservableProperty]
        private string teamDetailsString = string.Empty;

        public RelayCommand NavigateToUpdateDancerCommand { get; }

        private TeamDto teamToAdd;
        public TeamDto TeamToAdd
        {
            get => teamToAdd;
            set
            {
                SetProperty(ref teamToAdd, value);
                if (value is not null)
                {
                    HandleSelectedTeamChanged();
                }
            }
        }

        private async Task HandleSelectedTeamChanged()
        {
            if (TeamToAdd is not null)
            {
                await NavigateToUpdateDancer();
            }
        }

        private string? teamNumberEntry;
        public string? TeamNumberEntry
        {
            get => teamNumberEntry;
            set
            {
                SetProperty(ref teamNumberEntry, value);
                UpdateEntryStates();
               
            }
        }

        private string? teamNameEntry;
        public string? TeamNameEntry
        {
            get => teamNameEntry;
            set
            {
                SetProperty(ref teamNameEntry, value);
                UpdateEntryStates();
               
            }
        }

        private bool isTeamNumberEntryEnabled = true;
        public bool IsTeamNumberEntryEnabled
        {
            get => isTeamNumberEntryEnabled;
            set
            {
                SetProperty(ref isTeamNumberEntryEnabled, value);   
               
            }
        }

        private bool isTeamNameEntryEnabled = true;
        public bool IsTeamNameEntryEnabled
        {
            get => isTeamNameEntryEnabled;
            set
            {
                SetProperty(ref isTeamNameEntryEnabled, value);
            }
        }
        private void UpdateEntryStates()
        {

            IsTeamNumberEntryEnabled = string.IsNullOrWhiteSpace(TeamNameEntry);

            IsTeamNameEntryEnabled = string.IsNullOrWhiteSpace(TeamNumberEntry);
        }

        private bool CanNavigateToUpdateDancer()
        {
            return TeamToAdd is not null && SelectedDancer is not null;
        }

       
        private async Task NavigateToUpdateDancer()
        {            
            await NavigationService.GoToUpdateDancer(SelectedDancer, TeamToAdd);
        }


        public SearchTeamViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            SearchTeamCommand = new RelayCommand(async () => await SearchTeam(), CanSearchTeam);
            NavigateToUpdateDancerCommand = new RelayCommand(async () => await NavigateToUpdateDancer(), CanNavigateToUpdateDancer);
        }

        private bool CanSearchTeam()
        {
            return true;
           
        }
        
        private async Task SearchTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            var request = new RestRequest("/teams", Method.Get);

            if (TeamNameEntry != null)
            {
                request.AddOrUpdateParameter("name", TeamNameEntry);

            }

            if (TeamNumberEntry != null)
            {
                request.AddOrUpdateParameter("number", TeamNumberEntry);
            }

            var returnedCollection = await client.ExecuteGetAsync<List<TeamDto>>(request, CancellationToken.None);

            if (!returnedCollection.IsSuccessStatusCode)
            {
                JsonSerializerOptions _options = new();
                _options.PropertyNameCaseInsensitive = true;

                ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedCollection.Content ?? "{}", _options)!;

            }

            TeamNameEntry = string.Empty;
            TeamNumberEntry = string.Empty;

            var teamCollection = new ObservableCollection<TeamDto>(returnedCollection.Data);

            if (teamCollection.Count > 0)
            {
                TeamDtoCollection.Clear();
                foreach (var item in teamCollection)
                {
                    TeamDtoCollection.Add(item);

                }
            }
            
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("dancerDto") && query["dancerDto"] is DancerDto dancerDto)
            {
                SelectedDancer = dancerDto;
            }
        }

    }
}
