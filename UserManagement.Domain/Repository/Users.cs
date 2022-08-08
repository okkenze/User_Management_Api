using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;

namespace UserManagement.Domain.Repository
{
    public class Users : IUser
    {
        private readonly UsersDbContext _userDbContext;
        public Users(UsersDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public async Task<User> AddUser(User user)
        {
           
            _userDbContext.Users.Add(user);
            await _userDbContext.SaveChangesAsync();
            return user;
        }

        public async Task DeleteMultipleUser(List<int> id)
        {

            
            foreach(var item in id)
            {
               var user =  _userDbContext.Users.FirstOrDefault(x => x.Id == item);
                if(user != null)
                {
                    _userDbContext.Users.Remove(user);
                    await _userDbContext.SaveChangesAsync();
                    

                }
               
                
            }
        }

        public async Task DeleteUser(User user)
        {
            _userDbContext.Users.Remove(user);
            await _userDbContext.SaveChangesAsync();
        }

        public async Task<User> EditUser(User user)
        {
            var existingUser = await _userDbContext.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;
                existingUser.Gender = user.Gender;
                existingUser.DateOfBirth = user.DateOfBirth;
                existingUser.Nationality = user.Nationality;
                existingUser.RoleId = user.RoleId;
                _userDbContext.Users.Update(existingUser);

                await _userDbContext.SaveChangesAsync();


            }
            return user;
        }

        public async Task<ResponseObject> GetAllUsers()
        {
            //var users = await _userDbContext.Users.ToListAsync();
            var q = (from usr in _userDbContext.Users
                     join rl in _userDbContext.UserRoles on usr.RoleId equals rl.RoleId
                     orderby usr.Id
                     select new
                     {
                         usr.Id,
                         usr.FirstName,
                         usr.LastName,
                         usr.Email,
                         usr.Phone,
                         usr.Gender,
                         usr.DateOfBirth,
                         usr.Nationality,
                         rl.Role
                     }).ToListAsync();


            
            return new ResponseObject()
            {
                Data = q.Result,
               
            };
        }

        public async Task<User> GetUser(int Id)
        {

            var emp = await _userDbContext.Users.SingleOrDefaultAsync(e => e.Id == Id);
            return emp;
        }

        public async Task<ResponseObject> GetUserByEmailAndRole(string email, string role)
        {
            var res = (from u in _userDbContext.Users
                       where (u.Email == email )
                      join r in _userDbContext.UserRoles                   
                      on u.RoleId equals r.RoleId
                       where (r.Role == role)
                       orderby u.Id
                      select new
                      {
                          
                          u.Email,
                          r.Role
                      });

            if (res != null)
            {
                return new ResponseObject()
                {
                    Data = res,

                };
            }
            return new ResponseObject()
            {
                Data =res
            };
            
        }
    }
}
