using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Teacher;

namespace NET_Core_Task.BLL.MediatR.Teachers.GetById
{
    public record GetTeacherByIdQuery(int id) : IRequest<Result<TeacherDTO>>;
}
