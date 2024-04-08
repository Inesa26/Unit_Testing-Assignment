using UnitTestingApp.Model;

namespace UnitTestingApp.Repository
{
    public interface IUserRepository
    {
        User? FindById(int userId);
    }
}

