using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.ObjectModel;
using System.Text.Json;
using SimoneMaui.ViewModels.Dtos;

namespace SimoneMaui.ViewModels
{
    public partial class DeleteDancerViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private ObservableCollection<DancerDto> dancerDtoList;

        [ObservableProperty]
        private string? name = string.Empty;

        [ObservableProperty]
        private string? timeOfBirth = string.Empty;

        partial void OnNameChanged(string value)
        {
            DeleteDancerCommand.NotifyCanExecuteChanged();
        }
        partial void OnTimeOfBirthChanged(string value)
        {
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


       public RelayCommand DeleteDancerCommand { get; }

        public DeleteDancerViewModel()
        {
            DeleteDancerCommand = new RelayCommand(async () => await DeleteDancer(), CanDelete);
            DancerDtoList = new ObservableCollection<DancerDto>()
            {
                new DancerDto{Name="Test", TimeOfBirth="01-01-2001" }
            };


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

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }  
}
