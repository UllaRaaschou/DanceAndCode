using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Attendances
{
    public class UpdateAttendance
    {
        public void RegisterAttendanceEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPut("", Put);
        }

        public static async Task<IResult> Put(SimoneDbContext dBContext, IMapper mapper, PutAttendanceDto dto)
        {
            var attendanceToUpdate = await dBContext.Attendances.FirstOrDefaultAsync( a => a.AttendanceId == dto.AttendanceId );
            if (attendanceToUpdate == null)
            {
                return TypedResults.NotFound();
            }
            mapper.Map(dto, attendanceToUpdate);
            await dBContext.SaveChangesAsync();
            var responceDto = mapper.Map<PutAttendanceResponceDto>(attendanceToUpdate);            
            return TypedResults.Ok(responceDto);

        }

        public class PutAttendanceDto
        {
            public Guid AttendanceId { get; set; }
            public Guid TeamDancerRelationId { get; set; }
            public DateTime Date { get; set; }
            public bool IsPresent { get; set; } = false;
            public string Note { get; set; } = string.Empty;
            public TeamDancerRelation TeamDancerRelation { get; set; } = new TeamDancerRelation();
        }

        public class PutAttendanceResponceDto
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
