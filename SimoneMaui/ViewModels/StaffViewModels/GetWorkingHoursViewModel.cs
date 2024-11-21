﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using SimoneMaui.Navigation;
using SimoneMaui.ViewModels.Dtos;
using System.Collections.ObjectModel;

namespace SimoneMaui.ViewModels.StaffViewModels
{

    public partial class GetWorkingHoursViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GetWorkingHoursCommand))]
        private string? name = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GetWorkingHoursCommand))]
        private string? timeOfBirth = string.Empty;

        [ObservableProperty]
        private bool buttonIsVisible = true;

        public ObservableCollection<WorkingHoursDto> WorkingHoursList { get; set; } = new ObservableCollection<WorkingHoursDto>();

        public DateTime Date { get; set; } = DateTime.Now;
        public double Loen1 { get; set; }
        public double Loen2 { get; set; }
        public double Loen3 { get; set; }
        public double Loen4 { get; set; }
        public bool IsVikar { get; set; } = false;
        public string Comment { get; set; } = string.Empty; 


        public AsyncRelayCommand GetWorkingHoursCommand { get; }
        public INavigationService NavigationService { get; private set; }

        private readonly NavigationManager _navigationManager;

        public event Action<string> StaffDeleted;

        [ObservableProperty]
        private StaffDto? selectedStaff;

        public AsyncRelayCommand NavigateToFirstPageCommand { get; set; }
        public AsyncRelayCommand NavigateBackCommand { get; }
        public AsyncRelayCommand NavigateForwardCommand { get; }



        public GetWorkingHoursViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            _navigationManager = new NavigationManager(navigationService);

            GetWorkingHoursCommand = new AsyncRelayCommand(GetWorkingHours, CanGetWorkingHours);
           
            
            NavigateToFirstPageCommand = new AsyncRelayCommand(_navigationManager.NavigateToFirstPage);

        }

        
       
        private bool CanGetWorkingHours()
        {
            //return SelectedStaff != null;
            return true;
        }

    private async Task GetWorkingHours()
    {
            if (SelectedStaff != null)
            {
                var options = new RestClientOptions("https://localhost:7163");
                var client = new RestClient(options);
                
                var request = new RestRequest($"/WorkingHours/{SelectedStaff.StaffId}", Method.Get);

                var returnedRegistrations = await client.GetAsync<List<WorkingHoursDto>>(request, CancellationToken.None);


                SelectedStaff = null;
                ButtonIsVisible = false;

                foreach (var item in returnedRegistrations)
                {
                    WorkingHoursList.Add(item);
                }
                

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
