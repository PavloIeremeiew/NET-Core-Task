using FluentValidation;
using NET_Core_Task.BLL.DTO.Teacher;

namespace NET_Core_Task.BLL.MediatR.Teachers.Update
{
    public class UpdateTeacherRequestDTOValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherRequestDTOValidator()
        {
            RuleFor(x => x.Teacher.Age).GreaterThan(18);
            RuleFor(x => x.Teacher.FirstName).NotEmpty();
            RuleFor(x => x.Teacher.LastName).NotEmpty();
        }
    }
}
