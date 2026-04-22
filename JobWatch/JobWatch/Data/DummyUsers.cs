namespace JobWatch.Data;

public static class DummyUsers
{
    public record User(string Username, string Password, string[] Roles);

    private static readonly List<User> _users =
    [
        new User("admin", "admin", ["Admin"]),
        new User("candidate", "candidate", ["Candidate"]),
    ];

    public static User? Find(string username, string password) =>
        _users.FirstOrDefault(u =>
            u.Username == username && u.Password == password);
}
