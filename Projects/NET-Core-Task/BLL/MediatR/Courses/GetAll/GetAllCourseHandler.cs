using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.BLL.DTO.Teachers;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Courses.GetAll
{
    public class GetAllCourseHandler : IRequestHandler<GetAllCourseQuery, Result<IEnumerable<CourseDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetAllCourseHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<CourseDTO>>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken)
        {
            var courses = await _repositoryWrapper
                .CoursesRepository
                .GetAllAsync();

            if (courses is null)
            {
                var errorMsg = "Courses are not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<CourseDTO>>(courses));
        }
    }
}
