using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Student;
using NET_Core_Task.BLL.DTO.Teachers;

namespace NET_Core_Task.BLL.MediatR.Students.GetById
{
    public record GetStudentByIdQuery(int id) : IRequest<Result<StudentDTO>>;
}
