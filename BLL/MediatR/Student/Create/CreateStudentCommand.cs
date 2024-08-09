using FluentResults;
using NET_Core_Task.BLL.Behavior;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.BLL.DTO.Student;

namespace NET_Core_Task.BLL.MediatR.Students.Create
{
    public record CreateStudentCommand(StudentDTO StudentDTO) : IValidatableRequest<Result<StudentDTO>>;
}
