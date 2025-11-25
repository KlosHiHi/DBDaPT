using AuthLibrary.Contexts;
using AuthLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthLibrary.Services
{
    public class AuthService(CinemaUserDbContext context)
    {
        private CinemaUserDbContext _context = context;
        private int _maxFailTry = 3;
        private int _blockDuration = 1;

        private string HashPassword(string password)
            => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        private bool VerifyPassword(string password, string passwordHash)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);

        private async Task<bool> IsUserLockAsync(CinemaUser user)
        {
            if (user.UnlockDate.HasValue && user.UnlockDate <= DateTime.UtcNow)
            {
                user.UnlockDate = null;
                user.FailTryAuthQuantity = 0;
                await _context.SaveChangesAsync();
            }

            return user.UnlockDate.HasValue;
        }

        public void LockUser(CinemaUser user)
        {
            user.UnlockDate = DateTime.UtcNow.AddMinutes(_blockDuration);
        }

        private async Task<bool> IsAuthCorrectAsync(string password, CinemaUser user)
        {
            if (VerifyPassword(password, user.PasswordHash))
                return true;

            await AddFailTryAsync(user);
            return false;
        }

        private async Task AddFailTryAsync(CinemaUser user)
        {
            user.FailTryAuthQuantity++;

            if (user.FailTryAuthQuantity == _maxFailTry)
                LockUser(user);

            await _context.SaveChangesAsync();
        }

        public async Task<CinemaUser> GetUserByLoginAsync(string login)
            => await _context.CinemaUsers
                .FirstOrDefaultAsync(u => u.Login == login) ?? null!;

        public async Task<bool> RegistrateUserAsync(string login, string password)
        {
            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
                return false;

            var user = await GetUserByLoginAsync(login);

            if (user is not null)
                return false;

            var role = await _context.CinemaUserRoles.FirstOrDefaultAsync(r => r.RoleName == "Посетитель");

            CinemaUser cinemaUser = new()
            {
                Login = login,
                PasswordHash = HashPassword(password),
                RoleId = role.RoleId
            };

            await _context.CinemaUsers.AddAsync(cinemaUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CinemaUser?> AuthUserAsync(string login, string password)
        {
            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
                return null;

            var user = await GetUserByLoginAsync(login);
            if (user is null)
                return null;

            if (await IsUserLockAsync(user))
                return null;

            return await IsAuthCorrectAsync(password, user) ?
                user :
                null;
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
                new List<CinemaPrivilege>();
        }

        public async Task<IEnumerable<CinemaPrivilege>> GetRolePrivileges(CinemaUserRole userRole)
        {
            var role = await _context.CinemaUserRoles
                .FirstOrDefaultAsync(r => r.RoleName == userRole.RoleName);

            return role is not null ?
                role.Privileges :
                new List<CinemaPrivilege>();
        }
    }
}
