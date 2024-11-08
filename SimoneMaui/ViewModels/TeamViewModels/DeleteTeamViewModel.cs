﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using SimoneMaui.ViewModels.Dtos;
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;

namespace SimoneMaui.ViewModels
{
    public partial class DeleteTeamViewModel : ObservableObject, IQueryAttributable
    {
        public AsyncRelayCommand DeleteTeamCommand { get; }

        [ObservableProperty]
        private string teamDetailsString;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteTeamCommand))]
        private TeamDto teamToDelete;

        public DeleteTeamViewModel() 
        {
            DeleteTeamCommand = new AsyncRelayCommand(DeleteTeamAsync, CanDeleteTeam);
        }

        private bool CanDeleteTeam() 
        {
            if (TeamToDelete != null) 
            {
                return true; 
            } 
            return false;
        }

        public event Action<string> TeamDeleted;

        
        public async Task DeleteTeamAsync() 
        {
            if(teamToDelete != null) 
            {
                var options = new RestClientOptions("https://localhost:7163");
                var client = new RestClient(options);
                // The cancellation token comes from the caller. You can still make a call without it.
                var request = new RestRequest($"/teams/{TeamToDelete.TeamId}", Method.Delete);

                var response = await client.ExecuteDeleteAsync(request, CancellationToken.None);

                if (!response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions _options = new();
                    _options.PropertyNameCaseInsensitive = true;

                    ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(response.Content ?? "{}", _options)!;
                }
            }
            
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", "Der er ikke valgt et hold", "OK");
            }

            TeamDeleted?.Invoke("Hold slettet");
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("teamDto") && query["teamDto"] is TeamDto teamDto)
            {
                TeamToDelete = teamDto;
                TeamDetailsString = TeamToDelete.TeamDetailsString;

            }
        }
    }       
    
}

