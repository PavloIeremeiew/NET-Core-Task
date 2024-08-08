using FluentResults;
using MediatR;
using NET_Core_Task.BLL.Behavior;

namespace NET_Core_Task.BLL.MediatR.Teachers.Delete
{
    public record DeleteTeacherCommand(int id) : IValidatableRequest<Result<Unit>>;
}
