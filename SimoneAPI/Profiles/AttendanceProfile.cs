using SimoneAPI.DataModels;
using static SimoneAPI.Tobe.Features.Attendances.AddAttendance;
using AutoMapper;
using static SimoneAPI.Tobe.Features.Attendances.PutAttendance;

namespace SimoneAPI.Profiles
{
    public class AttendanceProfile: Profile
    {
        public AttendanceProfile() 
        {
            CreateMap<PostAttendanceDto, Attendance>();
            CreateMap<Attendance, PostAttendanceResponceDto>();
            CreateMap<PutAttendanceDto, Attendance>();
        }  
    }
}
