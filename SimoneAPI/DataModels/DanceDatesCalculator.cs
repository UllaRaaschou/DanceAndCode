using Microsoft.IdentityModel.Tokens;
using SimoneAPI.DbContexts;

namespace SimoneAPI.DataModels
{
    public static class DanceDatesCalculator
    {
              
        public static List<DateOnly> CalculateDanceDates(SimoneDbContext context, CalendarDataModel calendarDataModel, TeamDataModel team)
        {
            var seasonStart = calendarDataModel.SummerHolidayEnd.AddDays(1);
            
            for (int i = 1; i < 7; i++)
            {
                if (seasonStart.AddDays(i).DayOfWeek == team.DayOfWeek)
                {
                    seasonStart = seasonStart.AddDays(i);
                    break;
                }
            }
            
            DateOnly firstDancedate = seasonStart;
            List<DateOnly> danceDates = new() { firstDancedate };
            
            for (int i = 1; i < 45; i++)
            {
                var nextDanceDate = firstDancedate.AddDays(7);
                if (nextDanceDate == calendarDataModel.ChristmasShow) continue;
                if (nextDanceDate == calendarDataModel.RecitalShow) continue;

                var inHolidayPeriod = calendarDataModel.Holidays
                    .Where(holiday => nextDanceDate >= holiday.start && nextDanceDate < holiday.end)
                    .Any();

                if (!inHolidayPeriod) { 
                    danceDates.Add(nextDanceDate);
                }

                firstDancedate = nextDanceDate;
            }

            return danceDates;

        }
    }
}
