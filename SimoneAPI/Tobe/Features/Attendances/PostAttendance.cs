using AutoMapper;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Attendances
{
    public class PostAttendance
    {
        public void RegisterAttendanceEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPost("", Post);
        }

        public static async Task<IResult> Post (SimoneDbContext dBContext, IMapper mapper, PostAttendanceDto dto)
        {
            var attendance = mapper.Map<Attendance>(dto);
            dBContext.Attendances.Add(attendance);
            await dBContext.SaveChangesAsync();
            var responceDto = mapper.Map<PostAttendanceResponceDto>(attendance);
            return TypedResults.Created($"/TeamDancerRelations/Attendances/{responceDto.AttendanceId}", responceDto);

        }

        public class PostAttendanceDto 
        {
            public Guid TeamDancerRelationId { get; set; }
            public DateTime Date { get; set; }
            public bool IsPresent { get; set; } = false;
            public string Note { get; set; } = string.Empty;
            public TeamDancerRelation TeamDancerRelation { get; set; } = new TeamDancerRelation();
        }

        public class PostAttendanceResponceDto
        {
            public Guid AttendanceId { get; set; }
            public Guid TeamDancerRelationId { get; set; }
            public DateTime Date { get; set; }
            public bool IsPresent { get; set; } = false;
            public string Note { get; set; } = string.Empty;
            public TeamDancerRelation TeamDancerRelation { get; set; } = new TeamDancerRelation();
        }


    }
}
