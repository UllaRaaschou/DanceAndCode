using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using SimoneMaui.ViewModels.Dtos;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Linq;
using RestSharp.Serializers.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SimoneMaui.Navigation;

namespace SimoneMaui.ViewModels
{
    public partial class PostStaffViewModel : ObservableValidator, INotifyPropertyChanged
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Navn må fylde mellem 2 og 50 tegn")]
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostStaffCommand))]
        private string name = string.Empty;

        [Required]
        [RegularExpression(@"^\d{2}-\d{2}-\d{4}$", ErrorMessage = "Dato skal tastes i formatet dd-MM-yyyy")]
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostStaffCommand))]
        private string timeOfBirth = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostStaffCommand))]
        private MauiJobRoleEnum role = MauiJobRoleEnum.None;

        public AsyncRelayCommand PostStaffCommand { get; }

        public IReadOnlyList<string> Jobroles { get; } = Enum.GetNames(typeof(MauiJobRoleEnum));
        public INavigationService NavigationService { get; private set; }

        private readonly NavigationManager _navigationManager;

        public AsyncRelayCommand NavigateToFirstPageCommand { get; set; }
        public AsyncRelayCommand NavigateBackCommand { get; }
        public AsyncRelayCommand NavigateForwardCommand { get; }

       
        public PostStaffViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            _navigationManager = new NavigationManager(navigationService);
            PostStaffCommand = new AsyncRelayCommand(PostStaff, CanPostStaff);
            NavigateBackCommand = new AsyncRelayCommand(_navigationManager.NavigateBack, _navigationManager.CanNavigateBack);
            NavigateForwardCommand = new AsyncRelayCommand(_navigationManager.NavigateForward, _navigationManager.CanNavigateForward);
            NavigateToFirstPageCommand = new AsyncRelayCommand(_navigationManager.NavigateToFirstPage);
        }

        public event Action<string> DancerPosted;

        private bool CanPostStaff()
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrEmpty(timeOfBirth) && Role != MauiJobRoleEnum.None)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task PostStaff()
        {
            if (DateOnly.TryParseExact(TimeOfBirth, "dd-MM-yyyy", out var parsedDate))
            {
               
                var options = new RestClientOptions("https://localhost:7163");
                var client = new RestClient(options, configureSerialization: s =>
                {
                    s.UseSystemTextJson(new JsonSerializerOptions
                    {
                        Converters = { new JsonStringEnumConverter() }
                    });
                });

                var request = new RestRequest("/StaffMember", Method.Post);
                request.AddJsonBody(new { Name, TimeOfBirth = parsedDate, Role });

                var response = await client.PostAsync(request, CancellationToken.None);

                if (response.IsSuccessful)
                {
                    DancerPosted?.Invoke($"Følgende medarbejder er oprettet: {Name}, {TimeOfBirth}, {Role}");

                    Name = string.Empty;
                    TimeOfBirth = string.Empty;
                    Role = MauiJobRoleEnum.None;
                }
                else
                {
                    DancerPosted?.Invoke("Noget gik galt!");

                }


                
            }

            else
            {
                System.Diagnostics.Debug.WriteLine("Invalid date format");
            }
        }
    }
}
