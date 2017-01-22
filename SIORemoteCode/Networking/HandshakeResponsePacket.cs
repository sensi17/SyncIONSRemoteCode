using SyncIO.Transport.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIORemoteCode.Networking {
    [Serializable]
    public class HandshakeResponsePacket : IPacket {
        public bool Success { get; set; }
        public HandshakeResponsePacket(bool _success) {
            Success = _success;
        }
    }
}
