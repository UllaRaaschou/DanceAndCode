using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using SimoneMaui.Navigation;
using SimoneMaui.ViewModels.Dtos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace SimoneMaui.ViewModels
{
    public partial class PostDancerViewModel : ObservableValidator, INotifyPropertyChanged

    {
        [Required]
        [RegularExpression(@"^\d{2}-\d{2}-\d{4}$", ErrorMessage = "Dato skal tastes i formatet dd-MM-yyyy")]
        [ObservableProperty]
        private string timeOfBirth = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Navn må fylde mellem 2 og 50 tegn")]
        [ObservableProperty]
        private string name = string.Empty;

        public INavigationService NavigationService { get; private set; }

        private readonly NavigationManager _navigationManager;

        public AsyncRelayCommand PostDancerCommand { get; }

        public AsyncRelayCommand NavigateToFirstPageCommand { get; set; }
        public AsyncRelayCommand NavigateBackCommand { get; }
        public AsyncRelayCommand NavigateForwardCommand { get; }

        //public async Task NavigateToFirstPage()
        //{
        //    await NavigationService.GoToFirstPage();
        //}

        public PostDancerViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            _navigationManager = new NavigationManager(navigationService);
            PostDancerCommand = new AsyncRelayCommand(PostDancer, CanPost);
            NavigateBackCommand = new AsyncRelayCommand(_navigationManager.NavigateBack, _navigationManager.CanNavigateBack);
            NavigateForwardCommand = new AsyncRelayCommand(_navigationManager.NavigateForward, _navigationManager.CanNavigateForward);
            NavigateToFirstPageCommand = new AsyncRelayCommand(_navigationManager.NavigateToFirstPage);
            
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




        partial void OnNameChanged(string? newValue)
        {
            Name = ToTitleCase(newValue);
            PostDancerCommand.NotifyCanExecuteChanged();
        }

        partial void OnTimeOfBirthChanged(string? newValue)
        {
            TimeOfBirth = newValue;
            PostDancerCommand.NotifyCanExecuteChanged();
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

    }

}


