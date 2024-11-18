using RestSharp;
using SimoneAPI.DataModels;
using SimoneAPI.Entities;

namespace SimoneBlazor.Domain
{
    public class RelationBlazor
    {    
        public Guid TeamId { get; set; }
        public Guid DancerId { get; set; }
        public string DancerName { get; set; } = string.Empty;
        public DateOnly DancersLastDanceDate { get; set; }
        
        public bool IsChecked { get; set; }

        public List<Attendance> Attendances { get; set; } = new List<Attendance>();

        public RelationBlazor()
        {

            var teamDanceDates = GetDanceDatesOfTheRelation().Result;

            foreach (var date in teamDanceDates)
            {
                Attendances.Add(new Attendance
                {
                    Date = date
                });
            }

        }

        public async Task<List<DateOnly>> GetDanceDatesOfTheRelation()
        {
            var options = new RestClientOptions("https://localhost:7163");
            var client = new RestClient(options);

            var request = new RestRequest($"/Teams/{TeamId}/danceDates", Method.Get);
            return await client.GetAsync<List<DateOnly>>(request, CancellationToken.None);
        }


        public Dictionary<DateOnly, AttendanceBlazor> GetADateToAttendanceDictionary()
        {
            return Attendances.ToDictionary(a => a.Date, a => new AttendanceBlazor
            {
                AttendanceId = a.AttendanceId,
                Date = a.Date,
                IsPresent = a.IsPresent,
                Note = a.Note
            });
        }



    }
}
