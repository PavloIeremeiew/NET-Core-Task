using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Teachers;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Teachers
{
    public class CreateTeacherHandler : IRequestHandler<CreateTeacherCommand, Result<TeacherDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateTeacherHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<TeacherDTO>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var newStreetcode = _mapper.Map<Teacher>(request.TeacherCreateDTO);
            var repositoryTeachers = _repositoryWrapper.TeachersRepository;

            if (newStreetcode is null)
            {
                string errorMsg = "Teacher is not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var entity = await repositoryTeachers.CreateAsync(newStreetcode);
            bool resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<TeacherDTO>(entity));
            }
            else
            {
                string errorMsg = "Teacher is not save";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
