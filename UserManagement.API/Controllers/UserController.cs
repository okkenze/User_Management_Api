using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.API.Attributes;
using UserManagement.Domain;
using UserManagement.Domain.Models;
using UserManagement.Domain.Repository;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        /// <summary>
        /// Thie endpoint is used to create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("createUser")]
        public async Task<ActionResult> Createuser([FromBody] UserMapper user)
        {

            if (!ModelState.IsValid)
                   return BadRequest();
            
            try
            {
                var usr = new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    Nationality = user.Nationality,
                    RoleId=user.RoleId

                };
                await _user.AddUser(usr);
                return StatusCode(201,user);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Error = ex.Message });
            }
        }

        /// <summary>
        /// This endpoint deletes a user using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _user.GetUser(id);

                if (user != null)
                {
                  await  _user.DeleteUser(user);
                    return Ok(new { Message = $"Employee with Id: {id} deleted successfully" });
                }

                return NotFound(new { Message = $"Employee with Id: {id} was not found" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Error = ex.Message });
            }

        }

        /// <summary>
        /// This deletes multiple user by passing a list ids
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteMultipleUser")]
        public async Task<ActionResult> DeleteUser(List<int> id)
        {
            try
            {
                
               
                   await _user.DeleteMultipleUser(id);
                    return Ok(new { Message = $"Users deleted successfully" });
                

               
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Error = ex.Message });
            }

        }

        /// <summary>
        /// This endpoint is used to retrieve all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getAllUsers")]
        public async Task<ActionResult> GetUsers()
        {
            try
            {

                var users = await _user.GetAllUsers();

                return Ok(users);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Error = ex.Message });
            }

        }
        /// <summary>
        /// This retrieves a user using email and role
        /// </summary>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getUserByEmailAndRole/{email}/{role}")]
        public async Task<ActionResult> GetUserByEmailAndRole(string email,string role)
        {
            try
            {

                var users = await _user.GetUserByEmailAndRole(email,role);
                if(users != null)
                {
                    return Ok(users);
                }
                return StatusCode(400, new {v= "User Not Found" });

               
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Error = ex.Message });
            }

        }


        /// <summary>
        /// Retrieves a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getUser/{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            try
            {

                var users = await _user.GetUser(id);

                return Ok(users);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Error = ex.Message });
            }

        }

        /// <summary>
        /// Updates a user detail
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateUser")]
        public async Task<ActionResult> EditUser( [FromBody] User user)
        {
            try
            {
                var usr = await _user.GetUser(user.Id);

                if (usr != null)
                {
                    usr.Id = user.Id;
                   

                    await _user.EditUser(user);
                    return Ok(usr);
                }

                return NotFound(new { Message = $"Employee with Id: {user.Id} was not found" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Error = ex.Message });
            }

        }
    }
}
