using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SimoneMaui.Navigation;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SimoneMaui.Models;

namespace SimoneMaui.ViewModels
{
    public partial class SearchTeamViewModel: ObservableValidator, IQueryAttributable, INotifyPropertyChanged
    {
        public INavigationService NavigationService { get; set; }

        private readonly NavigationManager _navigationManager;
        [Range(1, 200, ErrorMessage = "Holdnummer skal være mellem 1 og 200")]       
        [NotifyPropertyChangedFor(nameof(IsTeamNameEntryEnabled))]
        [NotifyPropertyChangedFor(nameof(IsTeamNumberEntryEnabled))]
        [NotifyCanExecuteChangedFor(nameof(SearchTeamCommand))]
        [ObservableProperty]
        private int? teamNumberEntry;

        public string Number
        {
            get => teamNumberEntry?.ToString() ?? string.Empty;
            set => teamNumberEntry = int.TryParse(value, out var number) ? number : (int?)null;
        }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Navn må fylde mellem 2 og 50 tegn")]
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

        //[ObservableProperty]
        //private string number = string.Empty;

        [ObservableProperty]
        private string scheduledTime = string.Empty;

        [ObservableProperty]
        private string teamDetailsString = string.Empty;

        public bool IsTeamNumberEntryEnabled => string.IsNullOrWhiteSpace(TeamNameEntry);
        public bool IsTeamNameEntryEnabled => TeamNumberEntry == null;

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

        [ObservableProperty]
        private bool wannaAddTrialLessonToADancer = false;






        public RelayCommand TeamSelectedCommand { get; }
        public AsyncRelayCommand WannaUpdateTeamCommand { get; }
        public AsyncRelayCommand WannaDeleteTeamCommand { get; }        
        public AsyncRelayCommand SearchTeamCommand { get; }
        public AsyncRelayCommand NavigateToFirstPageCommand { get; set; }
        public AsyncRelayCommand NavigateBackCommand { get; }
        public AsyncRelayCommand NavigateForwardCommand { get; }
       
        //public async Task NavigateToFirstPage()
        //{
        //    await NavigationService.GoToFirstPage();
        //}





        public SearchTeamViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            _navigationManager = new NavigationManager(navigationService);
            SearchTeamCommand = new AsyncRelayCommand(SearchTeam, CanSearchTeam);
            WannaUpdateTeamCommand = new AsyncRelayCommand(WannaUpdateTeam, CanWannaUpdateTeam);
            WannaDeleteTeamCommand = new AsyncRelayCommand(WannaDeleteTeam, CanWannaDeleteTeam);
            TeamSelectedCommand = new RelayCommand(TeamSelected);
            NavigateBackCommand = new AsyncRelayCommand(_navigationManager.NavigateBack, _navigationManager.CanNavigateBack);
            NavigateForwardCommand = new AsyncRelayCommand(_navigationManager.NavigateForward, _navigationManager.CanNavigateForward);
            NavigateToFirstPageCommand = new AsyncRelayCommand(_navigationManager.NavigateToFirstPage);
        }


        private void TeamSelected()
        {
            OnSelectedTeamChanged();            
        }
        private async void OnSelectedTeamChanged()
        {
            TeamNameEntry = SelectedTeam.Name;
            TeamNumberEntry = int.TryParse(SelectedTeam.Number, out var teamNumberEntry)? teamNumberEntry:(int?)null;
            ScheduledTime = SelectedTeam.ScheduledTime;
            IsUpdateButtonVisible = true;
            IsDeleteButtonVisible = true;
            SearchResultVisible = false;
            IsSearchHeaderVisible = false;
            if(WannaAddTeamToADancer == true || WannaAddTrialLessonToADancer == true) 
            {
                await NavigationService.GoToUpdateDancer(SelectedDancer, SelectedTeam, WannaAddTeamToADancer, WannaAddTrialLessonToADancer);
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
            return (!string.IsNullOrEmpty(TeamNameEntry) || TeamNumberEntry != null);           
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

            if (TeamNumberEntry != null)
            {
                request.AddOrUpdateParameter("number", Number);
            }

            var teamCollection = await client.GetAsync<ObservableCollection<TeamDto>>(request, CancellationToken.None);

            TeamNameEntry = string.Empty;
            TeamNumberEntry = null;

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
            TeamNumberEntry = null;

            
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

            if (query.ContainsKey("WannaAddTrialLessonToADancer") && query["WannaAddTrialLessonToADancer"] is bool wannaAddTrialLessonToADancer)
            {
                WannaAddTrialLessonToADancer = wannaAddTrialLessonToADancer;
            }
        }

    }
}
