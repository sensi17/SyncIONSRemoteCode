using SyncIO.Transport.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIORemoteCode.Networking {
    [Serializable]
    public class HandshakePacket : IPacket {

        public string Username { get; set; }
        public string AppID { get; set; }

        public HandshakePacket(string _username, string _appID) {
            Username = _username;
            AppID = _appID;
        }

    }
}
