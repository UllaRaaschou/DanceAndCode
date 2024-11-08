using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using SimoneMaui.Navigation;
using SimoneMaui.ViewModels.Dtos;
using System.Collections.ObjectModel;

namespace SimoneMaui.ViewModels.StaffViewModels
{
    public partial class SearchStaffViewModel : ObservableObject, IQueryAttributable
    {
        public INavigationService NavigationService { get; set; }

        [ObservableProperty]
        private ObservableCollection<StaffDto> staffDtoList = new ObservableCollection<StaffDto>();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SearchStaffCommand))]
        private string? nameEntry = string.Empty;

        [ObservableProperty]
        private string? name = string.Empty;

        [ObservableProperty]
        private string? timeOfBirth = string.Empty;


        public AsyncRelayCommand StaffSelectedCommand { get; }
        public AsyncRelayCommand WannaUpdateStaffCommand { get; }
        public AsyncRelayCommand WannaDeleteStaffCommand { get; }
        public AsyncRelayCommand SearchStaffCommand { get; }


        public RelayCommand ChooseNavigationCommand { get; }

        [ObservableProperty]
        private bool searchResultVisible = true;

        [NotifyCanExecuteChangedFor(nameof(WannaUpdateStaffCommand))]
        [NotifyCanExecuteChangedFor(nameof(WannaDeleteStaffCommand))]
        [ObservableProperty]
        private StaffDto? selectedStaff;

        [ObservableProperty]
        private bool isUpdateButtonVisible;

        [ObservableProperty]
        private bool isDeleteButtonVisible;

        [ObservableProperty]
        private bool isSearchHeaderVisible;

        public event Action<string> NoStaffFoundInDb;


        public SearchStaffViewModel() { }

        public SearchStaffViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            SearchStaffCommand = new AsyncRelayCommand(SearchAsyncStaff, CanSearchAsync);
            WannaUpdateStaffCommand = new AsyncRelayCommand(WannaUpdateStaff, CanWannaUpdateStaff);
            WannaDeleteStaffCommand = new AsyncRelayCommand(WannaDeleteStaff, CanWannaDeleteStaff);
            StaffSelectedCommand = new AsyncRelayCommand(StaffSelected);
        }

        private async Task StaffSelected()
        {
            OnSelectedStaffChanged();
        }
        private async Task OnSelectedStaffChanged()
        {
            NameEntry = SelectedStaff.Name;
            IsUpdateButtonVisible = true;
            IsDeleteButtonVisible = true;
            SearchResultVisible = false;
            IsSearchHeaderVisible = false;
            await NavigationService.GoToUpdateStaff(SelectedStaff);           
        }


        private bool CanWannaDeleteStaff()
        {
            return selectedStaff != null;
        }
        private async Task WannaDeleteStaff()
        {
            await NavigationService.GoToDeleteStaff(SelectedStaff);
        }



        private bool CanWannaUpdateStaff()
        {
            return selectedStaff != null; ;
        }
        private async Task WannaUpdateStaff()
        {
            await NavigationService.GoToUpdateStaff(SelectedStaff);
        }



        private bool CanSearchAsync()
        {
            var dataWritten = !string.IsNullOrWhiteSpace(NameEntry);
            if (dataWritten == true)
            {
                return true;
            }
            return false;
        }

        private async Task<ObservableCollection<StaffDto>> SearchAsyncStaff()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            var request = new RestRequest("/StaffMember/SearchForStaffByName", Method.Get);

            if (NameEntry != null)
            {
                request.AddOrUpdateParameter("name", NameEntry);

            }

            var returnedCollection = await client.ExecuteGetAsync<List<StaffDto>>(request, CancellationToken.None);

            NameEntry = string.Empty;

            if (!returnedCollection.Data.Any() || returnedCollection.Data == null)
            {
                NoStaffFoundInDb.Invoke("Ingen medarbejder i databasen matcher søgekritetriet");
            }

            var staffCollection = new ObservableCollection<StaffDto>(returnedCollection.Data);

            StaffDtoList.Clear();

            foreach (var staffDto in staffCollection)
            {
                StaffDtoList.Add(staffDto);

            }

            return staffCollection;
        }



        partial void OnNameEntryChanged(string? newValue)
        {
            NameEntry = ToTitleCase(newValue);
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



        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            //if (query.ContainsKey("teamDto") && query["teamDto"] is TeamDto selectedTeam)

            //{
            //    SelectedTeam = selectedTeam;

            //}
            //if (query.ContainsKey("puttingDancerOnTeam") && query["puttingDancerOnTeam"] is bool puttingDancerOnTeam)

            //{
            //    PuttingDancerOnTeam = true;

            //}

        }
    }
}
