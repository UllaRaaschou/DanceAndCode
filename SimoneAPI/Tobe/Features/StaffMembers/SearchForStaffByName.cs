using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using static SimoneAPI.Tobe.Features.Dancer.SearchForDancerByName;

namespace SimoneAPI.Tobe.Features.StaffMembers
{
    public class SearchForStaffByName
    {
        public static async Task<IResult> Get(SimoneDbContext dbContext,
            [FromQuery] string name)
        {
            IEnumerable<Staff> models = (IEnumerable<Staff>)await dbContext.Staffs
                 .Where(d => d.Name.Contains(name)).ToListAsync();

            if (!models.Any())
            {
                return TypedResults.NotFound();
            }

            var responce = models.Select(x => new SearchStaffResponceDto
            {
                StaffId = x.StaffId,
                Name = x.Name,
                TimeOfBirth = x.TimeOfBirth                
            }).ToList();
            
            return TypedResults.Ok(responce);
        }
    }

    public class SearchStaffResponceDto
    {
        public Guid StaffId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly TimeOfBirth { get; set; }

    }
}
