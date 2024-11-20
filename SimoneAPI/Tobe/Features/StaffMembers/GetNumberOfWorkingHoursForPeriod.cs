using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.StaffMembers
{
    public static class GetNumberOfWorkingHoursForPeriod
    {
        //public static async Task<IResult> Get(SimoneDbContext context, Guid staffId, DateOnly firstDayOfPeriod, 
        //    DateOnly lastDayInPeriod )
        //{
        //    var staff = await context.Staffs.FirstOrDefaultAsync(s => s.StaffId == staffId);
        //    if (staff == null)
        //    {
        //        return TypedResults.NotFound();
        //    }

        //    decimal numberOfWorkingHours = 0;
        //    for (var date = firstDayOfPeriod; date <= lastDayInPeriod; date = date.AddDays(1))
        //    {
           
        //        WorkingHours? chosenValueOfWorking = await context.WorkingHours.FirstOrDefaultAsync(wh 
        //            => wh.StaffId == staffId && wh.Date == date);
        //        if (chosenValueOfWorking != null)
        //        {
        //            numberOfWorkingHours += chosenValueOfWorking.ChosenValueOfWorkingHours;
        //        }
        //    }

        //    return TypedResults.Ok(numberOfWorkingHours);
            
        //}
    }
}