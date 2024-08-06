﻿using AutoMapper;
using NET_Core_Task.BLL.DTO.Teacher;
using NET_Core_Task.DAL.Entities;

namespace NET_Core_Task.BLL.Mapping.Teachers
{
    public class TeacherProfile : Profile
    {
        protected TeacherProfile()
        {
            CreateMap<Teacher, TeacherDTO>().ReverseMap();
        }
    }
}
