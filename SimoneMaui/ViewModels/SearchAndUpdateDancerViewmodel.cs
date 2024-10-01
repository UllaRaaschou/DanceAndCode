using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace SimoneMaui.ViewModels
{
    public partial class SearchAndUpdateDancerViewmodel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<DancerDto> dancerDtoList = new ObservableCollection<DancerDto>();

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string timeOfBirth;

        private DancerDto selectedDancer;
        public DancerDto SelectedDancer
        {
            get => selectedDancer;
            set
            {
                if (selectedDancer != value)
                {
                    selectedDancer = value;
                    OnPropertyChanged();

                    //Her sørger jeg for, at de observable Proprties sættes til værdierne for den selectede danser
                    Name = selectedDancer.Name; 
                    TimeOfBirth = selectedDancer.TimeOfBirth;
                    
                    OnPropertyChanged(nameof(Name));
                    OnPropertyChanged(nameof(TimeOfBirth));
                };
            }           
        }

        
        public RelayCommand SearchDancerCommand { get; }
        public RelayCommand UpdateDancerCommand {get; }

        public SearchAndUpdateDancerViewmodel() 
        {
            SearchDancerCommand = new RelayCommand(async () => await SearchDancer());
            UpdateDancerCommand = new RelayCommand(async () => await UpdateDancer());

        }
        private async Task<ObservableCollection<DancerDto>> SearchDancer()
        {
            if (DateOnly.TryParseExact(TimeOfBirth, "dd-MM-yyyy", out var parsedDate))
            {
                var options = new RestClientOptions("https://localhost:7163");
                var client = new RestClient(options);
                // The cancellation token comes from the caller. You can still make a call without it.
                var request = new RestRequest("/dancers/SerachForDancerFromNameOrBirthday", Method.Get);
                request.AddJsonBody(new { Name, TimeOfBirth = parsedDate });

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
                return dancerCollection;

            }
            else { return null; }
        }

        public async Task UpdateDancer() 
        {
            if (DateOnly.TryParseExact(TimeOfBirth, "dd-MM-yyyy", out var parsedDate))
            {
                var options = new RestClientOptions("https://localhost:7163");
                var client = new RestClient(options);
                // The cancellation token comes from the caller. You can still make a call without it.
                var request = new RestRequest("/dancers", Method.Put);
                request.AddJsonBody(new { Name, TimeOfBirth = parsedDate });

                var returnedStatus = await client.ExecuteGetAsync(request, CancellationToken.None);

                if (!returnedStatus.IsSuccessStatusCode)
                {
                    JsonSerializerOptions _options = new();
                    _options.PropertyNameCaseInsensitive = true;

                    ProblemDetails details = JsonSerializer.Deserialize<ProblemDetails>(returnedStatus.Content ?? "{}", _options)!;

                }
            }
            else 
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", "Ugyldigt datoformat. Brug venligst dd-MM-yyyy.", "OK");
            }
        }
        

       
    }

    public class DancerDto
    {
        public string Name { get; set; } = string.Empty;
        public string TimeOfBirth { get; set;} = string.Empty;
    }
}
