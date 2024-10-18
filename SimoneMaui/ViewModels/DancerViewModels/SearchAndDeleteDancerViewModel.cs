using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.ObjectModel;
using System.Text.Json;
using SimoneMaui.ViewModels.Dtos;


namespace SimoneMaui.ViewModels
{
    public partial class SearchAndDeleteDancerViewmodel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<DancerDto> dancerDtoList;

        [ObservableProperty]
        private string? name = string.Empty;

        [ObservableProperty]
        private string? timeOfBirth = string.Empty;

        partial void OnNameChanged(string value)
        {
            SearchDancerCommand.NotifyCanExecuteChanged();
            DeleteDancerCommand.NotifyCanExecuteChanged();
        }
        partial void OnTimeOfBirthChanged(string value)
        {
            SearchDancerCommand.NotifyCanExecuteChanged();
            DeleteDancerCommand.NotifyCanExecuteChanged();
        }
        

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


        public RelayCommand SearchDancerCommand { get; }
        public RelayCommand DeleteDancerCommand { get; }

        public SearchAndDeleteDancerViewmodel()
        {
            SearchDancerCommand = new RelayCommand(async () => await SearchDancer(), CanSearch);
            DeleteDancerCommand = new RelayCommand(async () => await DeleteDancer(), CanDelete);
            DancerDtoList = new ObservableCollection<DancerDto>()
            {
                new DancerDto{Name="Test", TimeOfBirth="01-01-2001" }
            };


        }

        private bool CanSearch()
        {
            var dataWritten = !string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(TimeOfBirth);
            if (dataWritten == true && SelectedDancer == null)
            {
                return true;
            }
            return false;
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

            DancerDtoList.Clear();
            foreach (var item in dancerCollection)
            {
                DancerDtoList.Add(item);

            }
            return dancerCollection;
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
                DancerDtoList.Clear();


            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", "Der er ikke valgt en danser", "OK");
            }
        }



    }

   
}
