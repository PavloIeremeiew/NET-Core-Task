using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.BLL.DTO.Teachers;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.BLL.Specification.Teahers;
using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Courses.Create
{
    public class CreateCourseHandler:IRequestHandler<CreateCourseCommand, Result<CourseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateCourseHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<CourseDTO>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = _mapper.Map<Course>(request.CourseDTO);
            var repository = _repositoryWrapper.CoursesRepository;

            if (course is null)
            {
                string errorMsg = "Course is not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            Teacher? teacher = await _repositoryWrapper
              .TeachersRepository.GetFirstOrDefaultWithSpecAsync(new TeacherByIdSpec(request.CourseDTO.TeacherId));
            course.Teacher = teacher;

            var entity = await repository.CreateAsync(course);
            bool resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<CourseDTO>(entity));
            }
            else
            {
                string errorMsg = "Course is not save";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
