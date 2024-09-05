using FluentValidation;

namespace NET_Core_Task.BLL.MediatR.Students.Update
{
    public class UpdateStudentRequestDTOValidator: AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentRequestDTOValidator()
        {
            RuleFor(x => x.Student.Age).GreaterThan(0);
            RuleFor(x => x.Student.FirstName).NotEmpty();
            RuleFor(x => x.Student.LastName).NotEmpty();
        }
    }
}
