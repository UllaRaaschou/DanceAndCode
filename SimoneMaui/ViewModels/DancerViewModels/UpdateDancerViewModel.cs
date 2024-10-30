﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        public INavigationService NavigationService { get; set; }

        [ObservableProperty]
        private DancerDto? selectedDancer;

        partial void OnSelectedDancerChanged(DancerDto? value)
        {
            if (value != null)
            {
                Name = value.Name;
                TimeOfBirth = value.TimeOfBirth;
                Teams = value.Teams;
            }
        }


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TeamDetailsString))]
        [NotifyCanExecuteChangedFor(nameof(RemoveTeamCommand))]
        private TeamDto? selectedTeam;

        [ObservableProperty]
        private string? name = string.Empty;

        [ObservableProperty]
        private string? timeOfBirth = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Count))]
        [NotifyPropertyChangedFor(nameof(DancerIsSignedInToMinimumOneTeam))]
        private ObservableCollection<TeamDto> teams;


        public int? Count => Teams?.Count() ?? 0;

        public bool DancerIsSignedInToMinimumOneTeam => Count > 0;
        public string TeamDetailsString => SelectedTeam?.TeamDetailsString ?? "";

        [ObservableProperty]
        private bool isStartOfProcedure = true;

        [ObservableProperty]
        private bool wannaAddTeamToADancer = false;


        public bool TeamToAddIsSelected => teamToAdd != null;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TeamToAddIsSelected))]
        private TeamDto? teamToAdd;
       

        public RelayCommand RemoveTeamCommand { get; }
        //public RelayCommand WannaSearchCommand { get; }
        public RelayCommand UpdateDancerCommand { get; }
        public RelayCommand AddTeamCommand { get; }
        public AsyncRelayCommand WannaPutDancerOnATeamCommand { get; }
        public AsyncRelayCommand WannaDeleteDancerFromTeamCommand { get; }

        public UpdateDancerViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            WannaPutDancerOnATeamCommand = new AsyncRelayCommand(WannaPutDancerOnATeam, CanWannaPutDancerOnATeam);
            WannaDeleteDancerFromTeamCommand = new AsyncRelayCommand(WannaDeleteDancerFromTeam, CanWannaDeleteDancerFromTeam);

            RemoveTeamCommand = new RelayCommand(async() => await RemoveTeam(), CanRemoveTeam);
            AddTeamCommand = new RelayCommand(async () => await AddTeam(), CanAddTeam);            
           
            UpdateDancerCommand = new RelayCommand(async () => await UpdateDancer(), CanUpdate);
        }

        public event Action<string> TellUserToChoseTeam;


        private bool CanWannaPutDancerOnATeam()
        {
            return SelectedDancer != null;
        }
        private async Task WannaPutDancerOnATeam()
        {

            WannaAddTeamToADancer = true;
            await NavigationService.GoToSearchTeam(SelectedDancer, WannaAddTeamToADancer);
        }


        private bool CanWannaDeleteDancerFromTeam()
        {
            return SelectedDancer != null;
        }
        private async Task WannaDeleteDancerFromTeam()
        {
            TellUserToChoseTeam.Invoke("Vælg hold, som eleven skal slettes på");
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
            IsStartOfProcedure = false;
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
            Teams = SelectedDancer.Teams;
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
                Teams = dancerDto.Teams;                               
            }

            if (query.ContainsKey("teamDto") && query["teamDto"] is TeamDto teamDto)
            {
                TeamToAdd = teamDto;
            }
    }


    }    

}
