using System.Threading.Tasks;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
         Task<Response<LoggedUser>> Login(UserCredentials credentials);
         Task<Response<LoggedUser>> Register(RegisterCredentials credentials, string userRole);
         Task<Response<UpdateUser>> UpdateUserDataAsync(UpdateUser updateUser);
         Task<Response<LoggedUser>> GetCurrentUser();
    }
}