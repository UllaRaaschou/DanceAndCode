namespace SimoneBlazor.Domain
{
    public class RelationBlazor
    {    
        public Guid TeamId { get; set; }
        public Guid DancerId { get; set; }
        public string DancerName { get; set; } = string.Empty;
        public DateOnly DancersLastDanceDate { get; set; }
        
        public bool IsChecked { get; set; }

        public Dictionary<DateOnly, bool> Attendances { get; set; } = new Dictionary<DateOnly, bool>();
        
        
    }
}
