using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimoneAPI.Tobe.Features.WorkRegistration
{
    public class GetWorkingHours
    {
        public static async Task<IResult> Get(SimoneDbContext dBContext, Guid staffId)
        {
            DateTime periodStartDate;
            var todayDate = DateTime.Today;

            if (todayDate.Day >= 25)
            {
                periodStartDate = new DateTime(todayDate.Year, todayDate.Month, 25);
            }
            else
            {
                var periodStartMonth = DateTime.Today.Month - 1;
                periodStartDate = new DateTime(DateTime.Today.Year, periodStartMonth, 25);
            }

            var listOfWorkingHoursInPeriod = await dBContext.WorkingHours
                .OrderByDescending(wh => wh.Date)
                .Where(wh => wh.Date >= periodStartDate)
                .Where(wh => wh.StaffId == staffId)
                .ToListAsync();

            if (listOfWorkingHoursInPeriod == null || !listOfWorkingHoursInPeriod.Any())
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(listOfWorkingHoursInPeriod);
        }
    }
}
    
