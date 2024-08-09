using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Student;
using NET_Core_Task.BLL.DTO.Teachers;

namespace NET_Core_Task.BLL.MediatR.Students.GetAll
{
    public record GetAllStudentQuery : IRequest<Result<IEnumerable<StudentDTO>>>;
}
