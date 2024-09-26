using System.ComponentModel;

namespace SimoneMaui.ViewModels
{
    public class TeamViewModel: INotifyPropertyChanged
    {
        public Guid TeamId { get; set; }
        public int Number { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string SceduledTime { get; set; } = string.Empty;
        public ICollection<PostDancerViewModel> Dancers{ get; set; } = new HashSet<PostDancerViewModel>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
       => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}