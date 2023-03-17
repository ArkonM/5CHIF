using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PA3_Client
{
    public interface Receiver
    {
        void ReceiveMessage(MSG m, Transfer t);
        void TransferDisconnected(Transfer t);
    }
}
