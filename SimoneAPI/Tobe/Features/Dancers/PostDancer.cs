using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Entities;
using SimoneAPI.Tobe.Features.Dancers;
using System.ComponentModel.DataAnnotations;

namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class PostDancer
    {
        public static async Task<IResult> Post(SimoneDbContext dbContext, PostDancerDto dto)
        {         
            var dancer = await dbContext.DancerDataModels
                 .FirstOrDefaultAsync(d =>
                     d.Name.Contains(dto.Name) &&
                     d.TimeOfBirth == dto.TimeOfBirth);
            if (dancer != null)
            {
                return TypedResults.Problem(title: "Already registered", detail:"some detail");
            }

            var dataModel = new DancerDataModel
            {
                Name = dto.Name,
                TimeOfBirth = dto.TimeOfBirth
            };
            dbContext.DancerDataModels.Add(dataModel);
            await dbContext.SaveChangesAsync();

            var postDancerResponseDto = new PostDancerResponseDto
            {
                DancerId = dataModel.DancerId,
                Name = dataModel.Name,
                TimeOfBirth = dataModel.TimeOfBirth
            };

            return TypedResults.Created("/dancers", postDancerResponseDto);

        }

        public class PostDancerDto
        {
            public string Name { get; set; } = string.Empty;
            [DataType(DataType.Date)]
            public DateOnly TimeOfBirth { get; set; }
        }

        public class PostDancerResponseDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty ;
            public DateOnly TimeOfBirth { get; set; }
        }
    }
}
