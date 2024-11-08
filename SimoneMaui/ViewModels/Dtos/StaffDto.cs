namespace SimoneMaui.ViewModels.Dtos
{
    public class StaffDto
    {
        public string Name { get; set; } = string.Empty;
        public MauiJobRoleEnum Role { get; set; }
        public DateOnly TimeOfBirth { get; set; }
    }
}
