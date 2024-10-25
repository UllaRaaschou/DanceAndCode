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
    public partial class SearchDancerViewmodel: ObservableObject, IQueryAttributable
    {
        public INavigationService NavigationService { get; set; }

        [ObservableProperty]
        private ObservableCollection<DancerDto> dancerDtoList;

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

        public bool PuttingDancerOnTeam { get; set; } = false;
        public AsyncRelayCommand SearchDancerCommand { get; }

        public RelayCommand ChooseNavigationCommand { get; }

        


        private DancerDto? selectedDancer = null;
        public DancerDto? SelectedDancer
        {
            get => selectedDancer;
            set
            {
                if (SetProperty(ref selectedDancer, value))
                {
                    NameEntry = string.Empty;                    
                }
            }
        }

        //[RelayCommand]
        //private async Task NavigateToUpdateDancer()
        //{
        //    if (SelectedDancer is not null)
        //    {
        //        await NavigationService.GoToUpdateDancer(SelectedDancer);
        //    }
        //}

        //[RelayCommand]
        //private async Task NavigateToUpdateTeam() 
        //{
        //    if (SelectedTeam is not null && SelectedDancer is not null) 
        //    {
        //        await NavigationService.GoToUpdateTeam(SelectedTeam, SelectedDancer);
        //    }
        //}


        public SearchDancerViewmodel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            ChooseNavigationCommand = new RelayCommand(ChooseNavigation);
            SearchDancerCommand = new AsyncRelayCommand(SearchAsyncDancer, CanSearchAsync);
            dancerDtoList = new ObservableCollection<DancerDto>()
            {
                new DancerDto{Name="Test", TimeOfBirth="01-01-2001" }
            };
        }

        private async void ChooseNavigation()
        {
            if (SelectedTeam is not null && SelectedDancer != null && PuttingDancerOnTeam==true)
            {
                await NavigationService.GoToUpdateTeam(SelectedTeam, SelectedDancer);
            }

            if (SelectedTeam == null && SelectedDancer != null)
            {
                await NavigationService.GoToUpdateDancer(SelectedDancer);
            }
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

        private async Task<ObservableCollection<DancerDto>> SearchAsyncDancer()
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
                return null;
            }

            var dancerCollection = new ObservableCollection<DancerDto>(returnedCollection.Data);

            DancerDtoList.Clear();
            foreach (var item in dancerCollection)
            {
                DancerDtoList.Add(item);

            }
            return dancerCollection;
        }

        partial void OnNameEntryChanged(string? newValue)
        {
            NameEntry = ToTitleCase(newValue);
        }
        public void UpdateNameEntry(string? newValue)
        {
            NameEntry = ToTitleCase(newValue); // Anvend Title Case konvertering
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
                PuttingDancerOnTeam = true;

            }
        }
    }
}
