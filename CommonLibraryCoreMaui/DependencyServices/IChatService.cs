//using System.Threading.Tasks;
//using FM.WebSync;

//namespace CommonLibraryCoreMaui.DependencyServices
//{

////WebSync Cloud                           → LiveSwitch Cloud
////https://v4.websync.fm/websync.ashx	  https://cloud.liveswitch.io
////Client.RequestUrl                       Client.GatewayUrl
////Client.DomainName                       Client.ApplicationId
////Client.DomainKey                        Client.SharedSecret
////Client.Connect                          Client.Register
////Client.Disconnect                       Client.Unregister
////Client.Bind                             Client.Update
////Client.Unbind                           Client.Update
////Client.Subscribe                        Client.Join
////Client.Unsubscribe                      Client.Leave
////Client.Publish                          Channel.SendMessage
////SubscribeArgs.OnReceive                 Channel.OnMessage
////SubscribeArgs.OnClientSubscribe         Channel.OnRemoteClientJoin
////SubscribeArgs.OnUserJoin                Channel.OnRemoteClientJoin
////SubscribeArgs.OnClientUnsubscribe       Channel.OnRemoteClientLeave
////SubscribeArgs.OnUserLeave               Channel.OnRemoteClientLeave
//    public interface IChatCommunication
//    {
//        bool isConnected();
//        Task<bool> ConnectAsync(ConnectArgs connectArgs);
//        Task<bool> PublishAsync(PublishArgs publishArgs);
//        bool Publish(PublishArgs publishArgs);
//        Task<bool> SubscribeAsync(SubscribeArgs subscribeArgs);
//    }  
//}
