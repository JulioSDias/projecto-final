using Microsoft.EntityFrameworkCore;
using Projecto_Final.Contexts;
using Projecto_Final.Helpers;
using Projecto_Final.Models;
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

        public async Task<UserReturnDTO> GetByUsername(string username) {
            
            var DBuser = await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(u => u.Username == username);
            if(DBuser == null) 
                return null;

            var ReturnUser = new UserReturnDTO {
                Id = DBuser.Id,
                FirstName = DBuser.FirstName,
                LastName = DBuser.LastName,
                Email = DBuser.Email,
                SocialSecurity = DBuser.SocialSecurity,
                PhoneNumber = DBuser.PhoneNumber,
                Address = DBuser.Address,
                City = DBuser.City,
                Country = DBuser.Country,
                PostalCode = DBuser.PostalCode,
                CreatedDate = DBuser.CreatedDate,
                ModifiedDate = DBuser.ModifiedDate,
                RoleName = DBuser.Role.RoleName,
            };

            return ReturnUser;
        }

        public async Task<bool> Delete(string username) {
            var user = GetByUsername(username);
            if (user == null) return false;

            var DBuser = await _context.Users.FindAsync(user.Id);
            if (DBuser == null) return false;

            _context.Users.Remove(DBuser);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
