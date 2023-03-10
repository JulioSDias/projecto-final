﻿using Projecto_Final.Models;
using Projecto_Final.Models.UserDTOs;

namespace Projecto_Final.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> CreateUser(UserRegisterDTO newUser);
        Task<UserReturnDTO> GetByUsername(string username);
        Task<bool> DeleteByUsername(string username);
        Task<bool> DeleteById(Guid id);
        Task<bool> ChangeRole(UserUpdateDTO newChanges);
        Task<List<string>> GetByRole(string rolename);
        Task<UserReturnDTO> GetById(Guid id);



    }
}
