
namespace Comms
{
    public interface Receiver
    {
        void ReceiveMessage(Message m, Transfer t);
        void TransferDisconnected(Transfer t);

        void AddDebugInfo(Transfer t, string m, bool sent);
    }
}
