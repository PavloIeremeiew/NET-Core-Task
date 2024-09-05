using FluentValidation;

namespace NET_Core_Task.BLL.MediatR.Students.Delete
{
    public class DeleteStudentRequestDTOValidator: AbstractValidator<DeleteStudentCommand>
    {
        public DeleteStudentRequestDTOValidator()
        {
            RuleFor(x => x.id).GreaterThan(0);
        }
    }
}
