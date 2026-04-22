namespace JobWatch.Data;

public static class DummyUsers
{
    public record User(string Username, string Password);

    private static readonly List<User> _users =
    [
        new User("admin", "admin"),
        new User("candidate", "candidate"),
    ];

    public static User? Find(string username, string password) =>
        _users.FirstOrDefault(u =>
            u.Username == username && u.Password == password);
}
