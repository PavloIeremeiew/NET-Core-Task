using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.BLL.Services.Logger;
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

        public Task<Result<CourseUpdateDTO>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
