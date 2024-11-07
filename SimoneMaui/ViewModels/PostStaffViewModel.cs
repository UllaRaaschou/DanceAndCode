using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using SimoneMaui.ViewModels.Dtos;
using System.Xml.Linq;

namespace SimoneMaui.ViewModels
{
    public partial class PostStaffViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostStaffCommand))]
        private string name = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostStaffCommand))]
        private string timeOfBirth = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PostStaffCommand))]
        private MauiJobRoleEnum role = MauiJobRoleEnum.None;

        public AsyncRelayCommand PostStaffCommand { get; }

        public IReadOnlyList<string> Jobroles { get; } = Enum.GetNames(typeof(MauiJobRoleEnum));


        public PostStaffViewModel()
        {
            PostStaffCommand = new AsyncRelayCommand(PostStaff, CanPostStaff);
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
                var client = new RestClient(options);

                var request = new RestRequest("/staffMember", Method.Post);
                request.AddJsonBody(new { Name, TimeOfBirth = parsedDate, Role });

                var response = await client.PostAsync<StaffDto>(request, CancellationToken.None);

                Name = string.Empty;
                TimeOfBirth = string.Empty;
                Role = MauiJobRoleEnum.None;

                if (response != null)
                {
                    DancerPosted?.Invoke($"Følgende medarbejder er oprettet: {response.Name}, {response.TimeOfBirth}, {response.Role}");
                }
            }

            else
            {
                System.Diagnostics.Debug.WriteLine("Invalid date format");
            }
        }
    }
}
