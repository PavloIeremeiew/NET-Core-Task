using FluentResults;
using NET_Core_Task.BLL.Behavior;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.BLL.DTO.Teachers;

namespace NET_Core_Task.BLL.MediatR.Courses.Update
{
    public record UpdateCourseCommand(CourseUpdateDTO Course) : IValidatableRequest<Result<CourseUpdateDTO>>;
}
