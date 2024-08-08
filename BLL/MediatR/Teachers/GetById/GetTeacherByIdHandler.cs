using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Teacher;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Teachers.GetById
{
    public class GetTeacherByIdHandler : IRequestHandler<GetTeacherByIdQuery, Result<TeacherDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetTeacherByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<Result<TeacherDTO>> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
