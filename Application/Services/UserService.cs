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

        public UserService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IWebTokenGenerator webTokenGenerator, IUserAccessor userAccessor)
        {
            this.userAccessor = userAccessor;
            this.roleManager = roleManager;
            this.webTokenGenerator = webTokenGenerator;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<Response<LoggedUser>> GetCurrentUser()
        {
            var user = await userManager.FindByNameAsync(userAccessor.GetLoggedUserEmail());

            if(user == null)
            {
                return new Response<LoggedUser>("Użytkownik nie jest aktualnie zalogowany");
            }

            var userRole = await userManager.GetRolesAsync(user);

            var loggedUser = new LoggedUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Token = webTokenGenerator.CreateToken(user, userRole[0])
            };

            return new Response<LoggedUser>(loggedUser);
        }

        public async Task<Response<LoggedUser>> Login(UserCredentials credentials)
        {
            var user = await userManager.FindByEmailAsync(credentials.Email);

            if (user == null)
            {
                return new Response<LoggedUser>($"Użytkownik o adresie:{credentials.Email} nie został znaleziony");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, credentials.Password, false);

            var userRoles = await userManager.GetRolesAsync(user);

            if (result.Succeeded)
            {
                var loggedUser = new LoggedUser()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Token = webTokenGenerator.CreateToken(user, userRoles[0])
                };

                return new Response<LoggedUser>(loggedUser);
            }

            return new Response<LoggedUser>("Dane uwierzytelniające są nieprawidłowe");
        }

        public async Task<Response<LoggedUser>> Register(RegisterCredentials credentials, string userRole)
        {
            var existingUser = await userManager.FindByEmailAsync(credentials.Email);

            if (existingUser != null)
            {
                return new Response<LoggedUser>($"Użytkownik o adresie e-mail: {credentials.Email} już istnieje!");
            }

            var existingRole = await roleManager.RoleExistsAsync(userRole);

            if (!existingRole)
            {
                return new Response<LoggedUser>($"Rola: {userRole} nie istnieje w systemie!");
            }

            var user = new AppUser
            {
                FirstName = credentials.FirstName,
                LastName = credentials.LastName,
                DateOfBirth = credentials.DateOfBirth,
                Email = credentials.Email,
            };

            var result = await userManager.CreateAsync(user, credentials.Password);

            if (!result.Succeeded)
            {
                return new Response<LoggedUser>("Nie udało się utworzyć nowego użytkownika");
            }

            await userManager.AddToRoleAsync(user, userRole);
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, userRole));

            var logged = new LoggedUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Token = webTokenGenerator.CreateToken(user, userRole)
            };

            return new Response<LoggedUser>(logged);
        }

        public async Task<Response<UpdateUser>> UpdateUserDataAsync(UpdateUser updateUser)
        {
            var existingUser = await userManager.FindByEmailAsync(updateUser.OldEmail);

            if (existingUser == null)
            {
                return new Response<UpdateUser>("Zaloguj się aby wykonać tę operację!");
            }

            existingUser.Email = updateUser.Email;
            existingUser.PhoneNumber = updateUser.PhoneNumber;
            existingUser.FirstName = updateUser.FirstName;
            existingUser.LastName = updateUser.LastName;
            existingUser.DateOfBirth = updateUser.DateOfBirth;

            var result = await userManager.UpdateAsync(existingUser);

            if(!result.Succeeded)
            {
                return new Response<UpdateUser>(result.Errors.ToString());
            }

            return new Response<UpdateUser>(updateUser);
        }
    }
}