using Ardalis.Specification;
using NET_Core_Task.DAL.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NET_Core_Task.BLL.Specification.Course
{
    public class CourseByIdSpec : Specification<Student>
    {
        public CourseByIdSpec(int id)
        {
            Query.Where(f => f.Id == id);
        }
    }
}
