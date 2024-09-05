using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.BLL.DTO.Teachers;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.BLL.Specification.Courses;
using NET_Core_Task.BLL.Specification.Teahers;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Courses.GetById
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, Result<CourseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetCourseByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<CourseDTO>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _repositoryWrapper
               .CoursesRepository.GetFirstOrDefaultWithSpecAsync(new CourseByIdSpec(request.id));
            if (course is null)
            {
                var errorMsg = "Course is not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<CourseDTO>(course));
        }
    }
}
