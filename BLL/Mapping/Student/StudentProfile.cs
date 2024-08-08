using AutoMapper;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.BLL.DTO.Student;
using NET_Core_Task.DAL.Entities;

namespace NET_Core_Task.BLL.Mapping.Students
{
    public class StudentProfile: Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Student, StudentUpdateDTO>().ReverseMap();
        }
    }
}
