using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.BLL.Specification.Courses;
using NET_Core_Task.BLL.Specification.Students;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Students.Delete
{
    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, Result<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public DeleteStudentHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _repositoryWrapper
              .StudentsRepository.GetFirstOrDefaultWithSpecAsync(new StudentByIdSpec(request.id));

            if (student is null)
            {
                string errorMsg = "Student is not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.StudentsRepository.Delete(student);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
            if (resultIsSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = "Student is not deleted";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
