using Ardalis.Specification;
using NET_Core_Task.DAL.Entities;

namespace NET_Core_Task.BLL.Specification.Teahers
{
    public class TeacherByIdSpec : Specification<Teacher>
    {
        public TeacherByIdSpec(int id)
        {
            Query.Where(f => f.Id == id);
        }
    }
}
