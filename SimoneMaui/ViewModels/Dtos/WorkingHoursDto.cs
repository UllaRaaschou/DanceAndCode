using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimoneMaui.ViewModels.Dtos
{
    public class WorkingHoursDto
    {
        public Guid? WorkingHoursId { get; set; }
        public Guid StaffId { get; set; }
        public StaffDto? Staff { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public double Loen1 { get; set; }
        public double Loen2 { get; set; }
        public double Loen3 { get; set; }
        public double Loen4 { get; set; }
        public bool IsVikar { get; set; } = false;
        public string Comment { get; set; } = string.Empty;
    }
}
