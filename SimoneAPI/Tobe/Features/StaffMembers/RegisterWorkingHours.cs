using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.StaffMembers
{ }
    //public static class RegisterWorkingHours { }
    //{
        //public static void RegisterStaffEndpoint(this WebApplication endpointRouteBuilder)
        //{
        //    endpointRouteBuilder.MapPut("/{staffId:guid}/{workedHours:decimal}", Register);
        //}

//        public static async Task<IResult> Register(SimoneDbContext dbContext, 
            
//           Guid staffId, decimal workedHours, DateOnly? date = null )
//        {
//            var staff = await dbContext.Staffs.FirstOrDefaultAsync(s => s.StaffId == staffId);
//            if (staff == null)
//            {
//                return TypedResults.NotFound();
//            }

//            date??= DateOnly.FromDateTime(DateTime.Now);

//            staff.RegisteredWorkingHours.Add(new WorkingHours
//            {
//                StaffId = staffId,
//                ChosenValueOfWorkingHours = workedHours,
//                Date = date.Value

//            });

//            await dbContext.SaveChangesAsync();
//            return TypedResults.Ok();
//        }



//    }
//}
