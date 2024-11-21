using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.WorkRegistration
{
    public static class RegisterWorkingHours
    {
        public static async Task<IResult> Register(SimoneDbContext dbContext,[FromBody] WorkingHours workingHoursForRegistration)
        {
            var staff = await dbContext.Staffs.FirstOrDefaultAsync(s => s.StaffId == workingHoursForRegistration.StaffId);
            if (staff == null)
            {
                return TypedResults.NotFound();
            }

            var workingHoursForDatabase = new WorkingHours
            {
                StaffId = workingHoursForRegistration.StaffId,
                Date = workingHoursForRegistration.Date,
                Loen1 = workingHoursForRegistration.Loen1,
                Loen2 = workingHoursForRegistration.Loen2,
                Loen3 = workingHoursForRegistration.Loen3,
                Loen4 = workingHoursForRegistration.Loen4,

                IsVikar = workingHoursForRegistration.IsVikar,
                Comment = workingHoursForRegistration.Comment
            };

            dbContext.WorkingHours.Add(workingHoursForDatabase);

            var savedRegistration = await dbContext.SaveChangesAsync();
            return TypedResults.Ok(savedRegistration);
        }
    }
}

