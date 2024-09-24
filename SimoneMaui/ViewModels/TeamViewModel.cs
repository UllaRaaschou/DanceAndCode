namespace SimoneMaui.ViewModels
{
    public class TeamViewModel
    {
        public Guid TeamId { get; set; }
        public int Number { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string SceduledTime { get; set; } = string.Empty;
        public ICollection<DancerViewModel> Dancers{ get; set; } = new HashSet<DancerViewModel>();
    }
}