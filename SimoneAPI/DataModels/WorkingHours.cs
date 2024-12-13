namespace SimoneAPI.DataModels;

public class WorkingHours
{
    public Guid? WorkingHoursId { get; set; }    
    public Guid StaffId { get; set; }  
    public Staff? Staff { get; set; }
    public DateTime Date { get; set; }

    public double Loen1 { get; set; }
    public double Loen2 { get; set; }
    public double Loen3 { get; set; }
    public double Loen4 { get; set; }


    public bool IsVikar { get; set; } = false;  
    public string Comment { get; set; } = string.Empty;
   
}
        
