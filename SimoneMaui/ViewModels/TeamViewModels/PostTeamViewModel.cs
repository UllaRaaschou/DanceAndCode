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
        private string? dayOfWeek;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostTeamCommand))]
        private string? startAndEndTime;
       
        public string SceduledTime => $"{DayOfWeek} + {StartAndEndTime}";

        public IRelayCommand PostTeamCommand { get; }

        public PostTeamViewModel()
        {
            PostTeamCommand = new AsyncRelayCommand(PostTeam, CanPost);
        }

        private bool CanPost()
        {
            return !string.IsNullOrEmpty(Number)
                && !string.IsNullOrEmpty(Name) 
                && !string.IsNullOrEmpty(DayOfWeek) 
                && !string.IsNullOrEmpty(StartAndEndTime);
        }

        public event Action<string> NewTeamPosted;

        private async Task PostTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
           
            var request = new RestRequest("/teams", Method.Post);
            request.AddJsonBody(new { Number, Name, SceduledTime });

            var teamDto = await client.PostAsync<TeamDto>(request, CancellationToken.None);

            Number = string.Empty;
            Name = string.Empty;
            DayOfWeek = string.Empty;
            StartAndEndTime = string.Empty;

            NewTeamPosted?.Invoke($"Nyt dansehold oprettet: {teamDto?.TeamDetailsString}");

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
    
