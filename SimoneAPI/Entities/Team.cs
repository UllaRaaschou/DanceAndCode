namespace SimoneAPI.Entities
{
    public class Team
    {
       
        public Guid? TeamId { get; set; }
        public int Number { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string SceduledTime { get; set; } = string.Empty;
        public ICollection<Dancer> DancersOnTeam { get; set; } = new HashSet<Dancer>();

       
    }
}