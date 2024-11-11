using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using SimoneMaui.ViewModels.Dtos;

namespace SimoneMaui.ViewModels.StaffViewModels
{
    public partial class UpdateStaffViewModel: ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdateStaffCommand))]
        private MauiJobRoleEnum role = MauiJobRoleEnum.None;

        public AsyncRelayCommand UpdateStaffCommand { get; }

        public IReadOnlyList<string> Jobroles { get; } = Enum.GetNames(typeof(MauiJobRoleEnum));

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string timeOfBirth;

        [ObservableProperty]
        private StaffDto selectedStaff;

        public UpdateStaffViewModel() 
        {
            UpdateStaffCommand = new AsyncRelayCommand(UpdateStaff, CanUpdateStaff);
        }

        private bool CanUpdateStaff()
        {
           return selectedStaff != null;
        }

        private async Task UpdateStaff()
        {
            if (DateOnly.TryParseExact(TimeOfBirth, "dd-MM-yyyy", out var parsedDate))
            {
                var options = new RestClientOptions("https://localhost:7163");
                var client = new RestClient(options);
                // The cancellation token comes from the caller. You can still make a call without it.
                var request = new RestRequest($"/staffMember/{SelectedStaff.StaffId}", Method.Put);

                request.AddJsonBody(new {Name, TimeOfBirth = parsedDate, Role });

                var returnedStatus = await client.PutAsync<StaffDto>(request, CancellationToken.None);

                //if (!returnedStatus.IsSuccessStatusCode)
                //{
                //    JsonSerializerOptions _options = new();
                //    _options.PropertyNameCaseInsensitive = true;

                //    ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedStatus.Content ?? "{}", _options)!;

                //}

                SelectedStaff = null;
                //DancerDtoList.Clear();


            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", "Ugyldigt datoformat. Brug venligst dd-MM-yyyy.", "OK");
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("staffDto") && query["staffDto"] is StaffDto staffDto)

            {
                SelectedStaff = staffDto;
                Name = staffDto.Name;
                TimeOfBirth = staffDto.TimeOfBirth.ToString();
            }
        }
    }
}
