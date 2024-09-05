using FluentValidation;

namespace NET_Core_Task.BLL.MediatR.Courses.Delete
{
    public class DeleteCourseRequestDTOValidator: AbstractValidator<DeleteCourseCommand>
    {
        public DeleteCourseRequestDTOValidator()
        {
            RuleFor(x => x.id).GreaterThan(0);
        }
    }
}
