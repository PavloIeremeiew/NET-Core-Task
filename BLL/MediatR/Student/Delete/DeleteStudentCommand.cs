using FluentResults;
using MediatR;
using NET_Core_Task.BLL.Behavior;

namespace NET_Core_Task.BLL.MediatR.Students.Delete
{
    public record DeleteStudentCommand(int id) : IValidatableRequest<Result<Unit>>;
}
