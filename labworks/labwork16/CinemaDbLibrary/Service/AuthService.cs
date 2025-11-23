using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.DTOs;
using CinemaDbLibrary.Models;
using CinemaDbLibrary.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CinemaDbLibrary.Service
{
    public class AuthService(CinemaDbContext context)
    {
        private readonly CinemaDbContext _context = context;
        private int _maxFailTry = 3;
        private int _jwtActiveMinutes = 15;
        private int _blockDuration = 30;

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
            }

            return user.UnlockDate.HasValue;
        }

        private void LockUser(CinemaUser user, int secondsDuration)
        {
            user.UnlockDate = DateTime.UtcNow.AddSeconds(secondsDuration);
        }


        private bool IsAuthCorrect(string password, CinemaUser user)
        {
            if (VerifyPassword(password, user.PasswordHash))
                return true;

            AddFailTry(user);
            return false;
        }

        private void AddFailTry(CinemaUser user)
        {
            user.FailTryAuthQuantity++;

            if (user.FailTryAuthQuantity == _maxFailTry)
                LockUser(user, _blockDuration);
        }

        private async Task<TokenResponse> GenerateToken(CinemaUser user)
        {
            int id = user.UserId;
            string login = user.Login;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var role = await GetUserRoleAsync(login);

            var claims = new Claim[]
            {
                new ("id", id.ToString()),
                new ("login", login),
                new ("role", role.RoleName),
            };


            var token = new JwtSecurityToken(
                signingCredentials: credentials,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtActiveMinutes),
                issuer: AuthOptions.issuer,
                audience: AuthOptions.audience);


            return new TokenResponse(new JwtSecurityTokenHandler().WriteToken(token), null);
        }

        public async Task<bool> RegistrateUserAsync(LoginRequest request)
        {
            string login = request.Login;
            string password = request.Password;

            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
                return false;

            var user = await GetUserByLoginAsync(login);

            if (user is not null)
                return false;

            CinemaUser cinemaUser = new()
            {
                Login = login,
                PasswordHash = HashPassword(password),
                RoleId = 3
            };

            await _context.CinemaUsers.AddAsync(cinemaUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TokenResponse?> AuthUserAsync(LoginRequest request)
        {
            string login = request.Login;
            string password = request.Password;

            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
                return null;

            var user = await GetUserByLoginAsync(login);
            if (user is null)
                return null;

            if (await IsUserLockAsync(user))
                return null;

            return IsAuthCorrect(password, user) ?
                await GenerateToken(user) :
                null;
        }

        public async Task<CinemaUser> GetUserByLoginAsync(string login)
            => await _context.CinemaUsers
                .FirstOrDefaultAsync(u => u.Login == login) ?? null!;

        public async Task<CinemaUserRole?> GetUserRoleAsync(string login)
        {
            var user = await _context.CinemaUsers
                .Include(c => c.Role)
                .FirstOrDefaultAsync(cu => cu.Login == login);

            return user is not null ?
                 user.Role :
                 null;
        }

        public async Task<IEnumerable<CinemaPrivilege>> GetUserPrivileges(string login)
        {
            var role = await GetUserRoleAsync(login);

            return role is not null ?
                role.Privileges :
                null!;
        }

        public async Task<IEnumerable<CinemaPrivilege>?> GetRolePrivileges(CinemaUserRole userRole)
        {
            var role = await _context.CinemaUserRoles
                .FirstOrDefaultAsync(r => r.RoleName == userRole.RoleName);

            return role is not null ?
                role.Privileges :
                null;
        }
    }
}
