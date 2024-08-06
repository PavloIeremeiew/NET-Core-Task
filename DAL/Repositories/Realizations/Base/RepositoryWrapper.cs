using NET_Core_Task.DAL.Persistence;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;
using NET_Core_Task.DAL.Repositories.Interfaces.Courses;
using NET_Core_Task.DAL.Repositories.Interfaces.Students;
using NET_Core_Task.DAL.Repositories.Interfaces.Teachers;
using NET_Core_Task.DAL.Repositories.Realizations.Courses;
using NET_Core_Task.DAL.Repositories.Realizations.Students;
using NET_Core_Task.DAL.Repositories.Realizations.Teachers;
using System.Transactions;

namespace NET_Core_Task.DAL.Repositories.Realizations.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly UniversityDBContext _universityDBContext;

        private ICoursesRepository? _coursesRepository = null;
        private IStudentsRepository? _studentsRepository = null;
        private ITeachersRepository? _teachersRepository = null;

        public RepositoryWrapper(UniversityDBContext universityDBContext)
        {
            _universityDBContext = universityDBContext;
        }
            
        public ICoursesRepository CoursesRepository =>
          GetRepository(_coursesRepository as CoursesRepository);

        public IStudentsRepository StudentsRepository =>
          GetRepository(_studentsRepository as StudentsRepository);

        public ITeachersRepository TeachersRepository =>
          GetRepository(_teachersRepository as TeachersRepository);

        public T GetRepository<T>(T? repo)
     where T : IUniversityDBContextProvider, new()
        {
            if (repo is null)
            {
                repo = new T()
                {
                    DbContext = _universityDBContext
                };
            }

            return repo;
        }

        public int SaveChanges()
        {
            return _universityDBContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _universityDBContext.SaveChangesAsync();
        }
    }
}
