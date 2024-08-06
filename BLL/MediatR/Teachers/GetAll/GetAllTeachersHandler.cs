using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Teacher;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Teachers
{
    public class GetAllTeachersHandler : IRequestHandler<GetAllTeachersQuery, Result<IEnumerable<TeacherDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetAllTeachersHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<TeacherDTO>>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _repositoryWrapper
                .TeachersRepository
                .GetAllAsync();

            if (teachers is null)
            {
                var errorMsg = "Teachers are not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<TeacherDTO>>(teachers));
        }
    }
}
