using System.Threading.Tasks;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui.Services
{
    public class PatientLogoutService : IiOSLogout
    {
        public async Task<bool> Logout(Acr.UserDialogs.IUserDialogs userDialogs)
        {
            CommonAuthSession.ClearSession();

            //clear account add members if left by chance
            AccountAddFamilyMember.Instance.ClearNameList();
            AccountAddFamilyMember.Instance.Clear();
            Registration.Instance.Email = string.Empty;
            Registration.Instance.Password = string.Empty;
            return await Task.FromResult(true);
        }
    }

    public class ProviderLogoutService : IiOSLogout
    {
        public async Task<bool> Logout(IUserDialogs userDialogs)
        {
            bool x = await Task.Run(async () =>
            {
                Models.ProviderActiveVisitsResponse resp = await DataUtility.GetProviderActiveVisitsAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.ProviderID.ToString(), CommonAuthSession.Token).ConfigureAwait(false);

                if (resp != null)
                {
                    if (resp.StatusCode == Models.StatusCode.Success)
                    {
                        if (resp.ActiveVisits.Count > 0)
                        {
                            await userDialogs.AlertAsync("You can not sign out while you still have active visits!");
                        }
                        else
                        {
                            Models.StatusResponse status = await DataUtility.LogoutAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.ProviderID.ToString(), CommonAuthSession.Token).ConfigureAwait(false);

                            if (status != null)
                            {
                                if (!string.IsNullOrEmpty(status.ErrorMessage))
                                {
                                    await userDialogs.AlertAsync(status.ErrorMessage);
                                }
                                else
                                {
                                    if (status.StatusCode == Models.StatusCode.Success)
                                    {
                                        CommonAuthSession.ClearSession();
                                        return true;
                                    }
                                    else if (status.StatusCode == Models.StatusCode.Payload)
                                    {
                                        await userDialogs.AlertAsync(status.Payload);
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(status.Message))
                                        {
                                            await userDialogs.AlertAsync(status.Message);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                await userDialogs.AlertAsync("An error occurred!");
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(resp.Message)) await userDialogs.AlertAsync(resp.Message);
                    }
                }
                else
                {
                    await userDialogs.AlertAsync("An error occurred!");
                }

                return false;
            });

            return x;
        }
    }
}

