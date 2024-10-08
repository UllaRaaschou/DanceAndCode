using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;

namespace SimoneMaui.ViewModels
{
    public partial class UpdateDancerViewmodel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private DancerDto? selectedDancer = null;
        
        
        [ObservableProperty]
        private string? name = string.Empty;

        [ObservableProperty]
        private string? timeOfBirth = string.Empty;

        [ObservableProperty]
        private string teamDetailsString = string.Empty;

        partial void OnSelectedDancerChanged(DancerDto? selecetedDancer)
        {
            if (selectedDancer != null)
            {
                Name = selectedDancer.Name;
                TimeOfBirth = selectedDancer.TimeOfBirth;
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


        [ObservableProperty]
        private TeamDto? selectedTeam = null;

        [ObservableProperty]
        private ObservableCollection<TeamDto> teams = new ObservableCollection<TeamDto>();

        public RelayCommand SearchDancerCommand { get; }
        public RelayCommand UpdateDancerCommand { get; }

        public UpdateDancerViewmodel()
        {
            UpdateDancerCommand = new RelayCommand(async () => await UpdateDancer(), CanUpdate);
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


        private async Task<ObservableCollection<DancerDto>> SearchDancer()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            var request = new RestRequest("/dancers/SearchForDancerFromNameOrBirthday", Method.Get);

            if (!string.IsNullOrEmpty(TimeOfBirth))
            {
                DateOnly.TryParseExact(TimeOfBirth, "dd-MM-yyyy", out var parsedDate);
                request.AddOrUpdateParameter("TimeOfBirth", parsedDate); //.AddJsonBody(new { Name, TimeOfBirth = parsedDate });
            }
            if (Name != null)
            {
                request.AddOrUpdateParameter("name", Name);
                // request.AddJsonBody(new { Name, TimeOfBirth });
            }

            // The cancellation token comes from the caller. You can still make a call without it.

            var returnedCollection = await client.ExecuteGetAsync<List<DancerDto>>(request, CancellationToken.None);

            if (!returnedCollection.IsSuccessStatusCode)
            {
                JsonSerializerOptions _options = new();
                _options.PropertyNameCaseInsensitive = true;

                ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedCollection.Content ?? "{}", _options)!;

            }

            Name = string.Empty;
            TimeOfBirth = string.Empty;

            if (!returnedCollection.Data.Any() || returnedCollection.Data == null)
            {
                return null;
            }

            var dancerCollection = new ObservableCollection<DancerDto>(returnedCollection.Data);

            //foreach (var dancer in dancerCollection)
            //{
            //    dancer.TeamDetails = new ObservableCollection<string>(dancer.Teams.Select(team => team.TeamDetailsString));
            //}

            //DancerDtoList.Clear();
            foreach (var item in dancerCollection)
            {
                //DancerDtoList.Add(item);

            }
            return dancerCollection;
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
            Name = dancerDto.Name; // Assuming DancerDto has a Name property
            TimeOfBirth = dancerDto.TimeOfBirth; // Assuming DancerDto has a TimeOfBirth property
        }
    }


    }

    public class UpdateDancerDto
    {
        public Guid DancerId { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string TimeOfBirth { get; set; } = string.Empty;
        public ObservableCollection<TeamDto> Teams { get; set; } = new ObservableCollection<TeamDto>();
    }

    public class UpdateTeamDto
    {
        public Guid TeamId { get; set; } = Guid.Empty;
        public int Number { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string SceduledTime { get; set; } = string.Empty;
        public string TeamDetailsString => $"Hold {Number} '{Name}' - {SceduledTime}";



    }

}
