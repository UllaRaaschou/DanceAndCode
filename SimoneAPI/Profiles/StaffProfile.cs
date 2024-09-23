using AutoMapper;
using SimoneAPI.DataModels;
using SimoneAPI.Tobe.Features.StaffMembers;
//using static SimoneAPI.Tobe.Features.StaffMembers.UpdateStaff;

namespace SimoneAPI.Profiles
{
    public class StaffProfile: Profile
    {
        public StaffProfile() 
        {
            CreateMap<PostStaff.PostStaffDto, Staff>();
            CreateMap<Staff, PostStaff.PostStaffResponceDto>();
            CreateMap<Staff, GetStaffById.GetStaffResponceDto>()
                .ForMember(dest => dest.RegisteredLessonDtos, opt =>
                opt.MapFrom(rl => rl.RegisteredWorkingHours));
            CreateMap<UpdateStaff.UpdateStaffDto, Staff>();
                //.ForMember(dest => dest.RegisteredLessons, opt => opt
                //.MapFrom(usd => usd.RegisteredLessons));
                
        }
        
    }
}
