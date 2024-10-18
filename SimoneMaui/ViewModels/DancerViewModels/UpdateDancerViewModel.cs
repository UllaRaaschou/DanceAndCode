using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SimoneMaui.Navigation;
using System.Collections.ObjectModel;
using System.Text.Json;
using SimoneMaui.ViewModels.Dtos;

namespace SimoneMaui.ViewModels
{
    public partial class UpdateDancerViewModel : ObservableObject, IQueryAttributable
    {
        //[ObservableProperty]
        //private DancerDto? selectedDancer;

        private DancerDto? selectedDancer;
        public DancerDto? SelectedDancer 
        {
            get => selectedDancer;
            set
            {
                SetProperty(ref selectedDancer, value);
                OnSelectedDancerChanged(value);
            }
        }

        public INavigationService NavigationService { get; set; }

        [ObservableProperty]
        private string? name = string.Empty;

        [ObservableProperty]
        private string? timeOfBirth = string.Empty;

        [ObservableProperty]
        private ObservableCollection<TeamDto> teams;

        [ObservableProperty]
        private string teamDetailsString = string.Empty;

        //[ObservableProperty]
        //private DancerDto? updatedDancerDto = null;

        private TeamDto? selectedTeam;
        public TeamDto? SelectedTeam
        {
            get => selectedTeam;
            set
            {
                SetProperty(ref selectedTeam, value);
                TeamDetailsString = value?.TeamDetailsString;
                RemoveTeamCommand.NotifyCanExecuteChanged();
            }
        }

        private TeamDto? teamToAdd;
        public TeamDto? TeamToAdd
        {
            get => teamToAdd;
            set
            {
                SetProperty(ref teamToAdd, value);
                TeamDetailsString = value?.TeamDetailsString;
            }
        }


        public RelayCommand RemoveTeamCommand { get; }
        public RelayCommand WannaSearchCommand { get; }
        public RelayCommand UpdateDancerCommand { get; }
        public RelayCommand AddTeamCommand { get; }
             

        public UpdateDancerViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            RemoveTeamCommand = new RelayCommand(async() => await RemoveTeam(), CanRemoveTeam);
            WannaSearchCommand = new RelayCommand(async () => await WannaSearch(), CanWannaSearch);
            AddTeamCommand = new RelayCommand(async () => await AddTeam(), CanAddTeam);
            UpdateDancerCommand = new RelayCommand(async () => await UpdateDancer(), CanUpdate);


        }


        private void OnSelectedDancerChanged(DancerDto? selectedDancer)
        {
            if (selectedDancer != null)
            {                
                Name = selectedDancer.Name;
                TimeOfBirth = selectedDancer.TimeOfBirth;
                Teams = selectedDancer.Teams;
            }
        }

        partial void OnNameChanged(string value)
        {
            UpdateDancerCommand.NotifyCanExecuteChanged();
        }
        partial void OnTimeOfBirthChanged(string value)
        {
            UpdateDancerCommand.NotifyCanExecuteChanged();
        }


        private bool CanRemoveTeam()
        {
            if (SelectedDancer != null && SelectedTeam != null)
            {
                return true;
            }
            return false;
        }

        public async Task RemoveTeam() 
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            var request = new RestRequest($"/dancers/{SelectedDancer.DancerId}/teams/{SelectedTeam.TeamId}", Method.Delete);
            var returnedStatus = await client.ExecuteAsync(request, CancellationToken.None);
            if (!returnedStatus.IsSuccessStatusCode)
            {
                JsonSerializerOptions _options = new();
                _options.PropertyNameCaseInsensitive = true;

                ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedStatus.Content ?? "{}", _options)!;
            }
            Teams.Remove(SelectedTeam);
            SelectedTeam = null;
        }

        private bool CanWannaSearch()
        {
            if(selectedDancer != null && selectedTeam == null) 
            {
                return true;    
            }
            return false;
        }

        public async Task WannaSearch()
        {
            await NavigationService.GoToSearchTeam(SelectedDancer);
        }

        private bool CanAddTeam() 
        {
            if (selectedDancer != null && teamToAdd != null)
            {
                return true;
            }
            return false;
        }

        public async Task AddTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            var request = new RestRequest($"/dancers/{SelectedDancer.DancerId}/teams/{teamToAdd.TeamId}", Method.Put);
            var returnedStatus = await client.ExecuteAsync<DancerDto>(request, CancellationToken.None);
            SelectedDancer = returnedStatus.Data;          
            
        }

        private bool CanUpdate()
        {
            var dataWritten = !string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(TimeOfBirth);
            if (dataWritten == true && SelectedDancer != null)
            {
                return true;
            }
            return false;
        }


       public async Task UpdateDancer()
        {
            if (DateOnly.TryParseExact(TimeOfBirth, "dd-MM-yyyy", out var parsedDate))
            {
                var options = new RestClientOptions("https://localhost:7163");
                var client = new RestClient(options);
                // The cancellation token comes from the caller. You can still make a call without it.
                var request = new RestRequest($"/dancers/{SelectedDancer.DancerId}", Method.Put);

                request.AddJsonBody(new { Name, TimeOfBirth = parsedDate });

                var returnedStatus = await client.ExecutePutAsync(request, CancellationToken.None);

                if (!returnedStatus.IsSuccessStatusCode)
                {
                    JsonSerializerOptions _options = new();
                    _options.PropertyNameCaseInsensitive = true;

                    ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedStatus.Content ?? "{}", _options)!;

                }

                SelectedDancer = null;
                //DancerDtoList.Clear();


            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", "Ugyldigt datoformat. Brug venligst dd-MM-yyyy.", "OK");
            }
        }

      

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
            if (query.ContainsKey("dancerDto") && query["dancerDto"] is DancerDto dancerDto) 
               
            {
                SelectedDancer = dancerDto;
                
            }

            if (query.ContainsKey("teamDto") && query["teamDto"] is TeamDto teamDto)
            {
                TeamToAdd = teamDto;
            }
    }


    }

    

    

}
