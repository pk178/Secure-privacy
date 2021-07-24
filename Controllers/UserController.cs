using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secure_privacy.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<User>> Details(string name)
        {
            return await DataBase.DAO_User.GetUser(name);
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                if (await DataBase.DAO_User.CreateUser(user)) return Ok();
                
            }
            catch(Exception ex)
            {
                Log.Error(ex, "UserController - Create");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> Update(User user)
        {
            try
            {
                if (await DataBase.DAO_User.UpdateUser(user)) return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "UserController - Update");
            }
            
            return BadRequest();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(string loginName)
        {
            try
            {
                if (await DataBase.DAO_User.DeleteUser(loginName)) return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "UserController - Delete");
            }

            return BadRequest();
        }        
    }
}
