using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Student;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Students.GetAll
{
    public class GetAllStudentHandler : IRequestHandler<GetAllStudentQuery, Result<IEnumerable<StudentDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetAllStudentHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<Result<IEnumerable<StudentDTO>>> Handle(GetAllStudentQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
