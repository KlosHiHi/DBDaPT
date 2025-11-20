using AuthLibrary.Contexts;
using AuthLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthLibrary.Services
{
    public class AuthService(CinemaUserDbContext context)
    {
        private CinemaUserDbContext _context = context;
        private int _maxFailTry = 3;

        private string PasswordHashing(string password)
            => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        private bool IsPasswordRight(string password, string passwordHash)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);

        static void CheckUserLock(CinemaUser user)
        {
            if (user.UnlockDate <= DateTime.UtcNow)
                user.FailTryAuthQuantity = 0;
        }

        public void LockUser(CinemaUser user)
        {
            user.UnlockDate = DateTime.UtcNow.AddMinutes(5);
        }

        public async Task<bool> RegistrateUserAsync(string login, string password)
        {
            if (await _context.CinemaUsers.AnyAsync(cu => cu.Login == login))
                return false;

            CinemaUser cinemaUser = new()
            {
                Login = login,
                PasswordHash = PasswordHashing(password),
                RoleId = 3
            };

            await _context.CinemaUsers.AddAsync(cinemaUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CinemaUser?> AuthUserAsync(string login, string password)
        {
            var user = await _context.CinemaUsers
                .FirstOrDefaultAsync(cu => cu.Login == login);

            if (user is not null)
            {
                CheckUserLock(user);

                if (IsPasswordRight(password, user.PasswordHash))
                    return user;
                else
                    user.FailTryAuthQuantity++;

                if (user.FailTryAuthQuantity == _maxFailTry)
                    LockUser(user);
            }

            return null;
        }

        public async Task<CinemaUserRole> GetUserRoleAsync(string login)
        {
            var user = await _context.CinemaUsers
                .Include(c => c.Role)
                .FirstOrDefaultAsync(cu => cu.Login == login);

            return user is not null ?
                 user.Role :
                 null!;
        }

        public async Task<IEnumerable<CinemaPrivilege>> GetUserPrivileges(string login)
        {
            var role = await GetUserRoleAsync(login);

            return role is not null ?
                role.Privileges :
                null!;
        }

        public async Task<IEnumerable<CinemaPrivilege>> GetRolePrivileges(CinemaUserRole userRole)
        {
            var role = await _context.CinemaUserRoles
                .FirstOrDefaultAsync(r => r.RoleName == userRole.RoleName);

            return role is not null ?
                role.Privileges :
                null!;
        }
    }
}
