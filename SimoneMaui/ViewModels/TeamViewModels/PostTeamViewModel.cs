using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using SimoneMaui.ViewModels.Dtos;


namespace SimoneMaui.ViewModels
{
    public partial class PostTeamViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostTeamCommand))]
        private string? number;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostTeamCommand))]
        private string? name;


        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostTeamCommand))]
        private string? dayOfWeekEntry;

        public DayOfWeek DayOfWeek { get; set; } = default;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostTeamCommand))]
        private string? startAndEndTime;
       
        public string SceduledTime => $"{DayOfWeek} + {StartAndEndTime}";

        public IRelayCommand PostTeamCommand { get; }

        public PostTeamViewModel()
        {
            PostTeamCommand = new AsyncRelayCommand(PostTeam, CanPost);
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
            return !string.IsNullOrEmpty(Number)
                && !string.IsNullOrEmpty(Name) 
                && !string.IsNullOrEmpty(DayOfWeekEntry) 
                && !string.IsNullOrEmpty(StartAndEndTime);
        }

        public event Action<string> NewTeamPosted;










        private async Task PostTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
           
            var request = new RestRequest("/teams", Method.Post);
            request.AddJsonBody(new { Number, Name, SceduledTime, DayOfWeek });

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
    
