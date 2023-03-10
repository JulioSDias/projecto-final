using Microsoft.EntityFrameworkCore;
using Projecto_Final.Contexts;
using Projecto_Final.Helpers;
using Projecto_Final.Models;

namespace Projecto_Final.Services
{
    public class RoleService : IRoleService
    {
        private readonly StoreContext _context;
        private readonly IAppLogging _logging;

        public RoleService(StoreContext context, IAppLogging logging)
        {
            _context = context;
            _logging = logging;
        }

        public async Task<bool> Create(string newRole)
        {
            var newDBrole = new UserRole
            {
                RoleName = newRole,
                CreatedDate = DateTimeOffset.Now,
            };

            await _context.Roles.AddAsync(newDBrole);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserRole>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var RoleDB = await _context.Roles.FindAsync(id);

            if (RoleDB == null)
            {
                _logging.LogError("Role ID doesn't exist.");
                return false;
            }

            var users = await _context.Users.Include(r => r.Role).Where(i => i.RoleId == id).ToListAsync();

            foreach (User user in users) {
                user.RoleId = null;
                user.Role = null;
            }

            _context.Roles.Remove(RoleDB);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
