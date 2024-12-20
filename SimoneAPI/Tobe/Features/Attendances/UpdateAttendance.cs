﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Entities;

namespace SimoneAPI.Tobe.Features.Attendances
{
    public class UpdateAttendance
    {
        
        public static async Task<IResult> SaveAttendances(Guid teamId, SimoneDbContext dbContext, [FromBody] List<RelationBlazorDto> relations)
        {
            foreach (var relationBlazor in relations)
            {
                var teamDancerRelation = await dbContext.TeamDancerRelations
                    .Include(tdr => tdr.Attendances)
                    .FirstOrDefaultAsync(tdr => tdr.TeamId == teamId && tdr.DancerId == relationBlazor.DancerId);

                if (teamDancerRelation != null)
                {
                    foreach (var attendanceBlazor in relationBlazor.Attendances)
                    {
                        var existingAttendance = teamDancerRelation.Attendances
                            .FirstOrDefault(a => a.AttendanceId == attendanceBlazor.AttendanceId);

                        if (existingAttendance != null)
                        {
                            // Update existing attendance
                            existingAttendance.Date = attendanceBlazor.Date;
                            existingAttendance.IsPresent = attendanceBlazor.IsPresent;
                            existingAttendance.Note = attendanceBlazor.Note;
                        }
                        else
                        {
                            // Add new attendance
                            teamDancerRelation.Attendances.Add(new Attendance
                            {
                                AttendanceId = attendanceBlazor.AttendanceId,
                                Date = attendanceBlazor.Date,
                                IsPresent = attendanceBlazor.IsPresent,
                                Note = attendanceBlazor.Note
                            });
                        }
                    }
                    dbContext.TeamDancerRelations.Update(teamDancerRelation);
                }
            }
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
}
