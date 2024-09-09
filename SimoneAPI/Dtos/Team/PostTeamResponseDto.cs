using SimoneAPI.Dtos.Dancer;
namespace SimoneAPI.Dtos.Team
{
    public class PostTeamResponseDto
    {
        public Guid TeamId { get; set; }
        public int Number { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string SceduledTime { get; set; } = string.Empty ;
        public ICollection<RequestDancerDto> EnrolledDancers { get; set; } = new List<RequestDancerDto>();
    }
}

       
        