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
    public partial class SearchTeamViewModel: ObservableObject, IQueryAttributable
    {
        public INavigationService NavigationService { get; set; }

        [ObservableProperty]
        private DancerDto? selectedDancer;

        
        private TeamDto selectedTeam;
        public TeamDto SelectedTeam
        {
            get => selectedTeam;
            set
            {
                SetProperty(ref selectedTeam, value);
                if (value != null)
                {
                    
                }
            }
        }

        private void OnSelectedTeamChanged()
        {
            if (selectedTeam != null)
            {
                SelectedTeam = selectedTeam;
                TeamNameEntry = selectedTeam.Name;
                TeamNumberEntry = selectedTeam.Number.ToString();
                SceduledTime = selectedTeam.SceduledTime;
                IsUpdateButtonVisible = true;
                IsDeleteButtonVisible = true;
                SearchResultVisible = false;
                IsSearchHeaderVisible = false;
            }
        }

        [ObservableProperty]
        private ObservableCollection<TeamDto> teamDtoCollection = new();

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string number = string.Empty;

        [ObservableProperty]
        private string sceduledTime = string.Empty;

        [ObservableProperty]
        private string teamDetailsString = string.Empty;

        public bool IsTeamNumberEntryEnabled => string.IsNullOrWhiteSpace(TeamNameEntry);
        public bool IsTeamNameEntryEnabled => string.IsNullOrWhiteSpace(TeamNumberEntry);

        [ObservableProperty]
        private bool isSearchHeaderVisible = false;

        [ObservableProperty]
        private bool isUpdateButtonVisible = false;        

        [ObservableProperty]
        private bool isDeleteButtonVisible = false;
      
        private bool searchResultVisible = true;
        public bool SearchResultVisible
        {
            get => searchResultVisible;
            set => SetProperty(ref searchResultVisible, value);
        }

        [ObservableProperty]
        private bool searchHasRun = false;


        public AsyncRelayCommand NavigateToUpdateDancerCommand { get; }
        public AsyncRelayCommand WannaUpdateTeamCommand { get; }

        public AsyncRelayCommand WannaDeleteTeamCommand { get; }
        public AsyncRelayCommand SearchTeamCommand { get; }

        public AsyncRelayCommand TeamSelectedCommand{ get; }


        [NotifyPropertyChangedFor(nameof(IsTeamNameEntryEnabled))]
        [NotifyPropertyChangedFor(nameof(IsTeamNumberEntryEnabled))]
        [NotifyCanExecuteChangedFor(nameof(SearchTeamCommand))]
        [ObservableProperty]
        private string? teamNumberEntry;

        [NotifyPropertyChangedFor(nameof(IsTeamNumberEntryEnabled))]
        [NotifyPropertyChangedFor(nameof(IsTeamNameEntryEnabled))]
        [NotifyCanExecuteChangedFor(nameof(SearchTeamCommand))]
        [ObservableProperty]
        private string? teamNameEntry;


        public SearchTeamViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            SearchTeamCommand = new AsyncRelayCommand(SearchTeam, CanSearchTeam);
            NavigateToUpdateDancerCommand = new AsyncRelayCommand(NavigateToUpdateDancer, CanNavigateToUpdateDancer);
            WannaUpdateTeamCommand = new AsyncRelayCommand(WannaUpdateTeam, CanWannaUpdateTeam);
            TeamSelectedCommand = new AsyncRelayCommand(TeamSelected);
        }

        private async Task TeamSelected()
        {
            if (selectedTeam != null)
            {
                OnSelectedTeamChanged();
            }
        }

        private bool CanNavigateToUpdateDancer()
        {
            return (selectedTeam != null && selectedDancer != null);
        }       
        private async Task NavigateToUpdateDancer()
        {            
            await NavigationService.GoToUpdateDancer(selectedDancer, selectedTeam);
        }

        private bool CanWannaUpdateTeam() 
        {
            return SearchHasRun;
        }        
        private async Task WannaUpdateTeam() 
        {
            await NavigationService.GoToUpdateTeam(SelectedTeam);
        }


        private bool CanWannaDeleteteam()
        {
            return SearchHasRun;
        }
        private async Task WannaDeleteTeam()
        {
            await NavigationService.GoToDeleteTeam(SelectedTeam);
        }



        private bool CanSearchTeam()
        {
            return (!string.IsNullOrEmpty(TeamNameEntry) || !string.IsNullOrEmpty(TeamNumberEntry)) ;
           
        }        

        private async Task SearchTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            var request = new RestRequest("/teams", Method.Get);

            if (!string.IsNullOrEmpty(TeamNameEntry))
            {
                request.AddOrUpdateParameter("name", TeamNameEntry);

            }

            if (!string.IsNullOrEmpty(TeamNumberEntry))
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
           
            SearchHasRun = true;
            IsSearchHeaderVisible = true;

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
