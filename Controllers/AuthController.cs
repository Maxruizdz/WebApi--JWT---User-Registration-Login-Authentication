using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

using Web_Api_CreateJWT_User_Regis_Authentication.Models;

namespace Web_Api_CreateJWT_User_Regis_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        [HttpPost("Register")]
        public ActionResult<User> Register(UserDto request) {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash);

            user.Username = request.Username;
            user.Password = passwordHash;
        
        return new JsonResult(user);
        
         }

        [HttpPost("Login")]
        public ActionResult<User> Login(UserDto request ) {


            if (user.Username!=request.Username) {

                return BadRequest("User not found");
            
            
            
           }

            if (!BCrypt.Net.BCrypt.Verify(request.PasswordHash, user.Password)) {



                return BadRequest("Wrong password.");
            
            
            }        


            return Ok(user);
        
        
        }




    }
}
