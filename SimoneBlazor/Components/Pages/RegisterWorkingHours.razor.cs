using Microsoft.AspNetCore.Components;
using RestSharp;
using SimoneAPI.DataModels;
using SimoneAPI.Entities;
using SimoneBlazor.Domain;
using System.Runtime.CompilerServices;

namespace SimoneBlazor.Components.Pages
{
    public partial class RegisterWorkingHours : ComponentBase
    {
        public string[] labelTexts = new string[] { "Løn 1", "Løn 2", "Løn 3", "Løn 4" };
        public string[] dropdownValues;
        public List<EntryItem> entryItems = new List<EntryItem>();

        private DateTime selectedDate = DateTime.Today;
        public string[] selectedValues = new string[] { "0", "0", "0", "0" };

        public EntryItem Loen1 { get; set; } = new EntryItem();
        public EntryItem Loen2 { get; set; } = new EntryItem();
        public EntryItem Loen3 { get; set; } = new EntryItem();
        public EntryItem Loen4 { get; set; } = new EntryItem();

        public bool IsVikar { get; set; } = false;
        public string Comment { get; set; } = string.Empty;

        private List<(DateTime Date, decimal[] Values, bool IsVikar, string Comment)> savedValues = new List<(DateTime, decimal[], bool isVikar, string comment)>();
        public string UserMessage { get; set; } = string.Empty;

        public Staff mockStaff { get; set; }

        protected override void OnInitialized()
        {
            dropdownValues = GetDropDownValues();
            for (int i = 0; i < labelTexts.Length; i++)
            {
                entryItems.Add(new EntryItem { LabelText = labelTexts[i], SelectedValue = "0" });
            }
        }

        public void ChangeVikarStatus()
        {
            IsVikar = !IsVikar;
            StateHasChanged();
        }

        private async Task SaveValues()
        {
            var workingHoursToBeRegistered = new WorkingHours
            {
                StaffId = Guid.Parse("D7A499EB-65D8-4A62-BDD2-91C65E45E89C"),
                Date = selectedDate,
                Loen1 = double.TryParse(Loen1.SelectedValue, out var l1) ? l1 : 0,
                Loen2 = double.TryParse(Loen2.SelectedValue, out var l2) ? l2 : 0,
                Loen3 = double.TryParse(Loen3.SelectedValue, out var l3) ? l3 : 0,
                Loen4 = double.TryParse(Loen4.SelectedValue, out var l4) ? l4 : 0,
                IsVikar = IsVikar,
                Comment = Comment
            };

            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);

            var request = new RestRequest($"/WorkingHours", Method.Post);
            request.AddJsonBody(workingHoursToBeRegistered);

            try
            {
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    savedValues.Add((selectedDate, entryItems.Select(e => decimal.Parse(e.SelectedValue)).ToArray(), IsVikar, Comment));
                    UserMessage = "Values saved successfully!";
                }
                else
                {
                    UserMessage = $"Error: {response.StatusCode} - {response.ErrorMessage}";
                }
            }
            catch (Exception ex)
            {
                UserMessage = $"Exception: {ex.Message}";
            }

            StateHasChanged();
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
