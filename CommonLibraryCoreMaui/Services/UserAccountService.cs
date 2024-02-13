using System;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui.Services
{
    public class UserAccountService : IUserAccountService
    {
        public Task<StatusResponse> UploadImageAsync(byte[] imageBytes)
        {
            throw new NotImplementedException();
        }

        public async Task<StatusResponse> UserAccountAuthenticateCode(int userId, string code)
        {
            return await DataUtility.UserAccountAuthenticateCode(SettingsValues.ApiURLValue, userId, code);
        }

        public async Task<StatusResponse> UserAccountSendCode(int userId, string sendMethod)
        {
            return await DataUtility.UserAccountSendCode(SettingsValues.ApiURLValue, userId, sendMethod, string.Empty, null);
        }

        public async Task<UserContactPreference> GetUserContactPreference(int userId)
        {
            return await DataUtility.GetUserContactPreferenceAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, userId);
        }
    }
}
