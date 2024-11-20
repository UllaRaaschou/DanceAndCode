using Microsoft.AspNetCore.Components;
using SimoneBlazor.Domain;
using System.Runtime.CompilerServices;

namespace SimoneBlazor.Components.Pages
{
    public partial class RegisterWorkingHours : ComponentBase
    {
        public TeamBlazor Team { get; set; } = new TeamBlazor();
        public bool IsVikar { get; set; } = false;

        private DateTime selectedDate = DateTime.Today;
        public string[] labelTexts = new string[] { "Løn 1", "Løn 2", "Løn 3", "Løn 4" };
        public string[] dropdownValues;
        public string[] selectedValues = new string[] { "0", "0", "0", "0" };
        private List<(DateTime Date, decimal[] Values)> savedValues = new List<(DateTime, decimal[])>();

        protected override void OnInitialized()
        {
            dropdownValues = GetDropDownValues();
        }

        public void ChangeVikarStatus()
        {            
            IsVikar = !IsVikar;
            StateHasChanged();
        }

        private void SaveValues()
        {
            savedValues.Add((selectedDate, (decimal[])selectedValues.Clone()));
        }

        public string[] GetDropDownValues()
        {
            var calculatedValues = new List<string>();
            for (decimal i = 0.25m; i <= 5; i += 0.25m)
            {
                calculatedValues.Add(i.ToString());
            }
            return calculatedValues.ToArray();
        }
    }
}
