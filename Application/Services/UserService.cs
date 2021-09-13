using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IWebTokenGenerator webTokenGenerator;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserAccessor userAccessor;
        private readonly IFacebookAccessor facebookAccessor;

        public UserService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IWebTokenGenerator webTokenGenerator, IUserAccessor userAccessor, IFacebookAccessor facebookAccessor)
        {
            this.facebookAccessor = facebookAccessor;
            this.userAccessor = userAccessor;
            this.roleManager = roleManager;
            this.webTokenGenerator = webTokenGenerator;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<Response<LoggedUser>> FacebookLogin(string accessToken)
        {
            var userInfo = await facebookAccessor.FacebookLogin(accessToken); 
            if(userInfo == null)
            {
                return Response<LoggedUser>.Failure(ResponseResult.UserIsNotAuthorized, "Nie udało się zalogować z wykorzystaniem dostawcy: Facebook");
            }

            var user = await userManager.FindByNameAsync("fb_" + userInfo.Id);

            if(user == null)
            {
                user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = userInfo.Name.Split(' ', 2)[0],
                    LastName = userInfo.Name.Split(' ', 2)[1],
                    Email = userInfo.Email,
                    UserName = "fb_" + userInfo.Id,
                    PhotoUrl = userInfo.Picture.Data.Url,
                    Tags = new Collection<Tag>(),
                    TodoSets = new Collection<TodoSet>(),
                };

                var result = await userManager.CreateAsync(user);

                if(!result.Succeeded)
                {
                    return Response<LoggedUser>.Failure(ResponseResult.BadRequestStructure, "Nie udało się utworzyć konta użytkownika dla podanych informacji");
                }

                await userManager.AddToRoleAsync(user, "RegularUser");
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "RegularUser"));
            }

            var existingRole = await roleManager.RoleExistsAsync("RegularUser");

            if (!existingRole)
            {
                return Response<LoggedUser>.Failure(ResponseResult.BadRequestStructure, "Nie udało się utworzyć konta użytkownika dla podanych informacjiB");
            }

            var loggedUser = new LoggedUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                PhotoUrl = user.PhotoUrl,
                Token = webTokenGenerator.CreateToken(user, "RegularUser")
            };

            return Response<LoggedUser>.Success(ResponseResult.DataObtained, loggedUser);
        }

        public async Task<Response<LoggedUser>> GetCurrentUser()
        {
            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());

            if (user == null)
            {
                return Response<LoggedUser>.Failure(ResponseResult.UserIsNotAuthorized, "Użytkownik nie jest aktualnie zalogowany");
            }

            var userRole = await userManager.GetRolesAsync(user);

            var loggedUser = new LoggedUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Token = webTokenGenerator.CreateToken(user, userRole[0])
            };

            return Response<LoggedUser>.Success(ResponseResult.DataObtained, loggedUser);
        }

        public async Task<Response<LoggedUser>> Login(UserCredentials credentials)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(p => p.Email == credentials.Email && !p.UserName.StartsWith("fb"));

            if (user == null)
            {
                return Response<LoggedUser>.Failure(ResponseResult.ResourceDoesntExist,$"Użytkownik o adresie:{credentials.Email} nie został znaleziony");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, credentials.Password, false);

            var userRoles = await userManager.GetRolesAsync(user);

            if (!result.Succeeded) 
                return Response<LoggedUser>.Failure(ResponseResult.UserIsNotAuthorized, "Dane uwierzytelniające są nieprawidłowe");
            
            
            var loggedUser = new LoggedUser()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Token = webTokenGenerator.CreateToken(user, userRoles[0])
            };

            return Response<LoggedUser>.Success(ResponseResult.DataObtained, loggedUser);

        }

        public async Task<Response<LoggedUser>> Register(RegisterCredentials credentials, string userRole)
        {
            var existingUser = await userManager.FindByEmailAsync(credentials.Email);

            if (existingUser != null)
            {
                return Response<LoggedUser>.Failure(ResponseResult.BadRequestStructure, $"Użytkownik o adresie e-mail: {credentials.Email} już istnieje!");
            }

            var existingRole = await roleManager.RoleExistsAsync(userRole);

            if (!existingRole)
            {
                return Response<LoggedUser>.Failure(ResponseResult.ResourceDoesntExist, $"Rola: {userRole} nie istnieje w systemie!");
            }

            var user = new AppUser
            {
                FirstName = credentials.FirstName,
                LastName = credentials.LastName,
                UserName = $"{credentials.FirstName}{credentials.LastName}{Guid.NewGuid().ToString()}",
                Email = credentials.Email,
                Tags = new Collection<Tag>(),
                TodoSets = new Collection<TodoSet>(),
            };

            var result = await userManager.CreateAsync(user, credentials.Password);

            if (!result.Succeeded)
            {
                return Response<LoggedUser>.Failure(ResponseResult.InternalError, "Nie udało się utworzyć nowego użytkownika");
            }

            await userManager.AddToRoleAsync(user, userRole);
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, userRole));

            var logged = new LoggedUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Token = webTokenGenerator.CreateToken(user, userRole)
            };

            return Response<LoggedUser>.Success(ResponseResult.DataObtained, logged);
        }

        public async Task<Response<UpdateUser>> UpdateUserDataAsync(UpdateUser updateUser)
        {
            var existingUser = await userManager.FindByEmailAsync(updateUser.OldEmail);

            if (existingUser == null)
            {
                return Response<UpdateUser>.Failure(ResponseResult.UserIsNotAuthorized,"Zaloguj się aby wykonać tę operację!");
            }

            existingUser.Email = updateUser.Email;
            existingUser.PhoneNumber = updateUser.PhoneNumber;
            existingUser.FirstName = updateUser.FirstName;
            existingUser.LastName = updateUser.LastName;

            var result = await userManager.UpdateAsync(existingUser);

            if (!result.Succeeded)
            {
                return Response<UpdateUser>.Failure(ResponseResult.InternalError, result.Errors.ToString());
            }

            return Response<UpdateUser>.Success(ResponseResult.Updated);
        }
    }
}