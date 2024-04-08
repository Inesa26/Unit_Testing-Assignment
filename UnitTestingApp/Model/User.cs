namespace UnitTestingApp.Model
{
    public class User
    {
        public int Id;
        public string Name;
        public Status Status;

        public User(int id, string name, Status status)
        {
            Id = id;
            Name = name;
            Status = status;
        }
    }
}
