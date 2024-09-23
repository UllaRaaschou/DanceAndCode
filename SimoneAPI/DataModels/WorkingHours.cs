namespace SimoneAPI.DataModels
{
    public class WorkingHours
    {
        public Guid WorkingHoursId { get; set; }    
        public Staff Staff { get; set; } = new Staff();
        public Guid StaffId { get; set; }
        public DateOnly Date { get; set; }
        public decimal ChosenValueOfWorkingHours { get; set; }  
        public List<decimal> ListOfWorkingHours { get; } = new List<decimal>
        {
            0.25m,
            0.5m,
            0.75m,
            1m,
            1.25m,
            1.5m,
            1.75m,
            2m,
            2.25m,
            2.25m,
            2.5m,
            2.75m,
            3m,
            3.25m,
            3.5m,
            3.75m,
            4m,
            4.25m,
            4.5m,
            4.75m,
            5m


        };
    }
            
}
