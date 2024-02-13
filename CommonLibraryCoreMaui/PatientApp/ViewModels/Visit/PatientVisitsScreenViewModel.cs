using Acr.UserDialogs;
using CommonLibraryCoreMaui.DependencyServices;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using FM.LiveSwitch;
using Microsoft.Maui.Storage;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientVisitsScreenViewModel : BaseNavigationViewModel<VisitDetailNavigationParam>
    {
        public IVisitsService _visitsService;
      //  public IChatCommunication _chatService;

        RefershTimer providerAvailabilityCheckTimer;

        #region properties
        private string _visitID;
        public string VisitID
        {
            get { return _visitID; }
            set { SetProperty(ref _visitID, value); }
        }

        private string _providerName;
        public string ProviderName
        {
            get { return _providerName; }
            set { SetProperty(ref _providerName, value); }
        }

        private string _providerID;
        public string ProviderID
        {
            get { return _providerID; }
            set { SetProperty(ref _providerID, value); }
        }

        private string _patientFirstName;
        public string PatientFirstName
        {
            get { return _patientFirstName; }
            set { SetProperty(ref _patientFirstName, value); }
        }

        private string _patientLastName;
        public string PatientLastName
        {
            get { return _patientLastName; }
            set { SetProperty(ref _patientLastName, value); }
        }

        private bool _isVisibleChatbox;
        public bool IsVisbibleChatbox
        {
            get { return _isVisibleChatbox; }
            set { SetProperty(ref _isVisibleChatbox, value); }
        }

        private bool _isVisibleVideoChatDialog;
        public bool IsVisibleVideoChatDialog
        {
            get { return _isVisibleVideoChatDialog; }
            set { SetProperty(ref _isVisibleVideoChatDialog, value); }
        }

        private bool _isVisibleVideoChat;
        public bool IsVisibleVideoChat
        {
            get { return _isVisibleVideoChat; }
            set { SetProperty(ref _isVisibleVideoChat, value); }
        }

        private bool _isVisibleVoiceChat;
        public bool IsVisibleVoiceChat
        {
            get { return _isVisibleVoiceChat; }
            set { SetProperty(ref _isVisibleVoiceChat, value); }
        }

        public bool AudioOnly
        { get; set; }

        public bool ReceiveOnly
        { get; set; }

        public bool EnableScreenShare
        { get; set; }
        public bool EnableAudioSend { get; set; }
        public bool EnableAudioReceive { get; set; }
        public bool EnableVideoSend { get; set; }
        public bool EnableVideoReceive { get; set; }

        #endregion
        public string CurrentChatURL => SettingsValues.ChatServerUrlValue + $"LSPatientChatBox.aspx?isMobile=true&isiOS=true&token={CommonAuthSession.Token}&providerID={ProviderID}&visitID={VisitID}&patientFirstName={Uri.EscapeDataString(PatientFirstName)}&patientLastName={Uri.EscapeDataString(PatientLastName)}";

        public bool DisableActionBar = false;
        public bool ReloadWebView = false;
        public bool ScrollWebView = true;
        private bool isSubscribed = false;
        public Dictionary<string, bool> _RemoteAVMaps;
        public Dictionary<string, int[]> _RemoteEncodingMaps;
        public List<bool> sendEncodings;
        public List<string> encodings;

        public bool isRegistered = false;
        public event Action0 ClientRegistered;

        public event Action1<string> PeerJoined;
        public event Action1<string> PeerLeft;
        public int receiveEncoding = 0;


        public IMvxCommand EndVideoChatCommand => new MvxAsyncCommand(RequestEndVideoChatAsync);
        public IMvxCommand EndVoiceChatCommand => new MvxAsyncCommand(RequestEndVoiceChatAsync);
        public IMvxCommand EndVisitCommand => new MvxAsyncCommand(EndVisitAsync);

        public MvxInteraction NavigateToSelectProviderView { get; } = new MvxInteraction();

        public event EventHandler<MessageReceivedArgs> MessageReceived;
        public FM.LiveSwitch.Client WSClient;
        private int _ReregisterBackoff = 200;
        private const int _MaxRegisterBackoff = 60000;
        private bool Unregistering = false;
        public string RegToken;
        string wsUserID;
        string wsDeviceID;
        string wsID;
        string ZeroChannelName;
        Channel WSChannelZero;
        public Channel[] channels;
        string VisitChannelName;
        string ReasonForVisit;
        public Channel VisitChannel;
        public string AppID { get; set; }
        string VisitJoinToken;
        public bool IsLiveSwitch = false;

        public string PatientUsername;// = patientFirstName + ' ' + patientLastName.substring(0, 1);		// Set patient username 


        public Modes Mode { get; set; }
        public enum Modes
        {
            Peer = 3
        };

        // View methods
        public PatientVisitsScreenViewModel(IMvxNavigationService mvxNavigationService, IUserDialogs userDialogs, IVisitsService visitsService)
        {
            _navigationService = mvxNavigationService;
            _userDialogs = userDialogs;
            _visitsService = visitsService;

            providerAvailabilityCheckTimer = new RefershTimer(TimeSpan.FromMilliseconds(SettingsValues.PollVisitStatusPeriod), async () =>
        {
            if (StartVisit.Instance.PatientID != null && (int)StartVisit.Instance.ProviderID != 0)
            {
                IProviderStatus ips = ProviderStatusFactory.Get(StartVisit.Instance.VisitID, (int)StartVisit.Instance.PatientID, (int)StartVisit.Instance.ProviderID);
             
                if (!StartVisit.Instance.IsResumeVisit.GetValueOrDefault())
                {
                    if (await ips.IsProviderNotAvailable().ConfigureAwait(false))
                    {
                        providerAvailabilityCheckTimer.Stop();
                        NavigateToSelectProviderView.Raise();
                    }
                }
                if (StartVisit.Instance != null)
                {
                    if (StartVisit.Instance.VisitID != null)
                        await _visitsService.UpdateLastActiveVisitTime(StartVisit.Instance.VisitID.Value).ConfigureAwait(false);
                }
            }
        });
            ChatVersion();
          //  if (!IsLiveSwitch) _chatService = DependencyService.Get<IChatCommunication>();
        }

        public void ChatVersion()
        {
            var chtV = GetChatVersion().Result;
            IsLiveSwitch = chtV == "WebSync" ? false : true;
        }
        private async Task<string> GetChatVersion()
        {
            return await DataUtility.GetChatVersionResponseAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token);
        }

        private async Task<string> GetLSAppID()
        {
            var Token = CommonAuthSession.Token;
            return await DataUtility.GetLSAppIDAsync(SettingsValues.ApiURLValue, Token);
        }


        public async override Task Initialize()
        {
            // await _lvchatService.JoinAsync();
            await Process();
            await base.Initialize();
        }



        public override void Prepare(VisitDetailNavigationParam parameter)
        {
            VisitID = parameter?.VisitId;
            ProviderID = parameter?.ProviderId;
            ProviderName = parameter?.ProviderName;
            PatientFirstName = parameter?.PatientFirstName;
            PatientLastName = parameter?.PatientLastName;
            ReasonForVisit = parameter?.ReasonForVisit;
            base.Prepare();
        }



        public async override void ViewAppeared()
        {
            base.ViewAppeared();
            providerAvailabilityCheckTimer.Start();
            //await RaisePropertyChanged(nameof(ReloadWebView));
        }



        public override void ViewDisappeared()
        {
            providerAvailabilityCheckTimer.Stop();
            base.ViewDisappeared();
        }

        private async Task Process()
        {
            await InitChat();
        }
        public Future<object> PostMessage(FMChatMessageModel msgStartVisit)
        {
            if (msgStartVisit.ReasonForVisit != null)
            {
                var resp = Task.Run<StatusResponse>(async () => await DataUtility.PostVisitRequestMessage(SettingsValues.ApiURLValue, msgStartVisit, CommonAuthSession.Token)).Result;
            }
            else if (!string.IsNullOrEmpty(msgStartVisit.Text))
            {
                var resp = Task.Run<StatusResponse>(async () => await DataUtility.PostChatMessage(SettingsValues.ApiURLValue, msgStartVisit, CommonAuthSession.Token)).Result;
            }
            else if (msgStartVisit.SystemMessage != null)
            {
                var resp = Task.Run<StatusResponse>(async () => await DataUtility.PostVRSystemMessage(SettingsValues.ApiURLValue, msgStartVisit, CommonAuthSession.Token));
                //var task = Task.Run<string>(async () => await DataUtility.GetLiveSwitchTokenResponseAsync(SettingsValues.ApiURLValue, RegURL, CommonAuthSession.Token));
                // return resp.Result;
            }
            else { Promise<object>.RejectNow<object>(new Exception("Unknown Error type."));}

            return Promise<object>.ResolveNow<object>(null);
        }
        public Future<object> LSSendMessage(Channel channel, FMChatMessageModel msgStartVisit)
        {
            msgStartVisit.Timestamp = DateTime.Now.ToLocalTime().ToString();
            if (channel != null)
            {
                var msg = JsonConvert.SerializeObject(msgStartVisit);
                PostMessage(msgStartVisit).Fail((Exception p) => Log.Error("Ios: StoreMessage: Nope: " + p.Message + " message: " + msg));

                channel.SendMessage(msg)
                         .Fail((Exception p) =>
                         {
                             var cID = channel.Id;
                             Log.Debug(String.Format("iOS Failed sendmessage ; channelid = " + cID + " message =" + msg + "error msg" + p.Message));
                         });
            }
            return Promise<object>.ResolveNow<object>(null);
        }

        // Chat communcation
        public async Task InitChat()
        {
            bool success = false;
            try
            {

                if (IsLiveSwitch)
                {
                    PatientUsername = string.Format("{0} {1}", PatientFirstName, PatientLastName);
                    VisitChannelName = "/chat/" + ProviderID + '/' + VisitID;
                    ZeroChannelName = "/chat/" + ProviderID + "/0";
                    //ReasonForVisit
                    AppID = await GetLSAppID();
                    WSClient = new FM.LiveSwitch.Client(SettingsValues.GatewayURL, AppID);
                    WSClient.UserAlias = PatientUsername;
                    wsUserID = WSClient.UserId;
                    wsDeviceID = WSClient.DeviceId;
                    wsID = WSClient.Id;
                    RegisterWSClient();
                    success = true;
                }
                else
                {
                    //if (!_chatService.isConnected())
                    //{
                    //    var connectionArgs = new ConnectArgs
                    //    {
                    //        OnSuccess = (eConnect) =>
                    //        {
                    //            success = true; //ConnectToChat(false);
                    //        },
                    //        OnFailure = (eConnect) =>
                    //        {
                    //            string sMsg = eConnect.Exception.Message;
                    //            eConnect.Retry = true;
                    //            //UtilsUI.ShowMsgOkScreen(this, "Chat server disconnected. Msg: " + sMsg);
                    //        },
                    //        OnStreamFailure = (eConnect) =>
                    //        {
                    //            eConnect.Retry = true;
                    //            string sMsg = eConnect.GetExtensionValueJson("Message");
                    //            //UtilsUI.ShowMsgOkScreen(this, "Chat server stream disconnected. Msg: " + sMsg);
                    //        }
                    //    };

                    //    var connectResult = await _chatService.ConnectAsync(connectionArgs);
                    //}
                    //else
                    //{
                    //    success = true;// ConnectToChat(false);
                    //}
                }

                if (IsLiveSwitch || success)
                {
                    ConnectToChat(false);//
                }
                await RaisePropertyChanged(nameof(ProviderName));
            }
            catch (Exception ex)
            {
                ReportCrash(ex, Title);
            }
        }

        public async Task ReconnectToChat()
        {
            bool success = false;
            try
            {
                if (IsLiveSwitch)
                {
                    //RegisterWSClient();
                    success = true;
                }
                else
                {
                    //if (!_chatService.isConnected())
                    //{
                    //    var connectionArgs = new ConnectArgs
                    //    {
                    //        OnSuccess = (eConnect) =>
                    //        {
                    //            success = true; //ConnectToChat(true);
                    //        },
                    //        OnFailure = (eConnect) =>
                    //        {
                    //            string sMsg = eConnect.Exception.Message;
                    //            eConnect.Retry = true;
                    //            //UtilsUI.ShowMsgOkScreen(this, "Chat server disconnected. Msg: " + sMsg);
                    //        },
                    //        OnStreamFailure = (eConnect) =>
                    //        {
                    //            eConnect.Retry = true;
                    //            string sMsg = eConnect.GetExtensionValueJson("Message");
                    //            //UtilsUI.ShowMsgOkScreen(this, "Chat server stream disconnected. Msg: " + sMsg);
                    //        }
                    //    };

                    //    var connectResult = await _chatService.ConnectAsync(connectionArgs);
                    //}
                }

                if (success) ConnectToChat(true);
            }
            catch (Exception ex)
            {
                ReportCrash(ex, Title);
            }
        }

        private void ConnectToChat(bool reconnect)
        {
            try
            {
                FMChatMessageModel msgStartVisit = new FMChatMessageModel
                {
                    Token = CommonAuthSession.Token,
                    FirstName = CommonAuthSession.FirstName,
                    LastName = CommonAuthSession.LastName,
                    Username = "Notice",
                    Text = "",
                    MessageType = "patient_visit_start"
                };

                if (IsLiveSwitch)
                {
                    JoinAsync(false);
                }
                else
                {

                    //var publishResponse = _chatService.PublishAsync(new PublishArgs("/chat/" + ProviderID + "/" + VisitID, JsonConvert.SerializeObject(msgStartVisit))
                    //{
                    //    OnSuccess = (ePublish) =>
                    //    {
                    //        FMChatMessageModel msgSubscribe = new FMChatMessageModel();
                    //        msgSubscribe.Token = CommonAuthSession.Token;
                    //        msgSubscribe.Username = ProviderID;
                    //        msgSubscribe.ProviderID = ProviderID;

                    //        var subscribeResponse = _chatService.SubscribeAsync(new SubscribeArgs("/chat/" + ProviderID + "/" + VisitID, JsonConvert.SerializeObject(msgSubscribe))
                    //        {

                    //            OnSuccess = (eSubscribe) =>
                    //            {
                    //                if (!reconnect)
                    //                {
                    //                    string sMsg = eSubscribe.GetExtensionValueJson("Message");

                    //                    // send a request to the physician's visits queue listener
                    //                    FMChatMessageModel msgVisitRequest = new FMChatMessageModel();
                    //                    msgSubscribe.Token = CommonAuthSession.Token;
                    //                    msgVisitRequest.MessageType = "visit_request";
                    //                    msgVisitRequest.Text = "";
                    //                    msgStartVisit.FirstName = CommonAuthSession.FirstName;
                    //                    msgStartVisit.LastName = CommonAuthSession.LastName;
                    //                    msgVisitRequest.ProviderID = ProviderID;
                    //                    msgVisitRequest.VisitID = VisitID;

                    //                    var publishResponse2 = _chatService.PublishAsync(new PublishArgs("/chat/" + ProviderID + "/0", JsonConvert.SerializeObject(msgVisitRequest))
                    //                    {
                    //                        OnSuccess = (ePublish2) =>
                    //                        {
                    //                            UpdateVisitID();
                    //                        },
                    //                        OnFailure = (ePublish2) =>
                    //                        {
                    //                            string pMsg = ePublish2.Exception.Message;
                    //                            //UtilsUI.ShowMsgOkScreen(this, "Failed to send visit request. " + sMsg);
                    //                        }
                    //                    });


                    //                    // Record how the patient is connection to the transcript
                    //                    string appVersion = Preferences.Get("iOSPatientAppVersion", string.Empty);
                    //                    FMChatMessageModel msgEMDInit = new FMChatMessageModel();
                    //                    msgEMDInit.Token = CommonAuthSession.Token;
                    //                    msgEMDInit.Username = ProviderID.ToString();
                    //                    msgEMDInit.SystemMessage = "iOS Patient App";
                    //                    msgEMDInit.VisitID = VisitID;
                    //                    msgEMDInit.MessageType = "system_message";

                    //                    if (!string.IsNullOrEmpty(appVersion))
                    //                    {
                    //                        msgEMDInit.SystemMessage += ": Version " + appVersion;
                    //                    }

                    //                    var publishResponse3 = _chatService.PublishAsync(new PublishArgs("/chat/" + ProviderID.ToString() + "/0", JsonConvert.SerializeObject(msgEMDInit))
                    //                    {
                    //                        OnSuccess = (ePublish3) =>
                    //                        {
                    //                            string pMsg = ePublish3.ExtensionNames[0];
                    //                        },
                    //                        OnFailure = (ePublish3) =>
                    //                        {
                    //                            string pMsg = ePublish3.Exception.Message;
                    //                        }
                    //                    });
                    //                }
                    //            },
                    //            OnFailure = (eSubscribe) =>
                    //            {
                    //                string sMsg = eSubscribe.Exception.Message;
                    //                eSubscribe.Retry = true;
                    //                //UtilsUI.ShowMsgOkScreen(this, "Chat server visit queue disconnected. Msg: " + sMsg);
                    //            },
                    //            OnReceive = async (eSubscribe) =>
                    //            {
                    //                FMChatMessageModel msgVisitQueueMessage = JsonConvert.DeserializeObject<FMChatMessageModel>(eSubscribe.DataJson);

                    //                if (string.IsNullOrEmpty(ProviderName))
                    //                {
                    //                    ProviderName = msgVisitQueueMessage.FirstName + " " + msgVisitQueueMessage.LastName;
                    //                    UpdateProviderName();
                    //                }

                    //                if (msgVisitQueueMessage.MessageType.Equals("end_visit_confirmation"))
                    //                {
                    //                    // Confirm end visit
                    //                    GoToPostVisitViewAsync(msgVisitQueueMessage.VisitID);
                    //                }
                    //                else if (msgVisitQueueMessage.MessageType.Equals("videochat_request"))
                    //                {
                    //                    // Confirm video visit
                    //                    await StartVideoConfirmAsync();
                    //                }
                    //                else if (msgVisitQueueMessage.MessageType.Equals("voicechat_request"))
                    //                {
                    //                    // Confirm audio visit
                    //                    await StartVoiceConfirmAsync();
                    //                }
                    //                else if (msgVisitQueueMessage.MessageType.Equals("end_videochat_confirmation"))
                    //                {
                    //                    // Confirm end video visit
                    //                    EndVideoChat();
                    //                }
                    //                else if (msgVisitQueueMessage.MessageType.Equals("end_voicechat_confirmation"))
                    //                {
                    //                    // Confirm end audio visit
                    //                    EndVoiceChat();
                    //                }
                    //                else if (msgVisitQueueMessage.MessageType.Equals("reload_webview"))
                    //                {
                    //                    CallReloadWebView();
                    //                }
                    //                else if (msgVisitQueueMessage.MessageType.Equals("patient_msg"))
                    //                {
                    //                    CallScrollWebView();
                    //                }
                    //            }
                    //        });
                    //    },
                    //    OnFailure = (ePublish) =>
                    //    {
                    //        string sMsg = ePublish.Exception.Message;
                    //        ePublish.Retry = true;
                    //    }
                    //});
                }
            }
            catch (Exception ex)
            {
                ReportCrash(ex, Title);
            }
        }

        public Future<object> RegisterWSClient()
        {
            var RegURL = "/GetLSRegisterToken?wsUserID=" + wsUserID + "&wsDeviceID=" + wsDeviceID + "&wsID=" + wsID + "&channelName=" + ZeroChannelName;

            var token = DataUtility.GetLiveSwitchTokenResponseAsync(SettingsValues.ApiURLValue, RegURL, CommonAuthSession.Token).Result;
            RegToken = token;
            WSClient.OnStateChange += (client) =>
            {

                //if (client.State == ClientState.Registering) { Log.Debug($"client is registering"); }
                //else if (client.State == ClientState.Registered) { Log.Debug($"client is registered"); }
                if (client.State == ClientState.Unregistering && !Unregistering) { Log.Debug($"client is unregistering IOS -- showing lost connection dialog"); }
                else if (client.State == ClientState.Unregistered && !Unregistering)
                {
                    Log.Debug($"Registering with backoff = {_ReregisterBackoff}.");
                    // Incrementally increase register backoff to prevent runaway process
                    Thread.Sleep(_ReregisterBackoff);
                    if (_ReregisterBackoff <= _MaxRegisterBackoff)
                        _ReregisterBackoff += _ReregisterBackoff;

                    Log.Debug($"Client unregistered unexpectedly, trying to re-register.");
                    token = DataUtility.GetLiveSwitchTokenResponseAsync(SettingsValues.ApiURLValue, RegURL, CommonAuthSession.Token).Result; // ensure token has not expired
                                                                                                                                             //token = _lschatService.GetAsyncToken(wsUserID, wsDeviceID, wsID, ZeroChannelName, WSClient).Result;
                    RegToken = token;
                    WSClient.Register(token)
                         .Then<object>(channels =>
                         {

                             _ReregisterBackoff = 200; // reset for next time
                             return OnClientRegistered(channels, true, VisitChannelName, VisitID, PatientFirstName, PatientLastName, ProviderID, "en",
                                 CommonAuthSession.Token, ReasonForVisit);
                         })
                         .Fail((e) =>
                         {
                             Log.Error("Failed to register with Gateway. " + e.Message);

                         });
                }
            };

            WSClient.Register(token)
            .Then<object>(channels =>
            {
                return OnClientRegistered(channels, false, VisitChannelName, VisitID, PatientFirstName, PatientLastName, ProviderID, "en",
                                 CommonAuthSession.Token, ReasonForVisit);
            })
            .Fail((e) =>
            {
                Log.Error("Failed to register with Gateway.", e);
            });
            return Promise<object>.ResolveNow<object>(null);
        }
        public Future<object> OnClientRegistered(Channel[] channels, bool reconnect, string VisitChannelname, string VisitId, string FirstNamr, string LastName,
            string ProviderID, string LocaleId, string token, string reasonForvisit)
        {
            this.channels = channels;
            WSChannelZero = channels[0];
            WSChannelZero.OnRemoteClientJoin += (remoteClientInfo) =>
             {
                 Log.Info(string.Format("Remote client joined the channel (client ID: {0}, client tag: {1}, client roles: {2}, device ID: {3}, device alias: {4}, user ID: {5}, user tag: {6}).", remoteClientInfo.Id, remoteClientInfo.Tag, string.Join(" & ", remoteClientInfo.Roles ?? new string[0]), remoteClientInfo.DeviceId, remoteClientInfo.DeviceAlias, remoteClientInfo.UserId, remoteClientInfo.UserAlias));

                 string n = !string.IsNullOrEmpty(remoteClientInfo.UserAlias) ? remoteClientInfo.UserAlias : remoteClientInfo.UserId;

                 // OnPeerJoined(n);

             };

            WSChannelZero.OnRemoteClientLeave += (remoteClientInfo) =>
            {
                Log.Info(string.Format("Remote client left the channel (client ID: {0}, client tag: {1}, client roles: {2}, device ID: {3}, device alias: {4}, user ID: {5}, user tag: {6}).", remoteClientInfo.Id, remoteClientInfo.Tag, string.Join(" & ", remoteClientInfo.Roles ?? new string[0]), remoteClientInfo.DeviceId, remoteClientInfo.DeviceAlias, remoteClientInfo.UserId, remoteClientInfo.UserAlias));

                string n = !string.IsNullOrEmpty(remoteClientInfo.UserAlias) ? remoteClientInfo.UserAlias : remoteClientInfo.UserId;

                //OnPeerLeft(n);
            };
            WSChannelZero.OnRemoteUpstreamConnectionUpdate += (oldRemoteConnectionInfo, remoteConnectionInfo) =>
            {
                Log.Info(string.Format("Remote client updated upstream connection (connection ID: {0}, connection tag: {1}, client ID: {2}, client tag: {3}, client roles: {4}, device ID: {5}, device alias: {6}, user ID: {7}, user tag: {8}).", remoteConnectionInfo.Id, remoteConnectionInfo.Tag, remoteConnectionInfo.ClientId, remoteConnectionInfo.ClientTag, string.Join(" & ", remoteConnectionInfo.ClientRoles ?? new string[0]), remoteConnectionInfo.DeviceId, remoteConnectionInfo.DeviceAlias, remoteConnectionInfo.UserId, remoteConnectionInfo.UserAlias));
            };

            WSChannelZero.OnRemoteUpstreamConnectionClose += (remoteConnectionInfo) =>
            {
                Log.Info(string.Format("Remote client closed upstream connection (connection ID: {0}, connection tag: {1}, client ID: {2}, client tag: {3}, client roles: {4}, device ID: {5}, device alias: {6}, user ID: {7}, user tag: {8}).", remoteConnectionInfo.Id, remoteConnectionInfo.Tag, remoteConnectionInfo.ClientId, remoteConnectionInfo.ClientTag, string.Join(" & ", remoteConnectionInfo.ClientRoles ?? new string[0]), remoteConnectionInfo.DeviceId, remoteConnectionInfo.DeviceAlias, remoteConnectionInfo.UserId, remoteConnectionInfo.UserAlias));
            };

            WSChannelZero.OnPeerConnectionOffer += (peerConnectionOffer) =>
            {
                Log.Info(string.Format("Remote client offered peer connection (connection ID: {0}, connection tag: {1}, client ID: {2}, client tag: {3}, client roles: {4}, device ID: {5}, device alias: {6}, user ID: {7}, user tag: {8}).", peerConnectionOffer.RemoteConnectionId, peerConnectionOffer.ConnectionTag, peerConnectionOffer.RemoteClientInfo.Id, peerConnectionOffer.RemoteClientInfo.Tag, string.Join(" & ", peerConnectionOffer.RemoteClientInfo.Roles ?? new string[0]), peerConnectionOffer.RemoteClientInfo.DeviceId, peerConnectionOffer.RemoteClientInfo.DeviceAlias, peerConnectionOffer.RemoteClientInfo.UserId, peerConnectionOffer.RemoteClientInfo.UserAlias));

                //OpenPeerAnswerConnection(peerConnectionOffer);
            };
            WSChannelZero.OnMessage += (clientInfo, message) =>
            {
                string name = !string.IsNullOrEmpty(clientInfo.UserAlias) ? clientInfo.UserAlias : clientInfo.UserId;

                //OnMessageReceived(new MessageReceivedArgs(name, message));
            };

            isRegistered = true;
            ClientRegistered?.Invoke();
            JoinAsync(reconnect);

            return Promise<object>.ResolveNow<object>(null);
        }

        private void OnPeerJoined(string peer)
        {
            PeerJoined?.Invoke(peer);
        }

        private void OnPeerLeft(string peer)
        {
            PeerLeft?.Invoke(peer);
        }
        public void JoinAsync(bool reconnect)
        {

            var URL = "/GetLSJoinToken?wsUserID=" + wsUserID + "&wsDeviceID=" + wsDeviceID + "&wsID=" + wsID + "&channelName=" + VisitChannelName;

            var token = DataUtility.GetLiveSwitchTokenResponseAsync(SettingsValues.ApiURLValue, URL, CommonAuthSession.Token).Result;
            VisitJoinToken = token;
            UpdateVisitID();
            var text = "";
            if (reconnect)
                text = "Patient rejoining chat";
            WSClient.Join(VisitChannelName, VisitJoinToken)
                 .Then(channel =>
                 {
                     //OnClientJoined(channel, reconnect);
                     VisitChannel = channel;
                     channel.OnMessage += ((clientInfo, msgReceived) =>
                      {
                          Log.Debug("desktop visit channel message received " + msgReceived);
                          string name = !string.IsNullOrEmpty(clientInfo.UserAlias) ? clientInfo.UserAlias : clientInfo.UserId;
                          OnMessageReceived(new MessageReceivedArgs(name, msgReceived));
                      });
                 })
                 .Then(channel =>
                     {
                         Log.Debug("send patient_visit_start");
                         FMChatMessageModel msgStartVisit = new FMChatMessageModel
                         {
                             Token = CommonAuthSession.Token,
                             FirstName = CommonAuthSession.FirstName,
                             LastName = CommonAuthSession.LastName,
                             Username = "Notice",
                             Text = text,
                             MessageType = "patient_visit_start"
                         };
                         LSSendMessage(VisitChannel, msgStartVisit).Fail(e =>
                         {
                             Log.Debug(String.Format("patient_visit_start info message fail " + e.Message));
                         });
                     })
                 .Then(channel =>
                 {
                     Log.Debug("Send visit_request to channel 0");
                     FMChatMessageModel msgVisitRequest = new FMChatMessageModel();
                     msgVisitRequest.Token = CommonAuthSession.Token;
                     msgVisitRequest.MessageType = "visit_request";
                     msgVisitRequest.Text = "";
                     msgVisitRequest.FirstName = CommonAuthSession.FirstName;
                     msgVisitRequest.LastName = CommonAuthSession.LastName;
                     msgVisitRequest.ProviderID = ProviderID;
                     msgVisitRequest.VisitID = VisitID;

                     LSSendMessage(VisitChannel, msgVisitRequest).Fail(e =>
                     {
                         Log.Debug(String.Format("visit_request info message fail " + e.Message));
                     });
                 })
                 .Then(Channel =>
                 {
                     Log.Debug("send browser info to channel 0");
                     string appVersion = Preferences.Get("iOSPatientAppVersion", string.Empty);
                     FMChatMessageModel msgEMDInit = new FMChatMessageModel();
                     msgEMDInit.Token = CommonAuthSession.Token;
                     msgEMDInit.Username = ProviderID.ToString();
                     msgEMDInit.SystemMessage = "iOS Patient App";
                     msgEMDInit.VisitID = VisitID;
                     msgEMDInit.MessageType = "system_message";

                     if (!string.IsNullOrEmpty(appVersion))
                     {
                         msgEMDInit.SystemMessage += ": Version " + appVersion;
                     }
                     LSSendMessage(VisitChannel, msgEMDInit).Fail(e =>
                     {
                         Log.Debug(String.Format("ios send browser info to channel 0 message fail " + e.Message));
                     });
                 })
             .Then(Channel =>
             {
                 //if (!reconnect)
                 //{
                 FMChatMessageModel msgEMDInit = new FMChatMessageModel();
                 msgEMDInit.Token = CommonAuthSession.Token;
                 msgEMDInit.FirstName = CommonAuthSession.FirstName;
                 msgEMDInit.LastName = CommonAuthSession.LastName;
                 msgEMDInit.Text = "";
                 msgEMDInit.Username = PatientUsername;
                 msgEMDInit.VisitID = VisitID;
                 msgEMDInit.MessageType = "force_end_audio_video";
                 LSSendMessage(VisitChannel, msgEMDInit).Fail(e =>
                 {
                     Log.Debug(String.Format("force_end_audio_video message fail" + e.Message));
                 });
                 // }
             })
             .Then((Channel =>
             {
                 Log.Debug("visit in queue");
             }))
             .Fail(ex =>
             {
                 Log.Error("IOS JoinAsync failed to join channel " + VisitChannelName + " message: " + ex.Message);
             });
        }

        private void OnMessageReceived(MessageReceivedArgs args)
        {
            var name = args.Name;
            var msgVisitQueueMessage = JsonConvert.DeserializeObject<FMChatMessageModel>(args.Message); ;// JsonConvert.SerializeObject(args.Message);

            if (string.IsNullOrEmpty(ProviderName))
            {
                ProviderName = msgVisitQueueMessage.FirstName + " " + msgVisitQueueMessage.LastName;
                UpdateProviderName();
            }                                                                                            // var m = message.MessageType == "indicator_typing";
            if (msgVisitQueueMessage.MessageType.Equals("videochat_request"))
            {
                // Confirm video visit
                AudioOnly = false;
                MessageReceived?.Invoke(this, args);
                StartVideoConfirmAsync().ConfigureAwait(false);
            }
            else if (msgVisitQueueMessage.MessageType.Equals("voicechat_request"))
            {
                // Confirm audio visit
                AudioOnly = true;
                StartVoiceConfirmAsync().ConfigureAwait(false);
            }
            else if (msgVisitQueueMessage.MessageType.Equals("videochat_request_declined"))
            {
                SendVideochatrequestdeclined();
            }
            else if (msgVisitQueueMessage.MessageType.Equals("end_videochat_confirmation"))
            {
                // Confirm end video visit
                EndVideoChat();
            }
            else if (msgVisitQueueMessage.MessageType.Equals("end_voicechat_confirmation"))
            {
                // Confirm end audio visit
                EndVoiceChat();
            }
            else if (msgVisitQueueMessage.MessageType.Equals("reload_webview"))
            {
                CallReloadWebView();
            }
            else if (msgVisitQueueMessage.MessageType.Equals("patient_msg"))
            {
                CallScrollWebView();
            }
          
        }
        //TODO: SC this method UILongPressGestureRecognizer
        //private void LongPressRemote(UILongPressGestureRecognizer sender)
        //{
        //    string id = sender.View.AccessibilityIdentifier;
        //    // update receiveEncoding for the current selected remote session
        //    foreach (KeyValuePair<string, int[]> entry in _RemoteEncodingMaps)
        //    {
        //        if (entry.Key.Contains(id))
        //        {
        //            if (entry.Value[1] == 1)
        //            {
        //                receiveEncoding = entry.Value[0];
        //            }
        //        }
        //    }
        //    if (sender.State == UIGestureRecognizerState.Began)
        //    {
        //        //  ssvideoViewController.ShowRemoteContextMenu(id);
        //    }
        //}
        private async void UpdateVisitID()
        {
            await RaisePropertyChanged(nameof(VisitID));
        }


        private async void UpdateProviderName()
        {
            await RaisePropertyChanged(nameof(ProviderName));
        }



        private async Task CallReloadWebView()
        {
            await RaisePropertyChanged(nameof(ReloadWebView));
        }



        private async Task CallScrollWebView()
        {
            await Task.Delay(1000);
            await RaisePropertyChanged(nameof(ScrollWebView));
        }
        // Action Bar methods
        private async Task EndVisitAsync()
        {
            bool success = false;
            try
            {
                if (IsLiveSwitch)
                {
                    // RegisterWSClient();
                    success = true;
                }
                else
                {
                    //if (!_chatService.isConnected())
                    //{
                    //    var connectionArgs = new ConnectArgs
                    //    {
                    //        OnSuccess = (eConnect) =>
                    //        {
                    //            ConnectToChat(true);
                    //            success = true; //sendEndVisitRequest();
                    //        },
                    //        OnFailure = (eConnect) =>
                    //        {
                    //            string sMsg = eConnect.Exception.Message;
                    //            eConnect.Retry = false;
                    //            //UtilsUI.ShowMsgOkScreen(this, "Chat server disconnected. Msg: " + sMsg);
                    //        },
                    //        OnStreamFailure = (eConnect) =>
                    //        {
                    //            eConnect.Retry = true;
                    //            string sMsg = eConnect.GetExtensionValueJson("Message");
                    //            //UtilsUI.ShowMsgOkScreen(this, "Chat server stream disconnected. Msg: " + sMsg);
                    //        }
                    //    };

                    //    var connectResult = await _chatService.ConnectAsync(connectionArgs);
                    //}
                    //else
                    //{
                    //    success = true;// sendEndVisitRequest();
                    //}
                }
                if (success || IsLiveSwitch) sendEndVisitRequest();
                await Task.Delay(10);
            }
            catch (Exception ex)
            {
                ReportCrash(ex, Title);
            }
        }

        private void SendVideochatrequestdeclined()
        {
            FMChatMessageModel msgJoinVisit1 = new FMChatMessageModel();
            msgJoinVisit1.Token = CommonAuthSession.Token;
            msgJoinVisit1.MessageType = "videochat_request_declined";
            msgJoinVisit1.Text = "";
            msgJoinVisit1.Username = ProviderID;
            msgJoinVisit1.FirstName = CommonAuthSession.FirstName;
            msgJoinVisit1.LastName = CommonAuthSession.LastName;
            msgJoinVisit1.ProviderID = ProviderID;
            msgJoinVisit1.VisitID = VisitID.ToString();

            if (IsLiveSwitch)
            {
                LSSendMessage(VisitChannel, msgJoinVisit1).Fail(e =>
                {
                    Log.Debug(String.Format("videochat_request_declined info message fail " + e.Message));
                });
            }

        }
        private async void sendEndVisitRequest()
        {
            FMChatMessageModel msgJoinVisit1 = new FMChatMessageModel();
            msgJoinVisit1.Token = CommonAuthSession.Token;
            msgJoinVisit1.MessageType = "show_confirm_end_visit_request";
            msgJoinVisit1.Text = "";
            msgJoinVisit1.Username = ProviderID;
            msgJoinVisit1.FirstName = CommonAuthSession.FirstName;
            msgJoinVisit1.LastName = CommonAuthSession.LastName;
            msgJoinVisit1.ProviderID = ProviderID;
            msgJoinVisit1.VisitID = VisitID.ToString();

            if (IsLiveSwitch)
            {
                LSSendMessage(VisitChannel, msgJoinVisit1).Fail(e =>
                {
                    Log.Debug(String.Format("show_confirm_end_visit_request info message fail " + e.Message));
                });
            }
            else
            {
                //var publishArgs = new PublishArgs("/chat/" + ProviderID + "/" + VisitID, JsonConvert.SerializeObject(msgJoinVisit1))
                //{
                //    OnSuccess = (ePublish) =>
                //    {
                //    },
                //    OnFailure = (ePublish) =>
                //    {
                //        string sMsg = ePublish.Exception.Message;
                //    }
                //};

                //var publishResult = await _chatService.PublishAsync(publishArgs);
            }
        }
        public void GoToPostVisitViewAsync(string endVisitID)
        {
            if (!isSubscribed)
            {
                isSubscribed = true;
                _navigationService.Navigate<PatientPostVisitSurveyViewModel, VisitDetailNavigationParam>(new VisitDetailNavigationParam()
                {
                    VisitId = !string.IsNullOrEmpty(endVisitID) ? endVisitID : VisitID
                });
            }
        }

        // Video methods
        private async Task RequestEndVideoChatAsync()
        {
            var confirm = await UserDialogs.Instance.ConfirmAsync("Do you wish to end the video visit?", "Video Visit Request", "Yes", "No");
            bool success = false;
            if (confirm)
            {
                FMChatMessageModel msgJoinVisit1 = new FMChatMessageModel();
                msgJoinVisit1.Token = CommonAuthSession.Token;
                msgJoinVisit1.MessageType = "end_videochat_confirmation";
                msgJoinVisit1.Text = "";
                msgJoinVisit1.Username = ProviderID;
                msgJoinVisit1.FirstName = CommonAuthSession.FirstName;
                msgJoinVisit1.LastName = CommonAuthSession.LastName;
                msgJoinVisit1.ProviderID = ProviderID;
                msgJoinVisit1.VisitID = VisitID.ToString();

                if (IsLiveSwitch)
                {
                    LSSendMessage(VisitChannel, msgJoinVisit1).Then((o) =>
                    {
                        success = true;
                        Log.Debug(String.Format("end_videochat_confirmation info message success "));
                    }).Fail(e =>
                    {
                        Log.Debug(String.Format("end_videochat_confirmation info message fail " + e.Message));
                    });
                    //EndVideoChat();
                    //sendEndVideoVisitRequestLogEntry();
                }
                else
                {
                    //var publishArgs = new PublishArgs("/chat/" + ProviderID + "/" + VisitID, JsonConvert.SerializeObject(msgJoinVisit1))
                    //{
                    //    OnSuccess = (ePublish) =>
                    //    {
                    //        success = true;
                    //    },
                    //    OnFailure = (ePublish) =>
                    //    {
                    //        string sMsg = ePublish.Exception.Message;
                    //    }
                    //};

                    //var publishResult = await _chatService.PublishAsync(publishArgs);
                }
                if (success)
                {
                    EndVideoChat();
                    sendEndVideoVisitRequestLogEntry();
                }
            }
        }

        private void sendEndVideoVisitRequestLogEntry()
        {
            FMChatMessageModel msgJoinVisit1 = new FMChatMessageModel();
            msgJoinVisit1.Token = CommonAuthSession.Token;
            msgJoinVisit1.MessageType = "end_videochat_patient_confirmation_write_time";
            msgJoinVisit1.Text = "";
            msgJoinVisit1.Username = ProviderID;
            msgJoinVisit1.FirstName = CommonAuthSession.FirstName;
            msgJoinVisit1.LastName = CommonAuthSession.LastName;
            msgJoinVisit1.ProviderID = ProviderID;
            msgJoinVisit1.VisitID = VisitID.ToString();

            if (IsLiveSwitch)
            {
                LSSendMessage(VisitChannel, msgJoinVisit1).Fail(e =>
                {
                    Log.Debug(String.Format("end_videochat_patient_confirmation_write_time info message fail " + e.Message));
                });
            }
            else
            {
                //var publishArgs = new PublishArgs("/chat/" + ProviderID + "/" + VisitID, JsonConvert.SerializeObject(msgJoinVisit1))
                //{
                //    OnSuccess = (ePublish) =>
                //    {
                //    },
                //    OnFailure = (ePublish) =>
                //    {
                //        string sMsg = ePublish.Exception.Message;
                //    }
                //};


                //var publishResult = _chatService.PublishAsync(publishArgs);
            }
        }

        private void EndVideoChat()
        {
            if (IsVisibleVideoChat)
            {
                IsVisibleVideoChat = false;
            }

            DisableActionBar = false;
            UpdateVideoChatView();
        }

        private async Task StartVideoConfirmAsync()
        {
            try
            {
                var confirm = await UserDialogs.Instance.ConfirmAsync(ProviderName + " has requested a video visit.  Accept?", "Video Visit Request", "Yes", "No");

                if (confirm)
                {
                    IsVisibleVideoChat = true;
                    DisableActionBar = true;
                    sendVideoChatConfirmation();
                }
                else { SendVideochatrequestdeclined(); }
            }
            catch (Exception ex)
            {
                ReportCrash(ex, Title);
            }
        }   

        private async void UpdateVideoChatView()
        {
            await RaisePropertyChanged(nameof(DisableActionBar));
            await RaisePropertyChanged(nameof(IsVisibleVideoChat));
        }

        private void sendVideoChatConfirmation()
        {
            FMChatMessageModel msgJoinVisit = new FMChatMessageModel();
            msgJoinVisit.Token = CommonAuthSession.Token;
            msgJoinVisit.MessageType = "videochat_request_confirmation";
            msgJoinVisit.Text = "";
            msgJoinVisit.Username = ProviderID;
            msgJoinVisit.FirstName = CommonAuthSession.FirstName;
            msgJoinVisit.LastName = CommonAuthSession.LastName;
            msgJoinVisit.ProviderID = ProviderID;
            msgJoinVisit.VisitID = VisitID;
            if (IsLiveSwitch)
            {
                LSSendMessage(VisitChannel, msgJoinVisit).Fail(e =>
                {
                    Log.Debug(String.Format("videochat_request_confirmation info message fail " + e.Message));
                });
            }
            //else
            //{
            //    var publishResponse = _chatService.PublishAsync(new PublishArgs("/chat/" + ProviderID + "/" + VisitID, JsonConvert.SerializeObject(msgJoinVisit))
            //    {
            //        OnSuccess = (ePublish) =>
            //        {
            //            string pMsg = ePublish.ExtensionNames[0];
            //        },
            //        OnFailure = (ePublish) =>
            //        {
            //            string pMsg = ePublish.Exception.Message;
            //        }
            //    });
            //}
        }
        // Audio methods
        private async Task RequestEndVoiceChatAsync()
        {
            bool success;
            var confirm = await UserDialogs.Instance.ConfirmAsync("Do you wish to end the audio visit?", "Audio Visit Request", "Yes", "No");

            if (confirm)
            {
                FMChatMessageModel msgJoinVisit1 = new FMChatMessageModel();
                msgJoinVisit1.Token = CommonAuthSession.Token;
                msgJoinVisit1.MessageType = "end_voicechat_confirmation";
                msgJoinVisit1.Text = "";
                msgJoinVisit1.Username = ProviderID;
                msgJoinVisit1.FirstName = CommonAuthSession.FirstName;
                msgJoinVisit1.LastName = CommonAuthSession.LastName;
                msgJoinVisit1.ProviderID = ProviderID;
                msgJoinVisit1.VisitID = VisitID.ToString();
                success = false;
                if (IsLiveSwitch)
                {
                    LSSendMessage(VisitChannel, msgJoinVisit1).Then((o) =>
                    {
                        Log.Debug(String.Format("end_voicechat_confirmation info message success "));
                        success = true;
                        //EndVoiceChat();
                        //sendEndVoiceVisitRequestLogEntry();
                    }).Fail(e =>
                    {
                        Log.Debug(String.Format("end_voicechat_confirmation info message fail " + e.Message));
                    });
                }
                //else
                //{
                //    var publishArgs = new PublishArgs("/chat/" + ProviderID + "/" + VisitID, JsonConvert.SerializeObject(msgJoinVisit1))
                //    {
                //        OnSuccess = (ePublish) =>
                //        {
                //            success = true;
                //            //EndVoiceChat();
                //            //sendEndVoiceVisitRequestLogEntry();
                //        },
                //        OnFailure = (ePublish) =>
                //        {
                //            string sMsg = ePublish.Exception.Message;
                //        }
                //    };


                //    var publishResult = await _chatService.PublishAsync(publishArgs);
                //}
                if (success)
                {
                    EndVoiceChat();
                    sendEndVoiceVisitRequestLogEntry();
                }
            }
        }



        private void sendEndVoiceVisitRequestLogEntry()
        {
            FMChatMessageModel msgJoinVisit1 = new FMChatMessageModel();
            msgJoinVisit1.Token = CommonAuthSession.Token;
            msgJoinVisit1.MessageType = "end_voicechat_patient_confirmation_write_time";
            msgJoinVisit1.Text = "";
            msgJoinVisit1.Username = ProviderID;
            msgJoinVisit1.FirstName = CommonAuthSession.FirstName;
            msgJoinVisit1.LastName = CommonAuthSession.LastName;
            msgJoinVisit1.ProviderID = ProviderID;
            msgJoinVisit1.VisitID = VisitID.ToString();

            if (IsLiveSwitch)
            {
                LSSendMessage(VisitChannel, msgJoinVisit1).Fail(e =>
                {
                    Log.Debug(String.Format("end_voicechat_patient_confirmation_write_time info message fail " + e.Message));
                });
            }
            //else
            //{
            //    var publishArgs = new PublishArgs("/chat/" + ProviderID + "/" + VisitID, JsonConvert.SerializeObject(msgJoinVisit1))
            //    {
            //        OnSuccess = (ePublish) =>
            //        {
            //        },
            //        OnFailure = (ePublish) =>
            //        {
            //            string sMsg = ePublish.Exception.Message;
            //        }
            //    };


            //    var publishResult = _chatService.PublishAsync(publishArgs);
            //}
        }





        private async Task StartVoiceConfirmAsync()
        {
            var confirm = await UserDialogs.Instance.ConfirmAsync(ProviderName + " has requested a voice visit.  Accept?", "Voice Visit Request", "Yes", "No");

            if (confirm)
            {
                IsVisibleVoiceChat = true;
                DisableActionBar = true;
                sendVoiceChatConfirmation();
                //StartVoiceChat();
            }
        }



        private void StartVoiceChat()
        {
            try
            {
                if (!IsVisibleVoiceChat)
                {
                    IsVisibleVoiceChat = true;
                }

                DisableActionBar = true;
                UpdateVoiceChatView();
            }
            catch (Exception e)
            {
                string sMsg = e.Message;
            }
        }

        private void EndVoiceChat()
        {
            try
            {
                if (IsVisibleVoiceChat)
                {
                    IsVisibleVoiceChat = false;
                }

                DisableActionBar = false;
                UpdateVoiceChatView();
            }
            catch (Exception e)
            {
                string sMsg = e.Message;
            }
        }


        private async void UpdateVoiceChatView()
        {

            try
            {
                await RaisePropertyChanged(nameof(DisableActionBar));
                await RaisePropertyChanged(nameof(IsVisibleVoiceChat));
            }
            catch (Exception e)
            {
                string sMsg = e.Message;
            }
        }



        private void sendVoiceChatConfirmation()
        {
            FMChatMessageModel msgJoinVisit = new FMChatMessageModel();
            msgJoinVisit.Token = CommonAuthSession.Token;
            msgJoinVisit.MessageType = "voicechat_request_confirmation";
            msgJoinVisit.Text = "";
            msgJoinVisit.Username = ProviderID;
            msgJoinVisit.FirstName = CommonAuthSession.FirstName;
            msgJoinVisit.LastName = CommonAuthSession.LastName;
            msgJoinVisit.ProviderID = ProviderID;
            msgJoinVisit.VisitID = VisitID;
            if (IsLiveSwitch)
            {
                LSSendMessage(VisitChannel, msgJoinVisit).Fail(e =>
                {
                    Log.Debug(String.Format("voicechat_request_confirmation info message fail " + e.Message));
                });
            }
            //else
            //{
            //    var publishResponse = _chatService.PublishAsync(new PublishArgs("/chat/" + ProviderID + "/" + VisitID, JsonConvert.SerializeObject(msgJoinVisit))
            //    {
            //        OnSuccess = (ePublish) =>
            //        {
            //            string pMsg = ePublish.ExtensionNames[0];
            //        },
            //        OnFailure = (ePublish) =>
            //        {
            //            string pMsg = ePublish.Exception.Message;
            //        }
            //    });
            //}
        }


    }
}