using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using RestSharp.Serializers.Json;
using SimoneMaui.Navigation;
using SimoneMaui.ViewModels.Dtos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.Json;


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
       
        public string ScheduledTime => $"{DayOfWeek} + {StartAndEndTime}";

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
            DayOfWeek day = ToDayOfWeekConverter(DayOfWeekEntry);

            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options, configureSerialization: s =>
            {
                s.UseSystemTextJson(new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() }
                });
            });
            
            string? number = NumberEntry?.ToString();
           
            var request = new RestRequest("/teams", Method.Post);
            request.AddJsonBody(new { number, Name, ScheduledTime, day });

            await client.PostAsync(request, CancellationToken.None);

            NewTeamPosted?.Invoke($"Nyt dansehold oprettet: Hold {Number} '{Name}' - {ScheduledTime}");

            NumberEntry = null;
            Number = string.Empty;
            Name = string.Empty;
            DayOfWeekEntry = string.Empty;
            StartAndEndTime = string.Empty;
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
    
