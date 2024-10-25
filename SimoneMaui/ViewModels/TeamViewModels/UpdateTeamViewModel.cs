﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SimoneMaui.Navigation;
using System.Collections.ObjectModel;
using System.Text.Json;
using SimoneMaui.ViewModels.Dtos;
using CommunityToolkit.Maui;

namespace SimoneMaui.ViewModels
{
    public partial class UpdateTeamViewModel : ObservableObject, IQueryAttributable
    {
        public INavigationService NavigationService { get; set; }

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private string? timeOfBirth;

        [ObservableProperty]
        private string? number;

        [ObservableProperty]
        private string? sceduledTime;

        [ObservableProperty]
        private TeamDto? selectedTeam;

        [ObservableProperty]
        private DancerDto? selectedDancer;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Count))]
        private ObservableCollection<DancerDto> dancersOnTeam = new ObservableCollection<DancerDto>();

        [ObservableProperty]
        private ObservableCollection<TeamDto> teams;

        [ObservableProperty]
        private string? teamDetailsString;

        [ObservableProperty]
        private bool isHighlighted;

        [ObservableProperty]
        private DancerDto dancerToDelete;

        [ObservableProperty]
        private bool dancerIsSelected = false;

        [ObservableProperty]
        private bool isStartOfProcedure = true;

        public int Count => DancersOnTeam.Count;
        


        public bool PuttingDancerOnTeam { get; set; } = false;

        public AsyncRelayCommand WannaAddDancerCommand { get; }
        public AsyncRelayCommand WannaDeleteDancerCommand { get; }


        public AsyncRelayCommand AddDancerToTeamCommand { get; }
        public AsyncRelayCommand DeleteDancerFromTeamCommand { get; }

        public UpdateTeamViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            WannaAddDancerCommand = new AsyncRelayCommand(WannaAddDancer, CanWannaAddDancer);
            WannaDeleteDancerCommand = new AsyncRelayCommand(WannaDeleteDancer, CanWannaDeleteDancer);
            AddDancerToTeamCommand = new AsyncRelayCommand(AddDancerToTeam, CanAddDancerToTeam);
        }

        partial void OnSelectedDancerChanged(DancerDto value) 
        {
            if(value != null) 
            {
                DancerIsSelected = true;
                IsStartOfProcedure = false;
            }
        }

        private bool CanAddDancerToTeam()
        {
            if (selectedDancer != null && selectedTeam != null)
            {
                return true;
            }
            return false;
        }

        private async Task AddDancerToTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            // The cancellation token comes from the caller. You can still make a call without it.
            var request = new RestRequest($"/Teams/{SelectedTeam.TeamId}/AddToListOfDancers", Method.Put);

            request.AddJsonBody(new { dancerId = SelectedDancer.DancerId });

            var returnedStatus = await client.ExecutePutAsync(request, CancellationToken.None);

            JsonSerializerOptions _options = new();
            _options.PropertyNameCaseInsensitive = true;
            if (!returnedStatus.IsSuccessStatusCode)
            {            
                ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedStatus.Content ?? "{}", _options)!;
            }

            var mauiTeamDto = JsonSerializer.Deserialize<TeamDto>(returnedStatus.Content ?? "{}", _options);
            DancersOnTeam.Clear();
            foreach(var dancerDto in mauiTeamDto.DancersOnTeam)
            {
                dancerDto.IsHighlighted = dancerDto.DancerId == SelectedDancer.DancerId;
                DancersOnTeam.Add(dancerDto);
            }
            DancersOnTeam = mauiTeamDto.DancersOnTeam;
            SelectedDancer = null;
            DancerIsSelected = false;

        }

        public bool CanDeleteDancerFromTeam()
        {
            if (DancerToDelete != null && selectedTeam != null)
            {
                return true;
            }
            return false;
        }

        private bool CanWannaAddDancer()
        {
            return true;
        }
        public async Task WannaAddDancer()
        {
            PuttingDancerOnTeam = true;
            await NavigationService.GoToSearchDancer(SelectedTeam, PuttingDancerOnTeam);
        }



        private bool CanWannaDeleteDancer()
        {
            if(SelectedTeam != null && DancerIsSelected==false)
            {
                return true;
            }
            return false;
        }
        public async Task WannaDeleteDancer()
        {
            await NavigationService.GoToDeleteDancerFromTeam(SelectedTeam, DancerToDelete);
        }


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("dancerDto") && query["dancerDto"] is DancerDto dancerDto)

            {
                SelectedDancer = dancerDto;

            }

            if (query.ContainsKey("teamDto") && query["teamDto"] is TeamDto teamDto)
            {
                SelectedTeam = teamDto;
                Name = teamDto.Name;
                Number = teamDto.Number;
                SceduledTime = teamDto.SceduledTime;
                DancersOnTeam = new ObservableCollection<DancerDto>(teamDto.DancersOnTeam);

            }
        }
    }
}