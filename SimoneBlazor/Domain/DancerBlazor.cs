﻿namespace SimoneBlazor.Domain
{
    public class DancerBlazor
    {
        public Guid DancerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly TimeOfBirth { get; set; }
        public bool IsAttending { get; set; } = true;
        public List<TeamBlazor> TeamDancerRelations { get; set; } = new();
    }
}