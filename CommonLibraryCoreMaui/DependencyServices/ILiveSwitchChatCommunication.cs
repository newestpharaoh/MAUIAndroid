using FM.LiveSwitch;
using System.Threading.Tasks;

public interface ILiveSwitchChatCommunication
{
    Client Client { get; set; }
    //Task<bool> RegisterAsync();
    //Task<bool> PublishAsync(string token);
    //bool SendMessage();
    Future<object> JoinAsync(
        Action2<string, string> IncomingMessage,
        Action1<string> PeerLeft,
        Action1<string> PeerJoined,
        Action0 ClientRegistered);

    Task<string> GetAsyncToken(string wsUserID, string wsDeviceID, string wsID, string channerlName, Client Client);
}