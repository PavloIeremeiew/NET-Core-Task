using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.BLL.DTO.Student;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.BLL.Specification.Courses;
using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Students.Create
{
    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Result<StudentDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateStudentHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<StudentDTO>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request.StudentDTO);
            var repository = _repositoryWrapper.StudentsRepository;

            if (student is null)
            {
                string errorMsg = "Student is not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            if(request.StudentDTO.CoursesIds is not null)
            {
                List<Course> courses = new List<Course>();
                foreach(int id in request.StudentDTO.CoursesIds)
                {
                    Course? course = await _repositoryWrapper
              .CoursesRepository.GetFirstOrDefaultWithSpecAsync(new CourseByIdSpec(id));
                    courses.Add(course!);
                }
                student.Courses = courses;
            }
            var entity = await repository.CreateAsync(student);
            bool resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<StudentDTO>(entity));
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
