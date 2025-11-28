using GameStoreDbLibrary.Contexts;
using GameStoreDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreDbLibrary.Services
{
    public class GamesService(GameStoreDbContext context)
    {
        private GameStoreDbContext _context = context;

        public async Task<IEnumerable<string>> GetGamesNameAsync()
            => await _context.Lw23games.Select(g => g.Name).ToListAsync();

        public async Task<string> GetGamesLogosByName(string name)
        {
            var game = await GetGameByName(name);

            return game?.LogoFile ?? "";
        }

        public async Task<Lw23game?> GetGameByName(string name)
            => await _context.Lw23games.FirstOrDefaultAsync(g => g.Name == name);

        public async Task<bool> TryChangeLogoFile(string logoFileName, string gameName)
        {
            var game = await GetGameByName(gameName);

            if (game is null)
                return false;

            game.LogoFile = logoFileName;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> TryUploadScreenshot(string gameName, string fileName, byte[] screenshotData)
        {
            var game = await GetGameByName(gameName);

            if (game is null)
                return false;

            Lw23screenshot Screenshot = new()
            {
                GameId = game.GameId,
                FileName = fileName,
                Photo = screenshotData                
            };

            try
            {
                await _context.Lw23screenshots.AddAsync(Screenshot);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
