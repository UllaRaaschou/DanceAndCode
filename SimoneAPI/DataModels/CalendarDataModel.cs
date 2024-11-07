namespace SimoneAPI.DataModels
{
    public class CalendarDataModel
    {
        public Guid CalendarId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateOnly SummerHolidayStart { get; set; }
        public DateOnly SummerHolidayEnd { get; set; }

        public DateOnly AutumnHolidayStart { get; set; }
        public DateOnly AutumnHolidayEnd { get; set; }

        public DateOnly ChristmasHolidayStart { get; set; }
        public DateOnly ChristmasHolidayEnd { get; set; }

        public DateOnly WintherHolidayStart { get; set; }
        public DateOnly WintherHolidayEnd { get; set; }

        public DateOnly EasterHolidayStart { get; set; }
        public DateOnly EasterHolidayEnd { get; set; }

        public DateOnly ChristmasShow { get; set; }

        public DateOnly RecitalShow { get; set; }



        public List<(DateOnly start, DateOnly end)> Holidays => new List<(DateOnly, DateOnly)>
        {
        (SummerHolidayStart, SummerHolidayEnd),
        (AutumnHolidayStart, AutumnHolidayEnd),
        (ChristmasHolidayStart, ChristmasHolidayEnd),
        (WintherHolidayStart, WintherHolidayEnd),
        (EasterHolidayStart, EasterHolidayEnd)
        };

        public List<DateOnly> Shows => new List<DateOnly>
        {
        ChristmasShow,
        RecitalShow
        };


    }
}
