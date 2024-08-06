using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Persistence;
using NET_Core_Task.DAL.Repositories.Interfaces.Students;
using NET_Core_Task.DAL.Repositories.Realizations.Base;

namespace NET_Core_Task.DAL.Repositories.Realizations.Students
{
    public class StudentsRepository : RepositoryBase<Student>, IStudentsRepository
    {
        public StudentsRepository(UniversityDBContext dbContext)
            : base(dbContext)
        {
        }

        public StudentsRepository()
        {
        }
    }
}
