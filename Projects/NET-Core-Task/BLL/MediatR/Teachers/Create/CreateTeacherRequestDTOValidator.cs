using FluentValidation;

namespace NET_Core_Task.BLL.MediatR.Teachers
{
    public class CreateTeacherRequestDTOValidator : AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherRequestDTOValidator()
        {
            RuleFor(x => x.TeacherCreateDTO.Age).NotEmpty().Must(a => a > 18);
            RuleFor(x => x.TeacherCreateDTO.FirstName).NotEmpty();
            RuleFor(x => x.TeacherCreateDTO.LastName).NotEmpty();
        }
    }
}
