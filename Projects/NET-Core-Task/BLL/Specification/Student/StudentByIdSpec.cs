using Ardalis.Specification;
using NET_Core_Task.DAL.Entities;

namespace NET_Core_Task.BLL.Specification.Students
{
    public class StudentByIdSpec : Specification<Student>
    {
        public StudentByIdSpec(int id)
        {
            Query.Where(f => f.Id == id);
        }
    }
}
