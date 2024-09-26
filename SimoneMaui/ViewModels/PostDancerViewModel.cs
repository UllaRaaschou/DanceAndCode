using RestSharp.Authenticators;
using RestSharp;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;

namespace SimoneMaui.ViewModels
{
    public class PostDancerViewModel : INotifyPropertyChanged
    {
        private DateOnly timeOfBirth;
        private string name = string.Empty;

        public string Name
        {
            get => name;
            set
            {
                if (!value.Equals(name))
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateOnly TimeOfBirth
        {
            get => timeOfBirth;
            set
            {
                if (!value.Equals(timeOfBirth))
                {
                    timeOfBirth = value;
                    OnPropertyChanged();
                }
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ICommand PostDancerCommand { get; }

        private async Task PostDancer()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);
            // The cancellation token comes from the caller. You can still make a call without it.
            var response = await client.PostJsonAsync("/dancers", new { Name, TimeOfBirth}, CancellationToken.None);
            

        }

        public PostDancerViewModel()
        {
            PostDancerCommand = new Command(async () => await PostDancer());
        }
            


    }
}

