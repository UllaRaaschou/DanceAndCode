﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SimoneMaui.Models;
using SimoneMaui.Navigation;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace SimoneMaui.ViewModels.TeamViewModels
{
    public partial class DeleteDancerFromTeamViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private TeamDto selectedTeam;

        [ObservableProperty]
        private DancerDto dancerToDelete;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Count))]
        private ObservableCollection<DancerDto> dancersOnTeam = new ObservableCollection<DancerDto>();
        public int Count => DancersOnTeam.Count;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string timeOfBirth;

        [ObservableProperty]
        private bool dancerToDeleteIsSelected = false;

        [ObservableProperty]
        private bool dancerIsdeleted = false;

        [ObservableProperty]
        private string teamDetailsString = string.Empty;

        public AsyncRelayCommand NavigateToFirstPageCommand { get; set; }
        public AsyncRelayCommand NavigateBackCommand { get; }
        public AsyncRelayCommand NavigateForwardCommand { get; }

        public AsyncRelayCommand DeleteDancerFromTeamCommand { get; set; }
        public INavigationService NavigationService { get; private set; }

        private readonly NavigationManager _navigationManager;

        public DeleteDancerFromTeamViewModel(INavigationService navigationService) 
        {
            NavigationService = navigationService;
            _navigationManager = new NavigationManager(navigationService);
            DeleteDancerFromTeamCommand = new AsyncRelayCommand(DeleteDancerFromTeam, CanDeleteDancerFromTeam);
            NavigateBackCommand = new AsyncRelayCommand(_navigationManager.NavigateBack, _navigationManager.CanNavigateBack);
            NavigateForwardCommand = new AsyncRelayCommand(_navigationManager.NavigateForward, _navigationManager.CanNavigateForward);
            NavigateToFirstPageCommand = new AsyncRelayCommand(_navigationManager.NavigateToFirstPage);
        }

       
        partial void OnDancerToDeleteChanged(DancerDto? newValue)
        {
            DancerToDeleteIsSelected = true;
        }


        private bool CanDeleteDancerFromTeam()
        {
            //if(DancerToDeleteIsSelected == true && SelectedTeam!= null && DancerToDelete!= null) 
            //{
            //    return true;
            //}
            //return false;
            return true;
        }

        public async Task DeleteDancerFromTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);

            var request = new RestRequest($"/Teams/{SelectedTeam.TeamId}/DeleteFromListOfDancers", Method.Delete);
            request.AddJsonBody(new { dancerId = DancerToDelete.DancerId});

            var returnedStatus = await client.ExecuteDeleteAsync<TeamDto>(request, CancellationToken.None);
            if (returnedStatus.IsSuccessStatusCode)
            {
                TeamDto returnedDto = returnedStatus.Data!;
                SelectedTeam = returnedDto;
                DancersOnTeam = returnedDto.DancersOnTeam;
                //DancersOnTeam.Clear();
                //foreach (var dancerDto in returnedDto.DancersOnTeam)
                //{
                //    DancersOnTeam.Add(dancerDto);
                //}

                if (!returnedStatus.IsSuccessStatusCode)
                {
                    JsonSerializerOptions _options = new();
                    _options.PropertyNameCaseInsensitive = true;

                    ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedStatus.Content ?? "{}", _options)!;
                }
                DancerToDeleteIsSelected = false;
                DancerIsdeleted = true;
            }

        }


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("teamDto") && query["teamDto"] is TeamDto selectedTeam)

            {
                SelectedTeam = selectedTeam;
                DancersOnTeam = selectedTeam.DancersOnTeam;

            }           
        }
    }
}
