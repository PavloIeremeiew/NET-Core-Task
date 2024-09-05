using FluentResults;
using NET_Core_Task.BLL.Behavior;
using NET_Core_Task.BLL.DTO.Course;

namespace NET_Core_Task.BLL.MediatR.Courses.Create
{
    public record CreateCourseCommand(CourseDTO CourseDTO) : IValidatableRequest<Result<CourseDTO>>;
}
