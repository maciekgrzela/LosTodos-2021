using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IWebTokenGenerator
    {
         string CreateToken(AppUser user, string role);
    }
}