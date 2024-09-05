using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.BLL.Specification.Courses;
using NET_Core_Task.BLL.Specification.Teahers;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Courses.Delete
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public DeleteCourseHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _repositoryWrapper
               .CoursesRepository.GetFirstOrDefaultWithSpecAsync(new CourseByIdSpec(request.id));

            if (course is null)
            {
                string errorMsg = "Course is not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.CoursesRepository.Delete(course);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
            if (resultIsSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = "Course is not deleted";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
