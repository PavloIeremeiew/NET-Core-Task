using FluentValidation;

namespace NET_Core_Task.BLL.MediatR.Courses.Update
{
    public class UpdateCourseRequestDTOValidator: AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseRequestDTOValidator()
        {
            RuleFor(x => x.Course.Title).NotEmpty();
            RuleFor(x => x.Course.TeacherId).NotEmpty().GreaterThan(0);
        }
    }
}
