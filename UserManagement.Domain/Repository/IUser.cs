using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;

namespace UserManagement.Domain.Repository
{
    public interface IUser
    {
        Task<ResponseObject> GetAllUsers();
        Task<ResponseObject> GetUserByEmailAndRole(string email,string role);
        Task<User> GetUser(int Id);
        Task<User> AddUser(User user);
        Task DeleteUser(User user);
        Task<User> EditUser(User user);
        Task DeleteMultipleUser(List<int> id);


    }
}
