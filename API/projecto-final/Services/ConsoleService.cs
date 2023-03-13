using Microsoft.EntityFrameworkCore;
using Projecto_Final.Contexts;
using Projecto_Final.Models;

namespace Projecto_Final.Services
{
    public class ConsoleService : IConsoleService
    {
        private readonly StoreContext _context;
        public ConsoleService(StoreContext context)
        {

            _context = context;
        }

        public async Task<IEnumerable<GameConsole>> GetAll()
        {
            return await _context.Consoles.ToListAsync();
        }

        public async Task<bool> Create(string consoleName)
        {

            var newConsole = new GameConsole
            {
                Name = consoleName,
                CreatedDate = DateTimeOffset.Now
            };

            await _context.Consoles.AddAsync(newConsole);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<GameConsole> GetbyId(int id)
        {
            var console = await _context.Consoles.FindAsync(id);
            if (console == null) return null;

            return console;
        }

        public async Task<bool> DeleteById(int id)
        {
            var DBconsole = await _context.Consoles.FindAsync(id);
            if (DBconsole == null) return false;

            _context.Consoles.Remove(DBconsole);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
