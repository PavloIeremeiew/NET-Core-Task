using NET_Core_Task.DAL.Repositories.Interfaces.Courses;
using NET_Core_Task.DAL.Repositories.Interfaces.Students;
using NET_Core_Task.DAL.Repositories.Interfaces.Teachers;
using System.Transactions;

namespace NET_Core_Task.DAL.Repositories.Interfaces.Base
{
    public interface IRepositoryWrapper
    {
        ICoursesRepository CoursesRepository { get; }
        IStudentsRepository StudentsRepository { get; }
        ITeachersRepository TeachersRepository { get; }
    }
}
