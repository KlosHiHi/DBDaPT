using BCrypt.Net;
using lection1106.Contexts;
using lection1106.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Cryptography");

string login = "admin";
string password = "admin1";

using var context = new AppDbContext();
static void LockUser()
{
    var login = "admin";
    var password = "123";
    using var context = new AppDbContext();
    var user = context.Users.FirstOrDefault(u => u.Login == login);

    if (user is null)
    {
        Console.WriteLine("not found");
        return;
    }

    if (IsUserLocked(user))
    {
        Console.WriteLine($"locked until {user.LockedUntil:HH:mm:ss}");
        return;
    }

    // проверка, что попытка аутентификации неуспешна
    if (!IsPasswordCorrect(password, user))
    {
        Console.WriteLine("incorrect password");
        context.SaveChanges();
        return;
    }

    SuccessLogin(user);
    context.SaveChanges();

    Console.WriteLine("welcome");
    return;
}

static void ComputeHash()
{
    string salt = "!al2";
    string password = "parol" + salt;
    byte[] bytes = Encoding.UTF8.GetBytes(password);

    SHA384 algo = SHA384.Create();
    var hashBytes = algo.ComputeHash(bytes);

    var hash = Convert.ToBase64String(hashBytes); // base64 ASCII
    Console.WriteLine(hash);
    hash = Convert.ToHexString(hashBytes); // hex: 0-9A-F
    Console.WriteLine(hash);
}

static void ComputeBcryptHash()
{
    var password = "qwerty";
    var hash = BCrypt.Net.BCrypt.EnhancedHashPassword(
        password, 15, HashType.SHA512);

    var input = "12345";
    var isCorrect = BCrypt.Net.BCrypt.EnhancedVerify(input, hash, HashType.SHA512);
}

static async Task InsertData()
{
    var users = new List<User>()
    {
        new User{ Login = "admin", Password = "admin"},
        new User{ Login = "manager", Password = "12345"},
        new User{ Login = "customer", Password = "llo12"},
        new User { Login = "user", Password = "user1" } ,
    };
    var context = new AppDbContext();

    await context.Users.AddRangeAsync(users);

    await context.SaveChangesAsync();
}

static bool IsUserLocked(User user)
{
    if (user.LockedUntil.HasValue && user.LockedUntil <= DateTime.UtcNow)
    {
        user.FailedLoginAttempts = 0;
        user.LockedUntil = null;
        return false;
    }
    return user.LockedUntil.HasValue;
}

static bool IsPasswordCorrect(string password, User user)
{
    int attempts = 3;
    int duration = 30;

    if (user.Password != password)
    {
        user.FailedLoginAttempts++;
        if (user.FailedLoginAttempts >= attempts)
            user.LockedUntil = DateTime.UtcNow.AddSeconds(duration);
        return false;
    }
    return true;
}

static void SuccessLogin(User user)
{
    user.FailedLoginAttempts = 0;
    //user.LockedUntil = null;
    user.LastAccess = DateTime.UtcNow;
}