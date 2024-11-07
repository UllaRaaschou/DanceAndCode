using AutoMapper;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.StaffMembers
{
    public static class PostStaff
    {     
        public static async Task<IResult> Post(SimoneDbContext dBContext, IMapper mapper, PostStaffDto dto)
        {
            var staff = new Staff
            {
                Name = dto.Name,
                TimeOfBirth = dto.TimeOfBirth,
                Role = dto.Role
            };
            var staffMember = mapper.Map<Staff>(dto);
            dBContext.Staffs.Add(staffMember);
            await dBContext.SaveChangesAsync();
            var responceDto = mapper.Map<PostStaffResponceDto>(staffMember);
            return TypedResults.Created($"/Staffs/{responceDto.StaffId}", responceDto);

        }

        public class PostStaffDto 
        {
            public string Name { get; set; } = string.Empty;
            public JobRoleEnum Role { get; set; }
            public DateOnly TimeOfBirth { get; set; }
        }

        public class PostStaffResponceDto
        {
            public Guid StaffId { get; set; }
            public string Name { get; set; } = string.Empty;
            public JobRoleEnum Role { get; set; }
            public DateOnly TimeofBirth { get; set; }
        }
    }

}
