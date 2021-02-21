using System.Threading.Tasks;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IFacebookAccessor
    {
         Task<FacebookUserInfo> FacebookLogin(string accessToken);
    }
}