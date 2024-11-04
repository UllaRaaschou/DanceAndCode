namespace SimoneAPI.DataModels
{
    public class DanceDatesCalculator
    {
        private readonly CalendarDataModel _calendarDataModel;
        public DanceDatesCalculator(CalendarDataModel calendarDataModel)
        {
            _calendarDataModel = calendarDataModel;
        }

        public List<DateOnly> CalculateDanceDates(TeamDataModel team) 
        {
            var DayOfWeek = team.DayOfWeek;
        }
    }
}
