using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.BLL.Services.Logger;
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

        public Task<Result<CourseDTO>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
