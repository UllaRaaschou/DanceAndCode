namespace SimoneBlazor.Domain
{
    public class TeamBlazor
    {
        public Guid TeamId { get; set; }
        public int Number { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string ScheduledTime { get; set; } = string.Empty;
        public DayOfWeek DayOfWeek { get; set; } = default;
        public List<DancerBlazor> DancersOnTeam { get; set; } = new List<DancerBlazor>();

    }
}
