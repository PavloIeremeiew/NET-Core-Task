using FluentResults;
using NET_Core_Task.BLL.Behavior;
using NET_Core_Task.BLL.DTO.Student;
using NET_Core_Task.BLL.DTO.Teachers;

namespace NET_Core_Task.BLL.MediatR.Students.Update
{
    public record UpdateStudentCommand(StudentUpdateDTO Student) : IValidatableRequest<Result<StudentUpdateDTO>>;
}
