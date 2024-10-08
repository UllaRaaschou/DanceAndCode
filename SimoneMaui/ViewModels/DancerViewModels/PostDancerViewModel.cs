using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimoneMaui.ViewModels
{
    public partial class PostDancerViewModel : ObservableObject

    {
        [ObservableProperty]
        private string timeOfBirth = string.Empty;

        [ObservableProperty]
        private string name = string.Empty;

        partial void OnNameChanged(string value) => PostDancerCommand.NotifyCanExecuteChanged();
        partial void OnTimeOfBirthChanged(string value) => PostDancerCommand.NotifyCanExecuteChanged();
       

        public IRelayCommand PostDancerCommand { get; }
                 
        public PostDancerViewModel()
        {
            PostDancerCommand = new RelayCommand(async () => await PostDancer(), CanPost);
        }

        private async Task PostDancer()
        {
            if (DateOnly.TryParseExact(TimeOfBirth, "dd-MM-yyyy", out var parsedDate))
            {
                var options = new RestClientOptions("https://localhost:7163");
                var client = new RestClient(options);
                // The cancellation token comes from the caller. You can still make a call without it.
                var request = new RestRequest("/dancers", Method.Post);
                request.AddJsonBody(new { Name, TimeOfBirth = parsedDate });

                var response = await client.ExecutePostAsync(request, CancellationToken.None);

                if (!response.IsSuccessStatusCode) {
                    JsonSerializerOptions _options = new();
                    _options.PropertyNameCaseInsensitive = true;
         
                    ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(response.Content ?? "{}", _options)!;
                                       
                }

                Name = string.Empty;
                TimeOfBirth = string.Empty;

            }

            else 
            {
                System.Diagnostics.Debug.WriteLine("Invalid date format");
            }          
        }

        public bool CanPost() 
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(TimeOfBirth);
        }

    }

}


