using NET_Core_Task.DAL.Persistence;

namespace NET_Core_Task.DAL.Repositories.Interfaces.Base
{
    public interface IUniversityDBContextProvider
    {
        UniversityDBContext DbContext { init; }
    }
}
