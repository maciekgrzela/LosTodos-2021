using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.User;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseController
    {
        
        [HttpGet]
        public async Task<IActionResult> CurrentUser()
        {
            var user = await UserService.GetCurrentUser();
            var resource = Mapper.Map<Response<LoggedUser>, Response<LoggedUserResource>>(user);
            
            return HandleResult(resource);
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsResource credentialsResource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrors());
            }
        
            var credentials = Mapper.Map<UserCredentialsResource, UserCredentials>(credentialsResource);
            var userLogged = await UserService.Login(credentials);
            var resource = Mapper.Map<Response<LoggedUser>, Response<LoggedUserResource>>(userLogged);
            
            return HandleResult(resource);
        }
        
        [AllowAnonymous]
        [HttpPost("login/facebook")]
        public async Task<IActionResult> FacebookLogin([FromBody] FacebookAccessTokenResource accessTokenResource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrors());
            }
        
            var loggedByFacebook = await UserService.FacebookLogin(accessTokenResource.AccessToken);
            var resource = Mapper.Map<Response<LoggedUser>, Response<LoggedUserResource>>(loggedByFacebook);
            
            return HandleResult(resource);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterCredentialsResource registerCredentials)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrors());
            }
        
            var credentials = Mapper.Map<RegisterCredentialsResource, RegisterCredentials>(registerCredentials);
            var registered = await UserService.Register(credentials, "Admin");
            var resource = Mapper.Map<Response<LoggedUser>, Response<LoggedUserResource>>(registered);
            
            return HandleResult(resource);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterCredentialsResource registerCredentials)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrors());
            }
        
            var credentials = Mapper.Map<RegisterCredentialsResource, RegisterCredentials>(registerCredentials);
            var registered = await UserService.Register(credentials, "RegularUser");
            var resource = Mapper.Map<Response<LoggedUser>, Response<LoggedUserResource>>(registered);
            
            return HandleResult(resource);
        }
        
        [HttpPut]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> UpdateUserDataAsync([FromBody] UpdateUserResource userResource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrors());
            }
        
            var userToUpdate = Mapper.Map<UpdateUserResource, UpdateUser>(userResource);
            var userUpdated = await UserService.UpdateUserDataAsync(userToUpdate);

            return HandleResult(userUpdated);
        }
 
    }
}