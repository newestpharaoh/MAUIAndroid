namespace CommonLibraryCoreMaui
{
    public interface ISessionExpired
    {
        void IsSessionExpired();
    }

    public interface ILogout
    {
        void Logout();
    }

    public interface IiOSLogout
    {
        System.Threading.Tasks.Task<bool> Logout(Acr.UserDialogs.IUserDialogs _userDialogs = null);
    }

    public interface INavigateToLoginPage
    {
        void NavigateToLoginPage();
    }

    public interface IReconnectToChat
    {
        void ConnectToChat(bool isConnected);
    }
}
