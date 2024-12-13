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
    public partial class SearchDancerViewmodel: ObservableValidator, IQueryAttributable, INotifyPropertyChanged
    {
        public INavigationService NavigationService { get; set; }

        private readonly NavigationManager _navigationManager;
        [ObservableProperty]
        private ObservableCollection<DancerDto> dancerDtoList = new ObservableCollection<DancerDto>();

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Navn må fylde mellem 2 og 50 tegn")]
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SearchDancerCommand))]
        private string? nameEntry = string.Empty;

        [ObservableProperty]
        private string? name = string.Empty;

        [ObservableProperty]
        private string? timeOfBirth = string.Empty;

        [ObservableProperty]
        private TeamDto? selectedTeam;

        [ObservableProperty]
        private ObservableCollection<TeamDto> teams = new ObservableCollection<TeamDto>();

        [ObservableProperty]
        private string teamDetailsString= string.Empty;

        [ObservableProperty] private bool puttingDancerOnTeam;
        [ObservableProperty] private bool addTrialDancerToTeam;





        public AsyncRelayCommand DancerSelectedCommand { get; }
        public AsyncRelayCommand WannaUpdateDancerCommand { get;}        
        public AsyncRelayCommand WannaDeleteDancerCommand { get; }
        public AsyncRelayCommand SearchDancerCommand { get; }
       

        public RelayCommand ChooseNavigationCommand { get; }

        [ObservableProperty]
        private bool searchResultVisible = true;

        [NotifyCanExecuteChangedFor(nameof(WannaUpdateDancerCommand))]
        [NotifyCanExecuteChangedFor(nameof(WannaDeleteDancerCommand))]      
        [ObservableProperty]
        private DancerDto? selectedDancer;

        [ObservableProperty]
        private bool isUpdateButtonVisible;

        [ObservableProperty]
        private bool isDeleteButtonVisible;

        [ObservableProperty]
        private bool isSearchHeaderVisible;

        public event Action<string> NoDancerFoundInDb;

        public AsyncRelayCommand NavigateToFirstPageCommand { get; set; }
        public AsyncRelayCommand NavigateBackCommand { get; }
        public AsyncRelayCommand NavigateForwardCommand { get; }



        public SearchDancerViewmodel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            _navigationManager = new NavigationManager(navigationService);
            SearchDancerCommand = new AsyncRelayCommand(SearchAsyncDancer, CanSearchAsync);
            WannaUpdateDancerCommand = new AsyncRelayCommand(WannaUpdateDancer, CanWannaUpdateDancer);
            WannaDeleteDancerCommand = new AsyncRelayCommand(WannaDeleteDancer, CanWannaDeleteDancer);
            DancerSelectedCommand = new AsyncRelayCommand(DancerSelected);
            NavigateBackCommand = new AsyncRelayCommand(_navigationManager.NavigateBack, _navigationManager.CanNavigateBack);
            NavigateForwardCommand = new AsyncRelayCommand(_navigationManager.NavigateForward, _navigationManager.CanNavigateForward);
            NavigateToFirstPageCommand = new AsyncRelayCommand(_navigationManager.NavigateToFirstPage);
        }

        //public async Task NavigateToFirstPage()
        //{
        //    await NavigationService.GoToFirstPage();
        //}
        private async Task DancerSelected()
        {
            OnSelectedDancerChanged();
        }
        private async Task OnSelectedDancerChanged()
        {
            NameEntry = SelectedDancer.Name;
            IsUpdateButtonVisible = true;
            IsDeleteButtonVisible = true;
            SearchResultVisible = false;
            IsSearchHeaderVisible = false;
            if (AddTrialDancerToTeam == true)
            {
                await NavigationService.GoToUpdateTeam(SelectedTeam, SelectedDancer, PuttingDancerOnTeam, AddTrialDancerToTeam);
            }
            if (PuttingDancerOnTeam == true)
            {
                await NavigationService.GoToUpdateTeam(SelectedTeam, SelectedDancer, PuttingDancerOnTeam, AddTrialDancerToTeam);
            }

        }


        private bool CanWannaDeleteDancer()
        {
            return SelectedDancer != null;
        }
        private async Task WannaDeleteDancer()
        {
            await NavigationService.GoToDeleteDancer(SelectedDancer);
        }



        private bool CanWannaUpdateDancer()
        {
            return SelectedDancer != null; ;
        }
        private async Task WannaUpdateDancer()
        {
            await NavigationService.GoToUpdateDancer(SelectedDancer);
        }

        private bool CanSearchAsync()
        {
            var dataWritten = !string.IsNullOrWhiteSpace(NameEntry);
            if (dataWritten == true && SelectedDancer == null)
            {
                return true;
            }
            return false;
        }

        private async Task SearchAsyncDancer()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            var request = new RestRequest("/dancers/SearchDancerFromNameOnly", Method.Get);

            if (NameEntry != null)
            {
                request.AddOrUpdateParameter("name", NameEntry);
            }

            var returnedCollection = await client.ExecuteGetAsync<List<DancerDto>>(request, CancellationToken.None);

            if (!returnedCollection.IsSuccessStatusCode)
            {
                JsonSerializerOptions _options = new();
                _options.PropertyNameCaseInsensitive = true;

                ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedCollection.Content ?? "{}", _options)!;
            }

            NameEntry = string.Empty;

            if (!returnedCollection.Data.Any() || returnedCollection.Data == null)
            {
                NoDancerFoundInDb.Invoke("Ingen danser i databasen matcher søgekritetriet");
            }

            DancerDtoList.Clear();

            // Sorter danserne alfabetisk efter navn
            var sortedDancers = returnedCollection.Data.OrderBy(d => d.Name);

            foreach (var item in sortedDancers)
            {
                DancerDtoList.Add(item);
            }

            searchResultVisible = true;
        }



        partial void OnNameEntryChanged(string? newValue)
        {
            NameEntry = ToTitleCase(newValue);
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
            if (query.ContainsKey("teamDto") && query["teamDto"] is TeamDto selectedTeam)

            {
                SelectedTeam = selectedTeam;

            }
            if (query.ContainsKey("puttingDancerOnTeam") && query["puttingDancerOnTeam"] is bool puttingDancerOnTeam)

            {
                PuttingDancerOnTeam = puttingDancerOnTeam;

            }
            

            if (query.ContainsKey("addTrialDancerToTeam") && query["addTrialDancerToTeam"] is bool addTrialDancerToTeam)

            {
                AddTrialDancerToTeam = addTrialDancerToTeam;

            }
        }
    }
}
