namespace SimoneMaui.ViewModels
{
    public class DancerViewModel
    {
        public Guid DancerId { get; set; }
        public string Name { get; set; }
        public DateOnly TimeOfBirth { get; set; }
        public ICollection<TeamViewModel> Teams { get; set; } = new List<TeamViewModel>();
    }
}

