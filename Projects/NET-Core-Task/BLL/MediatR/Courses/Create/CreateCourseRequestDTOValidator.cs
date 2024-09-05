using FluentValidation;

namespace NET_Core_Task.BLL.MediatR.Courses.Create
{
    public class CreateCourseRequestDTOValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseRequestDTOValidator()
        {
            RuleFor(x => x.CourseDTO.Title).NotEmpty();
            RuleFor(x => x.CourseDTO.TeacherId).NotEmpty().GreaterThan(0);
        }
    }
}
