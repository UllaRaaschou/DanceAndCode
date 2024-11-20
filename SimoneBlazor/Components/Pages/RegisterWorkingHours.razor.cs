using Microsoft.AspNetCore.Components;
using SimoneBlazor.Domain;

namespace SimoneBlazor.Components.Pages
{
    public partial class RegisterWorkingHours : ComponentBase
    {
        public TeamBlazor Team { get; set; } = new TeamBlazor();
        public bool IsVikar { get; set; } = false;


        private DateTime selectedDate = DateTime.Today;
        public string[] labelTexts = new string[] { "Løn 1", "Løn 2", "Løn 3", "Løn 4" };
        public string[] dropdownValues = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8" };
        public string[] selectedValues = new string[] { "0", "0", "0", "0" };
        private List<(DateTime Date, decimal[] Values)> savedValues = new List<(DateTime, decimal[])>();


        public void ChangeVikarStatus()
        {
            IsVikar = !IsVikar;
            StateHasChanged();
        }

        private void SaveValues()
        {
           savedValues.Add((selectedDate, (decimal[])selectedValues.Clone()));
        }
    }
}
