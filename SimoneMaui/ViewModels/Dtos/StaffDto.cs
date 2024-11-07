using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimoneMaui.ViewModels.Dtos
{
    public class StaffDto
    {
        public string Name { get; set; } = string.Empty;
        public MauiJobRoleEnum Role { get; set; }
        public DateOnly TimeOfBirth { get; set; }
    }
}
