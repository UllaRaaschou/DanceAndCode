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

        [ObservableProperty]
        private string? dayOfWeekEntry = string.Empty;

        [ObservableProperty]
        private DayOfWeek dayOfWeek = default;

        [ObservableProperty]
        private bool isTrialLesson = false;


        public string DancerDetailsString => SelectedDancer?.DancerDetailsString ?? "";

        [ObservableProperty]
        private string ifTrialLessonIsTrue;

        public DayOfWeek ToDayOfWeekConverter(string dayOfWeekentry)
        {
            return dayOfWeekentry switch
            {
                "Mandag" => DayOfWeek.Monday,
                "Tirsdag" => DayOfWeek.Tuesday,
                "Onsdag" => DayOfWeek.Wednesday,
                "Torsdag" => DayOfWeek.Thursday,
                "Fredag" => DayOfWeek.Friday,
                "Lørdag" => DayOfWeek.Saturday,
                "Søndag" => DayOfWeek.Sunday
            };
        }



        public bool PuttingDancerOnTeam { get; set; } = false;

        public AsyncRelayCommand WannaAddDancerCommand { get; }
        public AsyncRelayCommand WannaDeleteDancerCommand { get; }
        public AsyncRelayCommand WannaAddTrialDancerCommand { get; }
        public AsyncRelayCommand AddTrialDancerCommand { get; }


        public AsyncRelayCommand AddDancerToTeamCommand { get; }
        public AsyncRelayCommand DeleteDancerFromTeamCommand { get; }
      

        public UpdateTeamViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            WannaAddDancerCommand = new AsyncRelayCommand(WannaAddDancer, CanWannaAddDancer);
            WannaDeleteDancerCommand = new AsyncRelayCommand(WannaDeleteDancer, CanWannaDeleteDancer);
            WannaAddTrialDancerCommand = new AsyncRelayCommand(WannaAddTrialDancer, CanWannaAddTrialDancer);
            AddDancerToTeamCommand = new AsyncRelayCommand(AddDancerToTeam, CanAddDancerToTeam);
            AddTrialDancerCommand = new AsyncRelayCommand(AddTrialDancer, CanAddTrialDancer);   
        }

        private bool CanAddTrialDancer()
        {
            if (selectedDancer != null && selectedTeam != null)
            {
                return true;
            }
            return false;
        }

        private async Task AddTrialDancer()
        {
            IsTrialLesson = true;
            await AddDancerToTeam();

        }

        private bool CanWannaAddTrialDancer()
        {
            return true;
        }

        private async Task WannaAddTrialDancer()
        {
            PuttingDancerOnTeam = true;
            await NavigationService.GoToSearchDancer(SelectedTeam, PuttingDancerOnTeam);
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



            partial void OnDayOfWeekEntryChanged(string? newValue)
            {
                DayOfWeekEntry = ToTitleCase(newValue);
            }

            partial void OnNameChanged(string? newValue)
            {
                Name = ToTitleCase(newValue);
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

        private async Task AddDancerToTeam()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);

            var request = new RestRequest($"/Teams/{SelectedTeam.TeamId}/AddToListOfDancers", Method.Put);

            request.AddJsonBody(new { dancerId = SelectedDancer.DancerId, IsTrialLesson });

            var response = await client.ExecuteAsync<TeamDto>(request, CancellationToken.None);

            if (response.Data != null)
            {
                var mauiTeamDto = response.Data;

                mauiTeamDto.DancersOnTeam.First(d => d.DancerId == SelectedDancer.DancerId).IsHighlighted = true;

                var validDancers = mauiTeamDto.DancersOnTeam
                                    .Where(d => d.LastDancedate >= DateOnly.FromDateTime(DateTime.Now))
                                    .ToList();

                DancersOnTeam = new ObservableCollection<DancerDto>(validDancers);

                DancersOnTeam = mauiTeamDto.DancersOnTeam;
                SelectedDancer = null;
                DancerIsSelected = false;
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
                    SelectedTeam = teamDto;
                    Name = teamDto.Name;
                    Number = teamDto.Number;
                    SceduledTime = teamDto.SceduledTime;
                    DancersOnTeam = new ObservableCollection<DancerDto>(teamDto.DancersOnTeam);

                }
            }
    }
}
