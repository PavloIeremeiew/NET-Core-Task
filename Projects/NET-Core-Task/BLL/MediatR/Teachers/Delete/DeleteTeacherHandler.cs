using FluentResults;
using MediatR;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.BLL.Specification.Teahers;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Teachers.Delete
{
    public class DeleteTeacherHandler : IRequestHandler<DeleteTeacherCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;
        public DeleteTeacherHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _repositoryWrapper
              .TeachersRepository.GetFirstOrDefaultWithSpecAsync(new TeacherByIdSpec(request.id));

            if (teacher is null)
            {
                string errorMsg = "Teacher is not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }
            if(teacher.Courses is not null)
            {
                foreach(var courses in teacher.Courses)
                {
                    _repositoryWrapper.CoursesRepository.Delete(courses);
                }
                
            }
            _repositoryWrapper.TeachersRepository.Delete(teacher);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
            if (resultIsSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = "Teacher is not deleted";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
