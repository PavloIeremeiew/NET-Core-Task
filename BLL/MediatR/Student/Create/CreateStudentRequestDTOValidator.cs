using FluentValidation;

namespace NET_Core_Task.BLL.MediatR.Students.Create
{
    public class CreateStudentRequestDTOValidator: AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentRequestDTOValidator()
        {
            RuleFor(x => x.StudentDTO.Age).GreaterThan(0);
            RuleFor(x => x.StudentDTO.FirstName).NotEmpty();
            RuleFor(x => x.StudentDTO.LastName).NotEmpty();
        }
    }
}
