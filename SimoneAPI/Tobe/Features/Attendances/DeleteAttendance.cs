using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Attendances
{
    public class DeleteAttendance
    {
        public void RegisterAttendanceEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapDelete("/TeamDancerRelations/Attendances", Delete);
        }

        public static async Task<IResult> Delete(SimoneDbContext dBContext, Guid AttendanceId)
        {
            var attendance = await dBContext.Attendances.FirstOrDefaultAsync(a => a.AttendanceId == AttendanceId);
            if (attendance == null)
            {
                return TypedResults.NotFound();
            }
            dBContext.Attendances.Remove(attendance);
            await dBContext.SaveChangesAsync();
            return TypedResults.NoContent();
        }
    }
}
