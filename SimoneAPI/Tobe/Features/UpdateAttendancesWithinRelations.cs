using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using Microsoft.Extensions.Logging;

namespace SimoneAPI.Tobe.Features
{
    public class UpdateAttendancesWithinRelations
    {
        public static async Task<IResult> SaveAttendances(Guid teamId, SimoneDbContext dbContext, [FromBody] List<TeamDancerRelation> relations, ILogger<UpdateAttendancesWithinRelations> logger)
        {
            // Hent eksisterende relationer fra databasen
            var teamDancerRelationsOnThisTeamFromDatabase = await dbContext.TeamDancerRelations
                .Include(tdr => tdr.Attendances)
                .Include(tdr => tdr.DancerDataModel)
                .Where(tdr => tdr.TeamId == teamId)
                .ToListAsync();
            if (!teamDancerRelationsOnThisTeamFromDatabase.Any()) 
            {
                return TypedResults.NotFound();
            }

                if (teamDancerRelationsOnThisTeamFromDatabase.Any())
            {
                foreach (var relationFromDatabase in teamDancerRelationsOnThisTeamFromDatabase)
                {
                    // Find den opdaterede relation fra inputlisten
                    var updatedRelation = relations.FirstOrDefault(r => r.TeamId == relationFromDatabase.TeamId && r.DancerId == relationFromDatabase.DancerId);

                    if (updatedRelation != null) // Tilføjet null-check for updatedRelation
                    {
                        //var attendanceListFromDatabase = relationFromDatabase.Attendances;
                        var updatedAttendanceList = updatedRelation.Attendances;

                        if (relationFromDatabase.Attendances.Any())
                        {
                            foreach (var att in relationFromDatabase.Attendances)
                            {
                                // Find den opdaterede attendance fra inputlisten
                                var updatedAttendance = updatedAttendanceList?.FirstOrDefault(ua => ua.AttendanceId == att.AttendanceId);
                                if (updatedAttendance != null) // Tilføjet null-check for updatedAttendance
                                {
                                    // Logning for at bekræfte, at koden udføres
                                    logger.LogInformation($"Opdaterer attendance med ID: {att.AttendanceId}"); 

                                    // Opdater attendance-egenskaber
                                    att.Date = updatedAttendance.Date;
                                    att.IsPresent = updatedAttendance.IsPresent;
                                    att.Note = updatedAttendance.Note;
                                }
                                else 
                                {
                                    // Logning for at bekræfte, at der ikke blev fundet en opdateret attendance
                                     logger.LogInformation($"Ingen opdateret attendance fundet for ID: {att.AttendanceId}");
                                }
                            }
                        }
                        else
                        {
                            // Initialiser attendanceListFromDatabase hvis den er null
                            //attendanceListFromDatabase = new List<Attendance>();
                            foreach (var att in updatedAttendanceList)
                            {
                                relationFromDatabase.Attendances.Add(new Attendance()
                                {
                                    //AttendanceId = Guid.NewGuid(),
                                    Date = att.Date,
                                    IsPresent = att.IsPresent,
                                    Note = att.Note,
                                    TeamId = relationFromDatabase.TeamId,
                                    DancerId = relationFromDatabase.DancerId
                                });
                            }
                            //relationFromDatabase.Attendances = attendanceListFromDatabase; // Tilføjet for at sikre, at relationen opdateres korrekt
                        }
                    }
                }
            }

            // Opdater relationer i databasen
            //dbContext.TeamDancerRelations.UpdateRange(teamDancerRelationsOnThisTeamFromDatabase); 

            // Gem ændringer i databasen
            await dbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }


        public class RelationBlazorDto
        {
            public Guid TeamId { get; set; }
            public Guid DancerId { get; set; }
            public DateOnly LastDanceDate { get; set; }
            //public bool IsChecked { get; set; } = false;
            public string Note { get; set; } = string.Empty;
            public List<Attendance> Attendances { get; set; } = new List<Attendance>();
        }
    }
}
