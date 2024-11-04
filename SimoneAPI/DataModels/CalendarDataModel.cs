﻿namespace SimoneAPI.DataModels
{
    public class CalendarDataModel
    {
        public Guid CalendarId { get; set; }
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


    }
}
