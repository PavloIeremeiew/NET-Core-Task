using Ardalis.Specification;
using NET_Core_Task.DAL.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NET_Core_Task.BLL.Specification.Courses
{
    public class CourseByIdSpec : Specification<Course>
    {
        public CourseByIdSpec(int id)
        {
            Query.Where(f => f.Id == id);
        }
    }
}
