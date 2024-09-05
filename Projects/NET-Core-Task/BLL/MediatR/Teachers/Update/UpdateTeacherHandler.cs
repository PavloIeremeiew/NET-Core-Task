using AutoMapper;
using FluentResults;
using MediatR;
using NET_Core_Task.BLL.DTO.Teachers;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;

namespace NET_Core_Task.BLL.MediatR.Teachers.Update
{
    public class UpdateTeacherHandler : IRequestHandler<UpdateTeacherCommand, Result<TeacherUpdateDTO>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        public UpdateTeacherHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<TeacherUpdateDTO>> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = _mapper.Map<Teacher>(request.Teacher);
            if (teacher is null)
            {
                var errorMsg = "Teacher is not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var response = _mapper.Map<TeacherUpdateDTO>(teacher);

            _repositoryWrapper.TeachersRepository.Update(teacher);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(response);
            }
            else
            {
                var errorMsg = "Teacher is not updated";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
