using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.User;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        public UsersController(IMapper mapper, IUserService userService)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CurrentUser()
        {
            var user = await userService.GetCurrentUser();
            if(!user.Success)
            {
                return Unauthorized(user.Message);
            }

            var resource = mapper.Map<LoggedUser, LoggedUserResource>(user.Value);
            return Ok(resource);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserCredentialsResource credentialsResource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrors());
            }

            var credentials = mapper.Map<UserCredentialsResource, UserCredentials>(credentialsResource);
            var result = await userService.Login(credentials);

            if(!result.Success)
            {
                return Unauthorized(result.Message);
            }

            var resource = mapper.Map<LoggedUser, LoggedUserResource>(result.Value);
            return Ok(resource);
        }

        [AllowAnonymous]
        [HttpPost("login/facebook")]
        public async Task<ActionResult> FacebookLogin([FromBody] FacebookAccessTokenResource accessTokenResource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrors());
            }

            var result = await userService.FacebookLogin(accessTokenResource.AccessToken);

            if(!result.Success)
            {
                return Unauthorized(result.Message);
            }

            var resource = mapper.Map<LoggedUser, LoggedUserResource>(result.Value);
            return Ok(resource);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterCredentialsResource registerCredentials)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrors());
            }

            var credentials = mapper.Map<RegisterCredentialsResource, RegisterCredentials>(registerCredentials);
            var register = await userService.Register(credentials, "Admin");

            if(!register.Success)
            {
                return Unauthorized(register.Message);
            }

            var resource = mapper.Map<LoggedUser, LoggedUserResource>(register.Value);
            return Ok(resource);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterCredentialsResource registerCredentials)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrors());
            }

            var credentials = mapper.Map<RegisterCredentialsResource, RegisterCredentials>(registerCredentials);
            var register = await userService.Register(credentials, "RegularUser");

            if(!register.Success)
            {
                return Unauthorized(register.Message);
            }

            var resource = mapper.Map<LoggedUser, LoggedUserResource>(register.Value);
            return Ok(resource);
        }

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserDataAsync([FromBody] UpdateUserResource userResource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrors());
            }

            var updateUser = mapper.Map<UpdateUserResource, UpdateUser>(userResource);
            var update = await userService.UpdateUserDataAsync(updateUser);

            if(!update.Success)
            {
                return Unauthorized(update.Message);
            }

            return NoContent();
        }
 
    }
}