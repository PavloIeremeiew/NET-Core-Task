using FluentValidation;

namespace NET_Core_Task.BLL.MediatR.Teachers.Delete
{
    public class DeleteTeacherRequestDTOValidator : AbstractValidator<DeleteTeacherCommand>
    {
        public DeleteTeacherRequestDTOValidator()
        {
            RuleFor(x => x.id).GreaterThan(0);
        }
    }
}
