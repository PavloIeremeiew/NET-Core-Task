using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Teachers;

namespace NET_Core_Task.BLL.MediatR.Teachers
{
    public record GetAllTeachersQuery : IRequest<Result<IEnumerable<TeacherDTO>>>;
}
