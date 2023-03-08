using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projecto_Final.Contexts;
using Projecto_Final.Helpers;
using Projecto_Final.Models;
using Projecto_Final.Models.RoleDTOs;
using Projecto_Final.Models.UserDTOs;
using SodokuAPI.Helpers;

namespace Projecto_Final.Services
{
    public class UserService : IUserService
    {
        private readonly StoreContext _context;
        private readonly ISecurity _security;
        private readonly IAppLogging _logging;

        public UserService(StoreContext  context, ISecurity security, IAppLogging logging) {
            _context = context;
            _security = security;
            _logging = logging;
        }

        public async Task<IEnumerable<User>> GetAllUsers() 
        {
            return await _context.Users.Include(r => r.Role).ToListAsync();
        }

        public async Task<bool> CreateUser(UserRegisterDTO newUser, int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);

            if (role == null) {
                _logging.LogError("role doesn't exist.");
                return false;
            }
            if (_context.Users.Any(x => x.Username == newUser.UserName) == true) {
                _logging.LogError("username already exists.");
                return false;
            }
            if (newUser.Password.Any(ch => !char.IsLetterOrDigit(ch)) == true) {
                _logging.LogError("invalid password");
                return false;
            }

            byte[] passwordHash, passwordSalt;
            passwordSalt = _security.CreatePasswordSalt(newUser.Password);
            passwordHash = _security.CreatePasswordHash(newUser.Password, passwordSalt);

            var newDBuser = new User
            {
                Username = newUser.UserName,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                SocialSecurity = newUser.SocialSecurity,
                PhoneNumber = newUser.PhoneNumber,
                Address = newUser.Address,
                City = newUser.City,
                Country = newUser.Country,
                PostalCode = newUser.PostalCode,
                CreatedDate = DateTimeOffset.Now,
                RoleId = role.Id,
                Role = role
            };

            

            await _context.Users.AddAsync(newDBuser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateRole(RoleCreateDTO newRole) {

            var newDBrole = new UserRole
            {
                RoleName = newRole.Name,
                CreatedDate = DateTimeOffset.Now,
            };

            await _context.Roles.AddAsync(newDBrole);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserRole>> GetAllRoles() {
            return await _context.Roles.ToListAsync();
        }

        public async Task<bool> DeleteRole(int RoleId) {
            var RoleDB = await _context.Roles.FindAsync(RoleId);

            if (RoleDB == null) {
                _logging.LogError("Role ID doesn't exist.");
                return false;
            }
            _context.Roles.Remove(RoleDB);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
