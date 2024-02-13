using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.DependencyServices
{
    public interface IIceLinkCommunication
    {
        Task<bool> Init();
        Task<bool> StartAudioCall();
        Task<bool> StartVideoCall();
        Task<bool> EndAudioCall();
        Task<bool> EndVideoCall();
        Task<bool> MuteVideoCall();
        Task<bool> MuteAudioCall();

        Task<bool> RecordVideoCall();
        Task<bool> RecordAudioCall();

        Task<bool> PauseVideoCall();
        Task<bool> PauseAudioCall();

        Task<bool> IsVideoCallActive();
        Task<bool> IsAudioCallActive();

        void GenerateCertificate();
    }
}
