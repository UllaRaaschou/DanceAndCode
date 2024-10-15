using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;


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

        private async Task PostTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            // The cancellation token comes from the caller. You can still make a call without it.
            var request = new RestRequest("/teams", Method.Post);
            request.AddJsonBody(new { Number, Name, SceduledTime });

            var response = await client.ExecutePostAsync(request, CancellationToken.None);

            if (!response.IsSuccessStatusCode)
            {
                JsonSerializerOptions _options = new();
                _options.PropertyNameCaseInsensitive = true;

                ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(response.Content ?? "{}", _options)!;

            }

            Number = string.Empty;
            Name = string.Empty;
            DayOfWeek = string.Empty;
            StartAndEndTime = string.Empty;

        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}      
    
