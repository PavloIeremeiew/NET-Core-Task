using FluentResults;
using MediatR;
using NET_Core_Task.DAL.Entities;
using NET_Core_Task.BLL.Behavior;
using NET_Core_Task.BLL.DTO.Teacher;

namespace NET_Core_Task.BLL.MediatR.Teachers
{
    public record CreateTeacherCommand(TeacherDTO TeacherCreateDTO) : IValidatableRequest<Result<Teacher>>;
}
