namespace JobWatch.Data;

public static class DummyUsers
{
    public record User(string Username, string Password, string[] Roles, Dictionary<string, string> Claims);

    private static readonly List<User> _users =
    [
        new User("admin", "admin", ["Admin"], new Dictionary<string, string> { ["Department"] = "Engineering" }),
        new User("candidate", "candidate", ["Candidate"], new Dictionary<string, string> { ["Department"] = "Sales" }),
    ];

    public static User? Find(string username, string password) =>
        _users.FirstOrDefault(u =>
            u.Username == username && u.Password == password);
}
