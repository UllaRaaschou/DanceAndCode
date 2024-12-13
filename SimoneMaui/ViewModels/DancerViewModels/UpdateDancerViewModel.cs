using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SimoneMaui.Navigation;
using System.Collections.ObjectModel;
using System.Text.Json;
using RestSharp.Serializers.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Globalization;
using SimoneMaui.Models;

namespace SimoneMaui.ViewModels
{
    public partial class UpdateDancerViewModel : ObservableValidator, INotifyPropertyChanged, IQueryAttributable
    {
        public INavigationService NavigationService { get; set; }

        private readonly NavigationManager _navigationManager;

        [ObservableProperty]
        private DancerDto? selectedDancer;
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TeamDetailsString))]
        [NotifyCanExecuteChangedFor(nameof(RemoveTeamCommand))]
        [NotifyCanExecuteChangedFor(nameof(WannaDeleteDancerFromTeamCommand))]
        private TeamDto? selectedTeam;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Navn må fylde mellem 2 og 50 tegn")]
        [ObservableProperty]
        private string? name = string.Empty;

        [Required]
        [ObservableProperty]
        private string? timeOfBirth = string.Empty;      

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Count))]
        [NotifyPropertyChangedFor(nameof(DancerIsSignedInToMinimumOneTeam))]
        private ObservableCollection<TeamDto> teams;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TeamToAddIsSelected))]
        private TeamDto? teamToAdd;

        [ObservableProperty]
        private bool isStartOfProcedure = true;

        [ObservableProperty]
        private bool isEndOfProcedure = true;

        [ObservableProperty]
        private bool wannaAddTeamToADancer = false;
        
        [ObservableProperty]
        private bool wannaAddTrialLessonToADancer = false;

        [ObservableProperty]
        private bool isTrialLesson = false;



        public int? Count => Teams?.Count() ?? 0;
        public bool DancerIsSignedInToMinimumOneTeam => (Count > 0 && SelectedTeam != null);
        public string TeamDetailsString => SelectedTeam?.TeamDetailsString ?? "";      
        public bool TeamToAddIsSelected => teamToAdd != null;


        public RelayCommand RemoveTeamCommand { get; }
        public AsyncRelayCommand FinalUpdateDancerCommand { get; }
        public RelayCommand AddTeamCommand { get; }
        public AsyncRelayCommand WannaPutDancerOnATeamCommand { get; }
        public AsyncRelayCommand WannaDeleteDancerFromTeamCommand { get; }
        public AsyncRelayCommand WannaAddTeamTrialLessonCommand { get; private set; }
        public AsyncRelayCommand AddTeamTrialLessonCommand { get; }
        public AsyncRelayCommand NavigateToFirstPageCommand { get; set; }
        public AsyncRelayCommand NavigateBackCommand { get; }
        public AsyncRelayCommand NavigateForwardCommand { get; }
        public bool DancerToAddIsSelected { get; private set; } = false;       
       
        // public bool TimeOfBirthValidated => ValidateTimeOfBirth(TimeOfBirth);
        public bool ValidateTimeOfBirth(string value)
        {
            if (DateOnly.TryParseExact(TimeOfBirth, "dd-MM-yyyy", out var parsedDate))
            {
                if (parsedDate.Year < 1940 || parsedDate.Year > DateTime.Now.Year)
                {
                    ErrorInDate.Invoke("Året skal være mellem 1940 og nu.");
                    return false;
                }

                //if (parsedDate.Month < 1 || parsedDate.Month > 12)
                //{
                //    ErrorInDate.Invoke("Vælg korrekt måned");
                //    return false;
                //}

                //if (parsedDate.Day < 1 || parsedDate.Day > DateTime.DaysInMonth(parsedDate.Year, parsedDate.Month))
                //{
                //    ErrorInDate.Invoke("Vælg korrekt dato");
                //    return false;
                //}
                else
                {
                    TimeOfBirth = value;
                    return true;
                }
            }
            else
            {
                ErrorInDate.Invoke("Problemer med datoformatet");
                return false;
            }
        }

        public event Action<string> ErrorInDate;
        public event Action<string> TellUserToChoseTeam;




        public UpdateDancerViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            _navigationManager = new NavigationManager(navigationService);
            WannaPutDancerOnATeamCommand = new AsyncRelayCommand(WannaPutDancerOnATeam, CanWannaPutDancerOnATeam);
            WannaDeleteDancerFromTeamCommand = new AsyncRelayCommand(WannaDeleteDancerFromTeam, CanWannaDeleteDancerFromTeam);
            WannaAddTeamTrialLessonCommand = new AsyncRelayCommand(WannaAddTeamTrialLesson, CanWannaAddTeamTrialLesson);

            RemoveTeamCommand = new RelayCommand(async() => await RemoveTeam(), CanRemoveTeam);
            AddTeamCommand = new RelayCommand(async () => await AddTeam(), CanAddTeam);       

            AddTeamTrialLessonCommand = new AsyncRelayCommand(AddTeamTrialLesson, CanAddTeamTrialLesson);
            FinalUpdateDancerCommand = new AsyncRelayCommand(FinalUpdateDancer, CanFinalUpdateDancer);

            NavigateBackCommand = new AsyncRelayCommand(_navigationManager.NavigateBack, _navigationManager.CanNavigateBack);
            NavigateForwardCommand = new AsyncRelayCommand(_navigationManager.NavigateForward, _navigationManager.CanNavigateForward);
            NavigateToFirstPageCommand = new AsyncRelayCommand(_navigationManager.NavigateToFirstPage);
        }




        // Ønsker at tildele Prøvetime
        private bool CanWannaAddTeamTrialLesson()
        {
            return SelectedDancer != null;
        }
        private async Task WannaAddTeamTrialLesson()
        {
            WannaAddTeamToADancer = false;
            WannaAddTrialLessonToADancer = true;

            await NavigationService.GoToSearchTeam(SelectedDancer, WannaAddTeamToADancer, WannaAddTrialLessonToADancer); 
        }



        // Tildel Prøvetime
        private bool CanAddTeamTrialLesson()
        {
            return (SelectedDancer != null && TeamToAdd != null);
        }
        private async Task AddTeamTrialLesson()
        {
            IsTrialLesson = true;
            await AddTeam();
        }

       

        // Ønsker at sætte elev på dansehold
        private bool CanWannaPutDancerOnATeam()
        {
            
            return SelectedDancer != null;
        }
        private async Task WannaPutDancerOnATeam()       
        {
            WannaAddTeamToADancer = true;
            WannaAddTrialLessonToADancer = false;
            await NavigationService.GoToSearchTeam(SelectedDancer, WannaAddTeamToADancer, WannaAddTrialLessonToADancer);
        }


        // Ønsker at slette elev fra dansehold
        private bool CanWannaDeleteDancerFromTeam()
        {
            return SelectedDancer != null;

        }
        private async Task WannaDeleteDancerFromTeam()
        {
            if(SelectedTeam == null) 
            {
                TellUserToChoseTeam.Invoke("Vælg hold, som eleven skal slettes på");

            }
            else
            {
                await RemoveTeam();
            }
           

        }


        // Sletter elev fra dansehold
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

        
        //Sætter eleven på et dansehold
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
            request.AddJsonBody(new { isTrialLesson = IsTrialLesson });
            var returnedStatus = await client.ExecuteAsync<DancerDto>(request, CancellationToken.None);
            if (returnedStatus.Data != null)
            {
                var mauiDancerDto = returnedStatus.Data;

                var validTeams = mauiDancerDto.Teams
                                        .Where(d => d.LastDancedate >= DateOnly.FromDateTime(DateTime.Now))
                                        .ToList();

                Teams = new ObservableCollection<TeamDto>(validTeams);
                TeamToAdd = null;
                WannaAddTeamToADancer = false;
            }
        }


        // Opdater og Gem-funktionen
        private bool CanFinalUpdateDancer()
        {
            var dataWritten = !string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(TimeOfBirth);
            if (dataWritten == true && SelectedDancer != null)
            {
                return true;
            }
            return false;
        }
       public async Task FinalUpdateDancer()
        {
            if (ValidateTimeOfBirth(TimeOfBirth)) 
            {
                if (DateOnly.TryParseExact(TimeOfBirth, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    var options = new RestClientOptions("https://localhost:7163");
                    var client = new RestClient(options, configureSerialization: s =>
                    {
                        s.UseSystemTextJson(new JsonSerializerOptions
                        {
                            Converters = { new JsonStringEnumConverter() }
                        });
                    });
                    // The cancellation token comes from the caller. You can still make a call without it.
                    var request = new RestRequest($"/dancers/{SelectedDancer.DancerId}", Method.Put);

                    request.AddJsonBody(new { Name, TimeOfBirth = parsedDate });

                    var returnedStatus = await client.PutAsync<DancerDto>(request, CancellationToken.None);


                    SelectedDancer = null;



                    await Shell.Current.GoToAsync($"//firstPage?unique={Guid.NewGuid()}");
                    //DancerDtoList.Clear();


                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Fejl", "Ugyldigt datoformat. Brug venligst dd-MM-yyyy.", "OK");
                }
            }           
        }


        partial void OnSelectedDancerChanged(DancerDto? value)
        {
            if (value != null)
            {
                Name = value.Name;
                TimeOfBirth = value.TimeOfBirth;
                Teams = value.Teams;
            }
        }
        partial void OnNameChanged(string? newValue)
        {
            Name = ToTitleCase(newValue);
            if (SelectedDancer != null)
            {
                SelectedDancer.Name = newValue;
            }
            FinalUpdateDancerCommand.NotifyCanExecuteChanged();
        }

        partial void OnTimeOfBirthChanged(string? newValue)
        {
            if(newValue?.Length == 10) 
            {
                if (ValidateTimeOfBirth(newValue))
                {
                    TimeOfBirth = newValue;

                    if (SelectedDancer != null)
                    {
                        SelectedDancer.TimeOfBirth = newValue;
                    }
                    FinalUpdateDancerCommand.NotifyCanExecuteChanged();
                }
            }         
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
            if (query.ContainsKey("dancerDto") && query["dancerDto"] is DancerDto dancerDto) 
               
            {
                SelectedDancer = dancerDto;
                Teams = dancerDto.Teams;
                DancerToAddIsSelected = true;
            }

            if (query.ContainsKey("teamDto") && query["teamDto"] is TeamDto teamDto)
            {
                TeamToAdd = teamDto;
                IsStartOfProcedure = false;
            }

            if (query.ContainsKey("WannaAddTeamToADancer") && query["WannaAddTeamToADancer"] is bool wannaAddTeamToADancer)
            {
                WannaAddTeamToADancer = wannaAddTeamToADancer;
            }

            if(query.ContainsKey("WannaAddTrialLessonToADancer") && query["WannaAddTrialLessonToADancer"] is bool wannaAddTrialLessonToADancer)
            {
                WannaAddTrialLessonToADancer = wannaAddTrialLessonToADancer;
            }
        }
    }    
}
