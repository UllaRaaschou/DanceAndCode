using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using SimoneMaui.Models;
using SimoneMaui.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimoneMaui.ViewModels.StaffViewModels
{
    public partial class SearchStaffViewModel : ObservableValidator, IQueryAttributable, INotifyPropertyChanged
    {
        public INavigationService NavigationService { get; set; }

        private readonly NavigationManager _navigationManager;
        [ObservableProperty]
        private ObservableCollection<StaffDto> staffDtoList = new ObservableCollection<StaffDto>();

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Navn må fylde mellem 2 og 50 tegn")]
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SearchStaffCommand))]
        private string? nameEntry = string.Empty;

        
        [ObservableProperty]
        private string? name = string.Empty;

        [Required]
        [RegularExpression(@"^\d{2}-\d{2}-\d{4}$", ErrorMessage = "Dato skal tastes i formatet dd-MM-yyyy")]
        [ObservableProperty]
        private string? timeOfBirth = string.Empty;


        public AsyncRelayCommand StaffSelectedCommand { get; }
        public AsyncRelayCommand WannaUpdateStaffCommand { get; }
        public AsyncRelayCommand WannaDeleteStaffCommand { get; }
        public AsyncRelayCommand SearchStaffCommand { get; }

        public AsyncRelayCommand WannaGetWorkingHoursCommand { get; }


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
        private bool isWorkingHourButtonVisible;

        [ObservableProperty]
        private bool isSearchHeaderVisible;

        [ObservableProperty]
        private bool workingWithWorkingHours;

        public event Action<string> NoStaffFoundInDb;
        public AsyncRelayCommand NavigateToFirstPageCommand { get; set; }
        public AsyncRelayCommand NavigateBackCommand { get; }
        public AsyncRelayCommand NavigateForwardCommand { get; }

        public SearchStaffViewModel() { }

        public SearchStaffViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            _navigationManager = new NavigationManager(navigationService);
            SearchStaffCommand = new AsyncRelayCommand(SearchAsyncStaff, CanSearchAsync);
            WannaUpdateStaffCommand = new AsyncRelayCommand(WannaUpdateStaff, CanWannaUpdateStaff);
            WannaDeleteStaffCommand = new AsyncRelayCommand(WannaDeleteStaff, CanWannaDeleteStaff);
            WannaGetWorkingHoursCommand = new AsyncRelayCommand(WannaGetWorkingHours, CanWannaGetWorkingHours);
            StaffSelectedCommand = new AsyncRelayCommand(StaffSelected);
            NavigateBackCommand = new AsyncRelayCommand(_navigationManager.NavigateBack, _navigationManager.CanNavigateBack);
            NavigateForwardCommand = new AsyncRelayCommand(_navigationManager.NavigateForward, _navigationManager.CanNavigateForward);
            NavigateToFirstPageCommand = new AsyncRelayCommand(_navigationManager.NavigateToFirstPage);
            

        }

        private bool CanWannaGetWorkingHours()
        {
            return true;
        }

        private async Task WannaGetWorkingHours()
        {
            await NavigationService.GoToGetWorkingHours(SelectedStaff);
        }

        private async Task StaffSelected()
        {
            await OnSelectedStaffChanged();
        }
        private async Task OnSelectedStaffChanged()
        {
            NameEntry = SelectedStaff.Name;
            IsUpdateButtonVisible = true;
            IsDeleteButtonVisible = true;
            IsWorkingHourButtonVisible = true;
            SearchResultVisible = false;
            IsSearchHeaderVisible = false;
            if(WorkingWithWorkingHours == true) 
            {
                await NavigationService.GoToGetWorkingHours(SelectedStaff);        
            }

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
            if (query.ContainsKey("workingWithWorkingHours") && query["workingWithWorkingHours"] is bool workingWithWorkingHours)

            {
                WorkingWithWorkingHours = workingWithWorkingHours;

            }
            //if (query.ContainsKey("puttingDancerOnTeam") && query["puttingDancerOnTeam"] is bool puttingDancerOnTeam)

            //{
            //    PuttingDancerOnTeam = true;

            //}

        }
    }
}
