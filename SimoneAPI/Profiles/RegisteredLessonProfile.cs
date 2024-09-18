using AutoMapper;
using SimoneAPI.DataModels;
using SimoneAPI.Tobe.Features.StaffMembers;

namespace SimoneAPI.Profiles
{
    public class RegisteredLessonProfile:Profile
    {
        public RegisteredLessonProfile() 
        {
            //CreateMap<UpdateStaff.RegisteredLessonDto, RegisteredLesson>();
            CreateMap<RegisteredLesson, GetStaffById.RegisteredLessonDto>();
        }
    }
}
