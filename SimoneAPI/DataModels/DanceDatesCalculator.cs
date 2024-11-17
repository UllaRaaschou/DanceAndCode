using SimoneAPI.DbContexts;

namespace SimoneAPI.DataModels
{
    public static class DanceDatesCalculator
    {
              
        public static List<DateOnly> CalculateDanceDates(SimoneDbContext context, CalendarDataModel calendarDataModel, TeamDataModel team)
        {
            

            var dayOfWeek = team.DayOfWeek;
            DateOnly firstDancedate = default;
            var seasonStart = calendarDataModel?.SummerHolidayEnd.AddDays(1);
            if(seasonStart != null) 
            {
                if (seasonStart.Value.DayOfWeek == dayOfWeek)
                {
                    firstDancedate = (DateOnly)seasonStart;
                }
                for (int i = 1; i < 7; i++)
                {
                    if (seasonStart.Value.AddDays(i).DayOfWeek == dayOfWeek)
                    {
                        firstDancedate = seasonStart.Value.AddDays(i);
                        break;
                    }
                }
            }
            
            List<DateOnly> danceDates = new();
            danceDates.Add(firstDancedate);
            for (int i = 1; i < 45; i++)
            {
                var nextDanceDate = firstDancedate.AddDays(7);
                foreach (var holiday in calendarDataModel.Holidays)
                {
                    if (nextDanceDate >= holiday.start || nextDanceDate < holiday.end ||

                        nextDanceDate != calendarDataModel.ChristmasShow ||
                        nextDanceDate != calendarDataModel.RecitalShow)
                    {
                        continue;
                    }
                }
                danceDates.Add(nextDanceDate);
                firstDancedate = nextDanceDate;
            }
            return danceDates;

        }
    }
}
