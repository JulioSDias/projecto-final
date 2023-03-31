using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Projecto_Final.Contexts;
using Projecto_Final.Helpers;
using Projecto_Final.Models;
using Projecto_Final.Models.UserDTOs;

namespace Projecto_Final.Services
{
    public class UserService : IUserService
    {
        private readonly StoreContext _context;
        private readonly ISecurity _security;
        private readonly IAppLogging _logging;
        private readonly IJwtTokenAuth _jwtauth;

        public UserService(StoreContext  context, ISecurity security, IAppLogging logging, IJwtTokenAuth jwtauth)
        {
            _context = context;
            _security = security;
            _logging = logging;
            _jwtauth = jwtauth;
        }

        public async Task<IEnumerable<User>> GetAllUsers() 
        {
            return await _context.Users.Include(r => r.Role).ToListAsync();
        }

        public async Task<bool> CreateUser(UserRegisterDTO newUser)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "client");

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

            string roleName;
            if (DBuser.RoleId == null)
                roleName = "none";
            else
                roleName = DBuser.Role.RoleName;

            var ReturnUser = new UserReturnDTO {
                Id = DBuser.Id,
                Username = DBuser.Username,
                Password = "SECRET",
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
                RoleName = roleName,
            };

            return ReturnUser;
        }

        public async Task<bool> DeleteByUsername(string username) {
            var user = await GetByUsername(username);
            if (user == null) return false;

            var DBuser = await _context.Users.FindAsync(user.Id);
            if (DBuser == null) return false;

            _context.Users.Remove(DBuser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var DBuser = await _context.Users.FindAsync(id);
            if (DBuser == null) return false;

            _context.Users.Remove(DBuser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangeRole(UserUpdateRoleDTO newChanges) {
            var DBuser = await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(u => u.Username == newChanges.Username);
            if (DBuser == null) return false;

            var DBrole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == newChanges.RoleName);
            if (DBrole == null) return false;

            DBuser.RoleId = DBrole.Id;
            DBuser.Role = DBrole;
            DBuser.ModifiedDate = DateTimeOffset.Now;
            
            
            _context.Users.Entry(DBuser).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Update(Guid id, UserUpdateDTO userChanges) {
            var DBuser = await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(i => i.Id == id);
            if (DBuser == null) return false;

            var DBrole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == userChanges.RoleName);
            if (DBrole == null) return false;

            if(userChanges.Password != null) {
                byte[] passwordHash, passwordSalt;
                passwordSalt = _security.CreatePasswordSalt(userChanges.Password);
                passwordHash = _security.CreatePasswordHash(userChanges.Password, passwordSalt);
                DBuser.PasswordSalt = passwordSalt;
                DBuser.PasswordHash = passwordHash;
            }

            DBuser.Username = userChanges.Username;
            DBuser.Email = userChanges.Email;
            DBuser.SocialSecurity = userChanges.SocialSecurity;
            DBuser.PhoneNumber = userChanges.PhoneNumber;
            DBuser.Address = userChanges.Address;
            DBuser.City = userChanges.City;
            DBuser.Country = userChanges.Country;
            DBuser.PostalCode = userChanges.PostalCode;
            DBuser.ModifiedDate = DateTimeOffset.Now;
            DBuser.RoleId = DBrole.Id;
            DBuser.Role = DBrole;


            _context.Users.Entry(DBuser).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;

        } 

        public async Task<List<string>> GetByRole(string rolename)
        {
            var users = await _context.Users.Include(r => r.Role).Where(i => i.Role.RoleName == rolename).ToListAsync();

            var usernames = new List<string>();
            foreach (User user in users)
            {
                usernames.Add(user.Username);
            }
            return usernames;
        }


        public async Task<UserReturnDTO> GetById(Guid id) { 
            var DBuser = await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(i => i.Id == id);
            if (DBuser == null) return null;

            string roleName;
            if (DBuser.RoleId == null)
                roleName = "none";
            else
                roleName = DBuser.Role.RoleName;

            var ReturnUser = new UserReturnDTO
            {
                Id = DBuser.Id,
                Username = DBuser.Username,
                Password = "SECRET",
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
                RoleName = roleName,
            };
            return ReturnUser;
        }

        public async Task<UserAuthenticateDTO?> Authenticate(string Username, string Password) {

            if (Username.IsNullOrEmpty() || Password.IsNullOrEmpty())
                return null;

            var user = await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(u => u.Username == Username);

            if (user == null) return null;

            if (_security.VerifyPassword(Password, user.PasswordHash, user.PasswordSalt)) {
               string token = _jwtauth.GenerateJwtToken(user);

                var userInfo = new UserAuthenticateDTO
                {
                    UserName = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Token = token
                };
                return userInfo;
            }

            return null;
        }

    }
}