namespace SimoneAPI.DataModels;

public class WorkingHours
{
    public Guid? WorkingHoursId { get; set; }    
    public Guid StaffId { get; set; }

    //TODO ******
    //public List<EntryItem> EntryItems { get; set; } = new List<EntryItem>();
    public Staff? Staff { get; set; }
    public DateTime Date { get; set; }

    public double Loen1 { get; set; }
    public double Loen2 { get; set; }
    public double Loen3 { get; set; }
    public double Loen4 { get; set; }
    public bool IsVikar { get; set; } = false;  
    public string Comment { get; set; } = string.Empty;
    public List<double> ListOfChosenDropDownMenuValues { get; set; } = new List<double>
    {
        0.25,
        0.5,
        0.75,
        1,
        1.25,
        1.5,
        1.75,
        2,
        2.25,
        2.5,
        2.75,
        3,
        3.25,
        3.5,
        3.75,
        4,
        4.25,
        4.5,
        4.75,
        5
    };
    //public decimal ListOfChosenDropDownMenuValues { get; set; }
}
        
