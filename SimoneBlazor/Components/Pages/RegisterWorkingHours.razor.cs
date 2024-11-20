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

        private DateTime selectedDate = DateTime.Today;
        public string[] selectedValues = new string[] { "0", "0", "0", "0" };
        public bool IsVikar { get; set; } = false;
        public string Comment { get; set; } = string.Empty;

        private List<(DateTime Date, decimal[] Values, bool IsVikar, string Comment)> savedValues = new List<(DateTime, decimal[], bool isVikar, string comment)>();
        public string UserMessage { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            dropdownValues = GetDropDownValues();
        }

        public void ChangeVikarStatus()
        {
            IsVikar = !IsVikar;
            StateHasChanged();
        }

        private async Task SaveValues()
        {
            //    var workingHours = new WorkingHours
            //    {
            //        StaffId = Guid.NewGuid(),
            //        Date = selectedDate,
            //        ChosenValueOfWorkingHours = decimal.Parse(selectedValues[0]),
            //        IsVikar = IsVikar,
            //        Comment = Comment
            //    };

            //    var options = new RestClientOptions("https://localhost:7163");
            //    var client = new RestClient(options);

            //    var request = new RestRequest("/workinghours", Method.Post);
            //    request.AddJsonBody(workingHours);

            //    try
            //    {
            //        var response = await client.ExecuteAsync(request);

            //        if (response.IsSuccessful)
            //        {
            //            savedValues.Add((selectedDate, (decimal[])selectedValues.Clone(), IsVikar, Comment));
            //            UserMessage = "Values saved successfully!";
            //        }
            //        else
            //        {
            //            UserMessage = $"Error: {response.StatusCode} - {response.ErrorMessage}";
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        UserMessage = $"Exception: {ex.Message}";
            //    }

            //    StateHasChanged();
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
