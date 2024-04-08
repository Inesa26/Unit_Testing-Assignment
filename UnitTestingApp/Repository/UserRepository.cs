using UnitTestingApp.Model;

namespace UnitTestingApp.Repository
{

    public class UserRepository : IUserRepository
    {
        private List<User> userList;

        public UserRepository()
        {
            userList = new List<User>
            {
                new User(1, "John", Status.ACTIVE),
                new User(2, "Maria", Status.ACTIVE),
                new User(3, "Peter", Status.INACTIVE),
                new User(4, "Anna", Status.ACTIVE),
                new User(5, "David", Status.INACTIVE)
            };
        }

        public User? FindById(int userId)
        {
            if (userId == null)
            {
                throw new ArgumentException("User id must not be null");
            }

            return userList.FirstOrDefault(user => userId == user.Id);
        }
    }
}
