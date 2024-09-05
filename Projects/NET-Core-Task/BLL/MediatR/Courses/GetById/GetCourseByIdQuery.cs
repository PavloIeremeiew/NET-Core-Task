using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Course;

namespace NET_Core_Task.BLL.MediatR.Courses.GetById
{
    public record GetCourseByIdQuery(int id) : IRequest<Result<CourseDTO>>;
}
