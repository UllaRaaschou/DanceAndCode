using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.ComponentModel;
using System.Text.Json;


namespace SimoneMaui.ViewModels
{
    public partial class PostTeamViewModel : ObservableObject
    {
        [ObservableProperty]
        private int number;

        [ObservableProperty]
        private string name;


        [ObservableProperty]
        private string dayOfWeek;
        partial void OnDayOfWeekChanged(string value)
        {
            OnPropertyChanged(nameof(SceduledTime));
        }


        [ObservableProperty]
        private string startAndEndTime;
        partial void OnStartAndEndTimeChanged(string value)
        {
            OnPropertyChanged(nameof(SceduledTime));
        }



        public string SceduledTime
        {
            get
            {
                return $"{dayOfWeek} + {startAndEndTime}";
            }
        }

        public IRelayCommand PostTeamCommand { get; }


        public PostTeamViewModel()
        {
            PostTeamCommand= new RelayCommand(async () => await PostTeam(), canPost);
        }

        private bool canPost()
        {
            return !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(dayOfWeek) && !string.IsNullOrEmpty(startAndEndTime);
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

            Number = 0;
            Name = string.Empty;
            DayOfWeek = string.Empty;
            StartAndEndTime = string.Empty;

        }
    }
}      
    
