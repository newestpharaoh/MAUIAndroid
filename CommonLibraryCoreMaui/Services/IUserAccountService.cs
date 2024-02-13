using CommonLibraryCoreMaui.Models;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.Services
{
    public interface IUserAccountService
    {
        Task<StatusResponse> UserAccountAuthenticateCode(int userId, string code);
        Task<StatusResponse> UserAccountSendCode(int userId, string sendMethod); 
        Task<UserContactPreference> GetUserContactPreference(int userId);
    }
}
