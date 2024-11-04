using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using SimoneMaui.ViewModels.Dtos;


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
       

        public AsyncRelayCommand PostDancerCommand { get; }
                 
        public PostDancerViewModel()
        {
            PostDancerCommand = new AsyncRelayCommand(PostDancer, CanPost);
        }

        public event Action<string> DancerPosted;

        private async Task PostDancer()
        {
            if (DateOnly.TryParseExact(TimeOfBirth, "dd-MM-yyyy", out var parsedDate))
            {
                var options = new RestClientOptions("https://localhost:7163");
                var client = new RestClient(options);
                // The cancellation token comes from the caller. You can still make a call without it.
                var request = new RestRequest("/dancers", Method.Post);
                request.AddJsonBody(new { Name, TimeOfBirth = parsedDate });

                var response = await client.PostAsync<DancerDto>(request, CancellationToken.None);

                Name = string.Empty;
                TimeOfBirth = string.Empty;

                if(response != null) 
                {
                    DancerPosted?.Invoke($"Følgende elev er oprettet: {response.Name}, {response.TimeOfBirth}");
                }               
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


