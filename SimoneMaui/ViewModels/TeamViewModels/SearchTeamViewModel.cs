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



        [ObservableProperty]
        private DancerDto? selectedDancer;

        [NotifyCanExecuteChangedFor(nameof(WannaUpdateTeamCommand))]
        [NotifyCanExecuteChangedFor(nameof(WannaDeleteTeamCommand))]
        [ObservableProperty]
        private TeamDto? selectedTeam;        

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

        [ObservableProperty]
        private bool searchResultVisible = true;

        [ObservableProperty]
        private bool wannaAddTeamToADancer = false;





        public RelayCommand TeamSelectedCommand { get; }
        public AsyncRelayCommand WannaUpdateTeamCommand { get; }
        public AsyncRelayCommand WannaDeleteTeamCommand { get; }        
        public AsyncRelayCommand SearchTeamCommand { get; }
       
                


        public SearchTeamViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            SearchTeamCommand = new AsyncRelayCommand(SearchTeam, CanSearchTeam);
            WannaUpdateTeamCommand = new AsyncRelayCommand(WannaUpdateTeam, CanWannaUpdateTeam);
            WannaDeleteTeamCommand = new AsyncRelayCommand(WannaDeleteTeam, CanWannaDeleteTeam);
            TeamSelectedCommand = new RelayCommand(TeamSelected);
        }


        private void TeamSelected()
        {
            OnSelectedTeamChanged();            
        }
        private async void OnSelectedTeamChanged()
        {
            TeamNameEntry = SelectedTeam.Name;
            TeamNumberEntry = SelectedTeam.Number.ToString();
            SceduledTime = SelectedTeam.SceduledTime;
            IsUpdateButtonVisible = true;
            IsDeleteButtonVisible = true;
            SearchResultVisible = false;
            IsSearchHeaderVisible = false;
            if(WannaAddTeamToADancer == true) 
            {
                await NavigationService.GoToUpdateDancer(SelectedDancer, SelectedTeam);
            }
        }

        private bool CanWannaUpdateTeam()
        {
            return SelectedTeam != null;
        }
        private async Task WannaUpdateTeam()
        {
            await NavigationService.GoToUpdateTeam(SelectedTeam);
        }
              


        private bool CanWannaDeleteTeam()
        {
            return SelectedTeam != null;
        }
        private async Task WannaDeleteTeam()
        {
            await NavigationService.GoToDeleteTeam(SelectedTeam);
        }

        //private bool CanNavigateToDeleteTeam()
        //{
        //    return (SelectedTeam != null);
        //}
        //private async Task NavigateToDeleteTeam()
        //{
        //    await NavigationService.GoToDeleteTeam(SelectedTeam);
        //}

       

        private bool CanSearchTeam()
        {
            return (!string.IsNullOrEmpty(TeamNameEntry) || !string.IsNullOrEmpty(TeamNumberEntry));           
        }

        public event Action<string> TeamNotFound;

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

            var teamCollection = await client.GetAsync<ObservableCollection<TeamDto>>(request, CancellationToken.None);

            TeamNameEntry = string.Empty;
            TeamNumberEntry = string.Empty;

            //var teamCollection = new ObservableCollection<TeamDto>(returnedCollection);

            if (teamCollection?.Count == 0)
            {
                // Ingen hold fundet i databasen
                TeamNotFound?.Invoke("Ingen hold i databasen matcher denne søgning");
                ReloadSearchPage(); // Genindlæs søgesiden
                return;
            }

            if (teamCollection?.Count > 0)
            {
                TeamDtoCollection.Clear();
                foreach (var item in teamCollection)
                {
                    TeamDtoCollection.Add(item);
                }
            }         
            IsSearchHeaderVisible = true;
        }

        private void ReloadSearchPage()
        {
            
            TeamNameEntry = string.Empty;
            TeamNumberEntry = string.Empty;

            
            IsSearchHeaderVisible = false;
            IsUpdateButtonVisible = false;
            IsDeleteButtonVisible = false;
            SearchResultVisible = true; // Gør søge-resultater synlige igen
        }


        partial void OnTeamNameEntryChanged(string? newValue)
        {
            TeamNameEntry = ToTitleCase(newValue);
        }
        public void UpdateTeamNameEntry(string? newValue)
        {
            TeamNameEntry = ToTitleCase(newValue); // Anvend Title Case konvertering
        }
        private string ToTitleCase(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            var cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(input.ToLower());
        }


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("dancerDto") && query["dancerDto"] is DancerDto dancerDto)
            {
                SelectedDancer = dancerDto;
            }

            if (query.ContainsKey("WannaAddTeamToADancer") && query["WannaAddTeamToADancer"] is bool wannaAddTeamToADancer)
            {
                WannaAddTeamToADancer = wannaAddTeamToADancer;
            }
        }

    }
}
