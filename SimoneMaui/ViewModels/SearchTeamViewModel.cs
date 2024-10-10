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

        private TeamDto selectedTeam;
        public TeamDto SelectedTeam
        {
            get => selectedTeam;
            set => SetProperty(ref selectedTeam, value);            
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



        [RelayCommand]
        private async Task NavigateToUpdateDancer()
        {
            if (SelectedTeam is not null && SelectedDancer is not null)
            {
                await NavigationService.GoToUpdateDancer(SelectedDancer, SelectedTeam);
            }
        }


        public SearchTeamViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            SearchTeamCommand = new RelayCommand(async () => await SearchTeam(), CanSearchTeam);
        }

        //[RelayCommand]
        //public async Task Tusse()
        //{
        //    await CanSearchTeam();
        //    await SearchTeam();
        //}



        private bool CanSearchTeam()
        {
            return true;
            //if(!string.IsNullOrEmpty(teamNameEntry) || !string.IsNullOrEmpty(teamNumberEntry))
            //{
            //    return true;
            //}
            //return false;
        }
        
        private async Task<ObservableCollection<TeamDto>> SearchTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            var request = new RestRequest("/teams/SearchForTeamByNameOrNumber", Method.Get);

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

            if (!returnedCollection.Data.Any() || returnedCollection.Data == null)
            {
                return null;
            }

            var teamCollection = new ObservableCollection<TeamDto>(returnedCollection.Data);

            if (teamCollection.Count > 0)
            {
                TeamDtoCollection.Clear();
                foreach (var item in teamCollection)
                {
                    TeamDtoCollection.Add(item);

                }
            }
            return TeamDtoCollection;
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
