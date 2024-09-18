using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.StaffMembers
{
    public static class UpdateStaff
    {
        //public static void RegisterStaffEndpoint(this WebApplication endpointRouteBuilder)
        //{
        //    endpointRouteBuilder.MapPut("", Put);
        //}

        public static async Task<Results<NotFound, Ok>> Put(SimoneDbContext dbContext, IMapper mapper, Guid staffId, [FromBody] UpdateStaffDto updateStaffDto)
        {
            var staffMember = await dbContext.Staffs.FirstOrDefaultAsync(s => s.StaffId == staffId);

            if (staffMember == null)
            {
                return TypedResults.NotFound();
            }
            mapper.Map(updateStaffDto, staffMember);

            return TypedResults.Ok();

        }

        public class UpdateStaffDto
        {
            public Guid StaffId { get; set; }
            public string Name { get; set; }
            public JobRoleEnum Role { get; set; }
            public DateTime TimeofBirth { get; set; }
            public ICollection<RegisteredLesson> RegisteredLessons { get; set; } = new HashSet<RegisteredLesson>();

        }

        public class RegisteredLessonDto
        {
            public Guid LessonId { get; set; }
            public DateTime Date { get; set; }
            public Guid TeamId { get; set; }
            public Guid StaffId { get; set; }
            public UpdateStaffDto StaffDtos { get; set; }
        }
    }
}



//    mapper.Map(updateDancerDto, dancerDataModel);

//    if (dancerDataModel.TeamDancerRelations != null)
//    {
//        dancerDataModel.TeamDancerRelations.Clear();

//        foreach (var teamName in updateDancerDto.Teams ?? Enumerable.Empty<string>())
//        {
//            var teamDataModel = await dbContext.TeamDataModels.FirstOrDefaultAsync(t =>
//            t.Name == teamName);

//            if (teamDataModel != null)
//            {
//                dancerDataModel.TeamDancerRelations.Add(new TeamDancerRelation
//                {
//                    TeamId = teamDataModel.TeamId,
//                    DancerId = dancerDataModel.DancerId
//                });
//            }
//        }
//    }

//    await dbContext.SaveChangesAsync();

//    var responseDto = mapper.Map<UpdateDancerResponceDto>(dancerDataModel);

//    if (dancerDataModel.TeamDancerRelations != null)
//    {
//        foreach (var relation in dancerDataModel.TeamDancerRelations)
//        {
//            var teamDataModel = await dbContext.TeamDataModels.FirstOrDefaultAsync(t => t.TeamId == relation.TeamId);
//            var teamName = teamDataModel?.Name;
//            if (teamName != null)
//            {
//                responseDto.Teams.Add(teamName);
//            }
//        }
//    }

//    return TypedResults.Ok(responseDto);
//}
