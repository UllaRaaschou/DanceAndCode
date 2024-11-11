using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using SimoneMaui.Navigation;
using SimoneMaui.ViewModels.Dtos;

namespace SimoneMaui.ViewModels.StaffViewModels
{
    public partial class DeleteStaffViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteStaffCommand))]
        private string? name = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteStaffCommand))]
        private string? timeOfBirth = string.Empty;

        [ObservableProperty]
        private bool buttonIsVisible = true;

       
        public RelayCommand DeleteStaffCommand { get; }
        public INavigationService NavigationService { get; private set; }

        private readonly NavigationManager _navigationManager;

        public event Action<string> StaffDeleted;

        [ObservableProperty]
        private StaffDto? selectedStaff;

        public AsyncRelayCommand NavigateToFirstPageCommand { get; set; }
        public AsyncRelayCommand NavigateBackCommand { get; }
        public AsyncRelayCommand NavigateForwardCommand { get; }

        public async Task NavigateToFirstPage()
        {
            await NavigationService.GoToFirstPage();
        }

        public DeleteStaffViewModel( INavigationService navigationService)
        {
            NavigationService = navigationService;
            _navigationManager = new NavigationManager(navigationService);
            DeleteStaffCommand = new RelayCommand(async () => await DeleteStaff(), CanDelete);
            NavigateBackCommand = new AsyncRelayCommand(_navigationManager.NavigateBack, _navigationManager.CanNavigateBack);
            NavigateForwardCommand = new AsyncRelayCommand(_navigationManager.NavigateForward, _navigationManager.CanNavigateForward);
            NavigateToFirstPageCommand = new AsyncRelayCommand(NavigateToFirstPage);

        }

        private bool CanDelete()
        {
            var dataWritten = !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(TimeOfBirth);
            if (dataWritten == true && SelectedStaff != null)
            {
                return true;
            }
            return false;
        }

        public async Task DeleteStaff()
        {
            if (SelectedStaff != null)
            {
                var options = new RestClientOptions("https://localhost:7163");
                var client = new RestClient(options);
                // The cancellation token comes from the caller. You can still make a call without it.
                var request = new RestRequest($"/StaffMember/{SelectedStaff.StaffId}", Method.Delete);

                var returnedStatus = await client.DeleteAsync(request, CancellationToken.None);


                SelectedStaff = null;
                ButtonIsVisible = false;
                StaffDeleted.Invoke("Medarbejderen er slettet i databasen");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", "Der er ikke valgt en medarbejder", "OK");
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("staffDto") && query["staffDto"] is StaffDto selectedDto)
            {
                SelectedStaff = selectedDto;
                Name = selectedDto.Name;
                TimeOfBirth = selectedDto.TimeOfBirth.ToString();
            }
        }
    }
}
