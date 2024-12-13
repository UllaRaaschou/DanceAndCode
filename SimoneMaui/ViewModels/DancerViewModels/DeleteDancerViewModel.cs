using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.ObjectModel;
using System.Text.Json;
using SimoneMaui.Navigation;
using SimoneMaui.Models;

namespace SimoneMaui.ViewModels
{
    public partial class DeleteDancerViewModel : ObservableObject, IQueryAttributable
    {
        //[ObservableProperty]
        //private ObservableCollection<DancerDto> dancerDtoList;

        [ObservableProperty]
        private string? name = string.Empty;

        [ObservableProperty]
        private string? timeOfBirth = string.Empty;

        [ObservableProperty]
        private bool buttonIsVisible = true;

        partial void OnNameChanged(string value)
        {
            DeleteDancerCommand.NotifyCanExecuteChanged();
        }
        partial void OnTimeOfBirthChanged(string value)
        {
            DeleteDancerCommand.NotifyCanExecuteChanged();
        }

        public AsyncRelayCommand NavigateToFirstPageCommand { get; set; }
        public AsyncRelayCommand NavigateBackCommand { get; }
        public AsyncRelayCommand NavigateForwardCommand { get; }
        public RelayCommand DeleteDancerCommand { get; }

        public event Action<string> DancerDeleted;
       

        private DancerDto? selectedDancer = null;
        public DancerDto? SelectedDancer
        {
            get => selectedDancer;
            set
            {
                if (selectedDancer != value)
                {
                    selectedDancer = value;
                    OnPropertyChanged();

                    //Her sørger jeg for, at de observable Proprties sættes til værdierne for den selectede danser
                    Name = selectedDancer?.Name;
                    TimeOfBirth = selectedDancer?.TimeOfBirth;

                    OnPropertyChanged(nameof(Name));
                    OnPropertyChanged(nameof(TimeOfBirth));
                };
            }
        }

        public INavigationService NavigationService { get; private set; }

        private readonly NavigationManager _navigationManager;

        public DeleteDancerViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            _navigationManager = new NavigationManager(navigationService);
            DeleteDancerCommand = new RelayCommand(async () => await DeleteDancer(), CanDelete);
            NavigateBackCommand = new AsyncRelayCommand(_navigationManager.NavigateBack, _navigationManager.CanNavigateBack);
            NavigateForwardCommand = new AsyncRelayCommand(_navigationManager.NavigateForward, _navigationManager.CanNavigateForward);
            NavigateToFirstPageCommand = new AsyncRelayCommand(_navigationManager.NavigateToFirstPage);


        }
        
        private bool CanDelete()
        {
            var dataWritten = !string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(TimeOfBirth);
            if (dataWritten == true && SelectedDancer != null)
            {
                return true;
            }
            return false;
        }

        public async Task DeleteDancer()
        {
            if (SelectedDancer != null)
            {
                var options = new RestClientOptions("https://localhost:7163");
                var client = new RestClient(options);
                // The cancellation token comes from the caller. You can still make a call without it.
                var request = new RestRequest($"/dancers/{SelectedDancer.DancerId}", Method.Delete);

                var returnedStatus = await client.ExecuteDeleteAsync(request, CancellationToken.None);

                if (!returnedStatus.IsSuccessStatusCode)
                {
                    JsonSerializerOptions _options = new();
                    _options.PropertyNameCaseInsensitive = true;

                    ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedStatus.Content ?? "{}", _options)!;

                }

                SelectedDancer = null;
                ButtonIsVisible = false;
                DancerDeleted.Invoke("Eleven er slettet i databasen");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", "Der er ikke valgt en danser", "OK");
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("dancerDto") && query["dancerDto"] is DancerDto dancerDto)
            {
                SelectedDancer = dancerDto;
            }
        }
    }  
}
