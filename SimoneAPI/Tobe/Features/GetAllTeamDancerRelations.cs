using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using System.Collections.ObjectModel;
using System.Security.Cryptography.Xml;

namespace SimoneAPI.Tobe.Features
{
    public class GetAllTeamDancerRelations
    {
        public static async Task<IResult> Get(SimoneDbContext dbContext, Guid teamId)
        {
            var teamDancerRelations = await dbContext.TeamDancerRelations
                        .Include(tdr => tdr.DancerDataModel)
                        .Where(tdr => tdr.TeamId == teamId)                       
                        .ToListAsync();      
            TeamDataModel? team = await dbContext.TeamDataModels
                                .SingleOrDefaultAsync(t => t.TeamId == teamId);

            List<DateOnly> teamDanceDates = new List<DateOnly>();
            if (team != null) 
            {
               teamDanceDates = team.getDanceDates(dbContext);
            }

            List<RelationsDto> attendanceDtoList = teamDancerRelations.Select(tdr => new RelationsDto
            {
                TeamId = tdr.TeamId,
                DancerId = tdr.DancerId,
                DancerName = tdr.DancerDataModel.Name,
                DancersLastDanceDate = tdr.LastDanceDate,
                Attendances= teamDanceDates.Select(tdd => new Attendance 
                {
                    Date = tdd,
                    IsPresent = false
                }).ToList()
            }).ToList();

            return TypedResults.Ok(attendanceDtoList);
        }
    }

    public class RelationsDto
    {
        public Guid TeamId { get; set; }

        public Guid DancerId { get; set; }
        public string DancerName{ get; set; } = string.Empty;
        public DateOnly DancersLastDanceDate { get; set; }
        public List<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
