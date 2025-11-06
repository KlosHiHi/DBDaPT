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
var user = await context.Users.FirstOrDefaultAsync(u => u.Login == login);

if (user is null)
{
    Console.WriteLine("not found user");
    return;
}

if (user.LockedUntil.HasValue && user.LockedUntil >= DateTime.UtcNow)
{
    Console.WriteLine($"user is locked, until {user.LockedUntil}");
    return;
}

if (user.Password != password)
{
    user.FailedLoginAttemps++;

    if (user.FailedLoginAttemps >= 3)
        user.LockedUntil = DateTime.UtcNow.AddMinutes(10);

    await context.SaveChangesAsync();

    Console.WriteLine("password is uncorrect");
    return;
}

user.LastAccess = DateTime.UtcNow;
user.FailedLoginAttemps = 0;
user.LockedUntil = null;

context.SaveChanges();

Console.WriteLine("Welcome");

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