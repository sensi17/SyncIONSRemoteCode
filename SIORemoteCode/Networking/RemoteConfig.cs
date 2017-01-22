using SyncIO.Transport;
using SyncIO.Transport.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIORemoteCode.Networking {
    public static class RemoteConfig {

        public static Packager GetPackager() {
            return new Packager(new Type[] {
                typeof(HandshakePacket),
                typeof(HandshakeResponsePacket)
            });
        }

    }
}
