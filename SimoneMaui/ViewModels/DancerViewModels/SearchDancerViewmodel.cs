using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SimoneMaui.Navigation;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace SimoneMaui.ViewModels
{
    public partial class SearchDancerViewmodel: ObservableObject, IQueryAttributable
    {
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
        public AsyncRelayCommand SearchDancerCommand { get; }

        public RelayCommand ChooseNavigationCommand { get; }

        public INavigationService NavigationService { get; set; }


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

        [RelayCommand]
        private async Task NavigateToUpdateDancer()
        {
            if (SelectedDancer is not null)
            {
                await NavigationService.GoToUpdateDancer(SelectedDancer);
            }
        }

        [RelayCommand]
        private async Task NavigateToUpdateTeam() 
        {
            if (SelectedTeam is not null && SelectedDancer is not null) 
            {
                await NavigationService.GoToUpdateTeam(SelectedTeam, SelectedDancer);
            }
        }


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

        private void ChooseNavigation()
        {
            if (SelectedTeam is not null && SelectedDancer != null)
            {
                NavigateToUpdateTeam();

            }
            if (SelectedTeam == null && SelectedDancer != null)
            {
                NavigateToUpdateDancer();
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

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("teamDto") && query["teamDto"] is TeamDto teamDto)

            {
                SelectedTeam = teamDto;
            }
        }

        private async Task<ObservableCollection<DancerDto>> SearchAsyncDancer()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            var request = new RestRequest("/dancers/SearchForDancerFromNameOrBirthday", Method.Get);

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
                  
    }

        
        public class DancerDto
    {
        public Guid DancerId { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string TimeOfBirth { get; set; } = string.Empty;
        public ObservableCollection<TeamDto> Teams  { get; set; } = new ObservableCollection<TeamDto>();
    }

    public class TeamDto
    {
        public Guid TeamId { get; set; } = Guid.Empty;
        public int Number { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string SceduledTime { get; set; } = string.Empty;
        public string TeamDetailsString => $"Hold {Number} '{Name}' - {SceduledTime}";      

    }

}
