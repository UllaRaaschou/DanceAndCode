using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using SimoneMaui.Navigation;
using System.Collections.ObjectModel;
using System.Text.Json;
using SimoneMaui.ViewModels.Dtos;
using RestSharp.Serializers.Json;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimoneMaui.ViewModels
{
    public partial class UpdateTeamViewModel : ObservableValidator, IQueryAttributable, INotifyPropertyChanged
    {
        public INavigationService NavigationService { get; set; }

        private readonly NavigationManager _navigationManager;
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Navn må fylde mellem 2 og 50 tegn")]
        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private string? timeOfBirth;

        [Required]
        [Range(1, 200, ErrorMessage = "Holdnummer skal være mellem 1 og 200")]
        [ObservableProperty]
        private int? numberEntry;

        public string Number
        {
            get => numberEntry?.ToString() ?? string.Empty;
            set => numberEntry = int.TryParse(value, out var number) ? number : (int?)null;
        }

        [TimeValidation(ErrorMessage = "Start- og sluttidspunkt skal være i formatet hh:mm")]
        [ObservableProperty]
        private string? startAndEndTime;
        public string SceduledTime => $"{DayOfWeek} + {StartAndEndTime}";

        [ObservableProperty]
        private TeamDto? selectedTeam;

        [ObservableProperty]
        private DancerDto? selectedDancer;

        [ObservableProperty]
        private bool isEndOfProcedure = true;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Count))]

        private ObservableCollection<DancerDto> dancersOnTeam = new ObservableCollection<DancerDto>();

        [ObservableProperty]
        private ObservableCollection<TeamDto> teams;

        [ObservableProperty]
        private string? teamDetailsString;

        [ObservableProperty]
        private bool isHighlighted;

        [ObservableProperty]
        private DancerDto dancerToDelete;

        [ObservableProperty]
        private bool dancerIsSelected = false;

        [ObservableProperty]
        private bool isStartOfProcedure = true;

        public int Count => DancersOnTeam.Count;

        [ObservableProperty]
        private string? dayOfWeekEntry = string.Empty;

        [ObservableProperty]
        private DayOfWeek dayOfWeek = default;

        [ObservableProperty]
        private bool isTrialLesson = false;


        public string DancerDetailsString => SelectedDancer?.DancerDetailsString ?? "";

        [ObservableProperty]
        private string ifTrialLessonIsTrue;

        public DayOfWeek ToDayOfWeekConverter(string dayOfWeekentry)
        {
            return dayOfWeekentry switch
            {
                "Mandag" => DayOfWeek.Monday,
                "Tirsdag" => DayOfWeek.Tuesday,
                "Onsdag" => DayOfWeek.Wednesday,
                "Torsdag" => DayOfWeek.Thursday,
                "Fredag" => DayOfWeek.Friday,
                "Lørdag" => DayOfWeek.Saturday,
                "Søndag" => DayOfWeek.Sunday
            };
        }


        [ObservableProperty]
        private bool puttingDancerOnTeam;

        [ObservableProperty]
        private bool addTrialDancerToTeam;

        public AsyncRelayCommand WannaAddDancerCommand { get; }
        public AsyncRelayCommand WannaDeleteDancerCommand { get; }
        public AsyncRelayCommand WannaAddTrialDancerCommand { get; }
        public AsyncRelayCommand AddTrialDancerCommand { get; }


        public AsyncRelayCommand AddDancerToTeamCommand { get; }
        public AsyncRelayCommand DeleteDancerFromTeamCommand { get; }
        public AsyncRelayCommand FinalUpdateTeamCommand { get; }
        public AsyncRelayCommand NavigateToFirstPageCommand { get; set; }
        public AsyncRelayCommand NavigateBackCommand { get; }
        public AsyncRelayCommand NavigateForwardCommand { get; }

      


        public UpdateTeamViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            _navigationManager = new NavigationManager(navigationService);
            WannaAddDancerCommand = new AsyncRelayCommand(WannaAddDancer, CanWannaAddDancer);
            WannaDeleteDancerCommand = new AsyncRelayCommand(WannaDeleteDancer, CanWannaDeleteDancer);
            WannaAddTrialDancerCommand = new AsyncRelayCommand(WannaAddTrialDancer, CanWannaAddTrialDancer);
            AddDancerToTeamCommand = new AsyncRelayCommand(AddDancerToTeam, CanAddDancerToTeam);
            AddTrialDancerCommand = new AsyncRelayCommand(AddTrialDancer, CanAddTrialDancer);
            FinalUpdateTeamCommand = new AsyncRelayCommand(FinalUpdateTeam, CanFinalUpdateTeam);
            NavigateBackCommand = new AsyncRelayCommand(_navigationManager.NavigateBack, _navigationManager.CanNavigateBack);
            NavigateForwardCommand = new AsyncRelayCommand(_navigationManager.NavigateForward, _navigationManager.CanNavigateForward);
            NavigateToFirstPageCommand = new AsyncRelayCommand(_navigationManager.NavigateToFirstPage);

        }

        private bool CanFinalUpdateTeam()
        {
            return selectedTeam != null;
        }

        private async Task FinalUpdateTeam()
        {
                var options = new RestClientOptions("https://localhost:7163");
                var client = new RestClient(options, configureSerialization: s =>
                {
                    s.UseSystemTextJson(new JsonSerializerOptions
                    {
                        Converters = { new JsonStringEnumConverter() }
                    });
                });
                // The cancellation token comes from the caller. You can still make a call without it.
                var request = new RestRequest($"/Teams", Method.Put);

                request.AddJsonBody(new { SelectedTeam!.TeamId, Number, Name, StartAndEndTime, DancersOnTeam  });

                var returnedStatus = await client.PutAsync<TeamDto>(request, CancellationToken.None);


                SelectedDancer = null;
            //DancerDtoList.Clear();
            await Shell.Current.GoToAsync($"//firstPage?unique={Guid.NewGuid()}");



        }

        private bool CanAddTrialDancer()
        {
            if (selectedDancer != null && selectedTeam != null)
            {
                return true;
            }
            return false;
        }

        private async Task AddTrialDancer()
        {
            IsTrialLesson = true;
            
            await AddDancerToTeam();

        }

        private bool CanWannaAddTrialDancer()
        
        {
            return true;
        }

        private async Task WannaAddTrialDancer()
        {
            AddTrialDancerToTeam = true;
            PuttingDancerOnTeam = false;
            await NavigationService.GoToSearchDancer(SelectedTeam, PuttingDancerOnTeam, AddTrialDancerToTeam);
        }

        partial void OnSelectedDancerChanged(DancerDto value) 
        {
            if(value != null) 
            {
                DancerIsSelected = true;
                IsStartOfProcedure = false;
            }
        }

        private bool CanAddDancerToTeam()
        {
            if (selectedDancer != null && selectedTeam != null)
            {
                return true;
            }
            return false;
        }

        


            public bool CanDeleteDancerFromTeam()
            {
                if (DancerToDelete != null && selectedTeam != null)
                {
                    return true;
                }
                return false;
            }

            private bool CanWannaAddDancer()
            {
                return true;
            }
            public async Task WannaAddDancer()
            {
                PuttingDancerOnTeam = true;
                await NavigationService.GoToSearchDancer(SelectedTeam, PuttingDancerOnTeam, AddTrialDancerToTeam);
            }



            private bool CanWannaDeleteDancer()
            {
                if(SelectedTeam != null && DancerIsSelected==false)
                {
                    return true;
                }
                return false;
            }
            public async Task WannaDeleteDancer()
            {
                await NavigationService.GoToDeleteDancerFromTeam(SelectedTeam, DancerToDelete);
            }



            partial void OnDayOfWeekEntryChanged(string? newValue)
            {
                DayOfWeekEntry = ToTitleCase(newValue);
            }

            partial void OnNameChanged(string? newValue)
            {
                Name = ToTitleCase(newValue);
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

        private async Task AddDancerToTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);

            var request = new RestRequest($"/Teams/{SelectedTeam.TeamId}/AddToListOfDancers", Method.Put);

            request.AddJsonBody(new { dancerId = SelectedDancer.DancerId, IsTrialLesson });

            var response = await client.ExecuteAsync<TeamDto>(request, CancellationToken.None);

            if (response.Data != null)
            {
                var mauiTeamDto = response.Data;

                mauiTeamDto.DancersOnTeam.First(d => d.DancerId == SelectedDancer.DancerId).IsHighlighted = true;

                var validDancers = mauiTeamDto.DancersOnTeam
                                    .Where(d => d.LastDancedate >= DateOnly.FromDateTime(DateTime.Now))
                                    .ToList();

                DancersOnTeam = new ObservableCollection<DancerDto>(validDancers);

                DancersOnTeam = mauiTeamDto.DancersOnTeam;
                SelectedDancer = null;
                DancerIsSelected = false;
            }
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
                    StartAndEndTime = teamDto.SceduledTime;
                    DancersOnTeam = new ObservableCollection<DancerDto>(teamDto.DancersOnTeam);

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
