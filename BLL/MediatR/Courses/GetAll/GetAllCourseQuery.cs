using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Course;

namespace NET_Core_Task.BLL.MediatR.Courses.GetAll
{
    public record GetAllCourseQuery : IRequest<Result<IEnumerable<CourseDTO>>>;
}
