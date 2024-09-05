using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Courses.Update
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, Result<CourseUpdateDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public UpdateCourseHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<CourseUpdateDTO>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = _mapper.Map<Course>(request.Course);
            if (course is null)
            {
                var errorMsg = "Сourse is not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var response = _mapper.Map<CourseUpdateDTO>(course);

            _repositoryWrapper.CoursesRepository.Update(course);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(response);
            }
            else
            {
                var errorMsg = "Сourse is not updated";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
