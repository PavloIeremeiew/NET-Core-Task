using AutoMapper;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.DAL.Entities;

namespace NET_Core_Task.BLL.Mapping.Courses
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Course, CourseUpdateDTO>().ReverseMap();
        }
    }
}
