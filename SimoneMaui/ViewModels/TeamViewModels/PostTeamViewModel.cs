using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using SimoneMaui.Navigation;
using SimoneMaui.ViewModels.Dtos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace SimoneMaui.ViewModels
{
    public partial class PostTeamViewModel : ObservableValidator, IQueryAttributable, INotifyPropertyChanged
    {
        [Required]
        [Range(1, 200, ErrorMessage = "Holdnummer skal være mellem 1 og 200")]
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostTeamCommand))]
        private int? numberEntry;

        public string Number
        {
            get => numberEntry?.ToString() ?? string.Empty;
            set => numberEntry = int.TryParse(value, out var number) ? number : (int?)null;
        }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Navn må fylde mellem 2 og 50 tegn")]
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostTeamCommand))]
        private string? name;


        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostTeamCommand))]
        private string? dayOfWeekEntry;

        public DayOfWeek DayOfWeek { get; set; } = default;

        
        [NotifyCanExecuteChangedFor(nameof(PostTeamCommand))]
        [TimeValidation(ErrorMessage = "Start- og sluttidspunkt skal være i formatet hh:mm")]
        [ObservableProperty]
        private string? startAndEndTime;
       
        public string SceduledTime => $"{DayOfWeek} + {StartAndEndTime}";

        public IRelayCommand PostTeamCommand { get; }
        public INavigationService NavigationService { get; private set; }

        private readonly NavigationManager _navigationManager;
        public AsyncRelayCommand NavigateToFirstPageCommand { get; set; }
        public AsyncRelayCommand NavigateBackCommand { get; }
        public AsyncRelayCommand NavigateForwardCommand { get; }

        public PostTeamViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            _navigationManager = new NavigationManager(navigationService);
            PostTeamCommand = new AsyncRelayCommand(PostTeam, CanPost);
            NavigateBackCommand = new AsyncRelayCommand(_navigationManager.NavigateBack, _navigationManager.CanNavigateBack);
            NavigateForwardCommand = new AsyncRelayCommand(_navigationManager.NavigateForward, _navigationManager.CanNavigateForward);
            NavigateToFirstPageCommand = new AsyncRelayCommand(_navigationManager.NavigateToFirstPage);

        }


        public DayOfWeek ToDayOfWeekConverter (string dayOfWeekentry) 
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




        private bool CanPost()
        {
            return NumberEntry!= null
                && !string.IsNullOrEmpty(Name) 
                && !string.IsNullOrEmpty(DayOfWeekEntry) 
                && !string.IsNullOrEmpty(StartAndEndTime);
        }

        public event Action<string> NewTeamPosted;










        private async Task PostTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            string? number = NumberEntry?.ToString();
           
            var request = new RestRequest("/teams", Method.Post);
            request.AddJsonBody(new { number, Name, SceduledTime, DayOfWeek });

            var teamDto = await client.PostAsync<TeamDto>(request, CancellationToken.None);

            Number = string.Empty;
            Name = string.Empty;
            DayOfWeekEntry = string.Empty;
            StartAndEndTime = string.Empty;

            NewTeamPosted?.Invoke($"Nyt dansehold oprettet: {teamDto?.TeamDetailsString}");

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








        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query == null || query.Count == 0)
            {
                return; // Ingen parametre, så der udføres intet
            }
        }
    }
}      
    
