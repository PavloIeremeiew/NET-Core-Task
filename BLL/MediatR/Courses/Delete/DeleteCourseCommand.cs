using FluentResults;
using MediatR;
using NET_Core_Task.BLL.Behavior;

namespace NET_Core_Task.BLL.MediatR.Courses.Delete
{
    public record DeleteCourseCommand(int id) : IValidatableRequest<Result<Unit>>;
}
