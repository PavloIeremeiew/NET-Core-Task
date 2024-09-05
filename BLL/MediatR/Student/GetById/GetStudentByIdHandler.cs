using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.BLL.DTO.Student;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.BLL.Specification.Courses;
using NET_Core_Task.BLL.Specification.Students;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Students.GetById
{
    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, Result<StudentDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetStudentByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<StudentDTO>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _repositoryWrapper
               .StudentsRepository.GetFirstOrDefaultWithSpecAsync(new StudentByIdSpec(request.id));
            if (student is null)
            {
                var errorMsg = "Student is not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<StudentDTO>(student));
        }
    }
}
