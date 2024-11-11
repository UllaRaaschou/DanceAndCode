using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.StaffMembers
{
    public class DeleteStaff
    {
        public static async Task<IResult> Delete(SimoneDbContext dbContext, Guid staffId)
        {
            var staff = await dbContext.Staffs.FirstOrDefaultAsync(s => s.StaffId == staffId);
            if (staff == null)
            {
                return TypedResults.NotFound();
            }

            dbContext.Staffs.Remove(staff);
            await dbContext.SaveChangesAsync();
            return TypedResults.NoContent();

        }
    }
}
