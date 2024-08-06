using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Persistence;
using NET_Core_Task.DAL.Repositories.Interfaces.Teachers;
using NET_Core_Task.DAL.Repositories.Realizations.Base;

namespace NET_Core_Task.DAL.Repositories.Realizations.Teachers
{
    public class TeachersRepository : RepositoryBase<Teacher>, ITeachersRepository
    {
        public TeachersRepository(UniversityDBContext dbContext)
            : base(dbContext)
        {
        }

        public TeachersRepository()
        {
        }
    }
}
