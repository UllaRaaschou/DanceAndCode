using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.StaffMembers
{
    public static class GetStaffById
    {
        public static void RegisterStaffEndpoint(this WebApplication endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPost("", Get);
        }

        public static async Task<Results<NotFound, Ok<GetStaffResponceDto>>> Get(SimoneDbContext dbContext,
        IMapper mapper, Guid staffId)
        {
            var staffMember = await dbContext.Staffs
                .Include(s => s.RegisteredLessons)
                .FirstOrDefaultAsync(s => s.StaffId == staffId);                

            return staffMember != null
            ? TypedResults.Ok(mapper.Map<GetStaffResponceDto>(staffMember))
            : TypedResults.NotFound();
        }

        public class GetStaffResponceDto
        {
            public Guid StaffId { get; set; }
            public string Name { get; set; } = string.Empty;
            public JobRoleEnum Role { get; set; }
            public DateTime TimeofBirth { get; set; }
            public ICollection<RegisteredLessonDto> RegisteredLessonDtos { get; set; } = new HashSet<RegisteredLessonDto>();
        }

        public class RegisteredLessonDto
        {
            public Guid LessonId { get; set; }
            public DateTime Date { get; set; }
            public Guid TeamId { get; set; }
            public Guid StaffId { get; set; }
            public Staff Staff { get; set; } = new Staff();
        }
    }
}
