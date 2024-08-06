using NET_Core_Task.DAL.Entities;
using NET_Core_Task.DAL.Persistence;
using NET_Core_Task.DAL.Repositories.Interfaces.Courses;
using NET_Core_Task.DAL.Repositories.Realizations.Base;

namespace NET_Core_Task.DAL.Repositories.Realizations.Courses
{
    public class CoursesRepository : RepositoryBase<Course>, ICoursesRepository
    {
        public CoursesRepository(UniversityDBContext dbContext)
            : base(dbContext)
        {
        }

        public CoursesRepository()
        {
        }
    }
}
