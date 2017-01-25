using SIORemoteCode.Networking;
using SyncIO.Client;
using SyncIO.Client.RemoteCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIORemoteCode {

    public delegate void OnRemoteCodeConnection();
    public class RemoteCode {

        public string AppID { get; }
        public string Username { get; }

        public event OnRemoteCodeConnection OnConnect;
        public event OnRemoteCodeConnection OnDisconnect;

        private SyncIOClient client;
        private object SyncLock = new object();

        private Dictionary<string, SecureFunctionGeneric> FuncList = new Dictionary<string, SecureFunctionGeneric>();

        public RemoteCode(string _appID, string _username) {
            AppID = _appID;
            Username = _username;

            client = new SyncIOClient(SyncIO.Transport.TransportProtocal.IPv4, RemoteConfig.GetPackager());

            client.OnDisconnect += (c, e) => OnDisconnect?.Invoke();
            client.SetHandler<HandshakeResponsePacket>((c, p) => OnConnect?.Invoke());

            client.OnHandshake += (c, id, succ) => {
                if (!succ)
                    OnConnect?.Invoke();
                else
                    c.Send(new HandshakePacket(Username, AppID));
            };
        }


        public bool Init(string IP, int Port) {
            if (client.Connect(IP, Port)) {
                return true;
            } else {
                OnConnect?.Invoke();
                return false;
            }
               
        }

        public SecureFunction<T> GetFunction<T>(string name) {

            if (!client.Connected)
                return null;

            lock (SyncLock) {
                var realName = string.Format("{0}.{1}", AppID, name);
                if (FuncList.ContainsKey(realName)) {
                    return FuncList[realName] as SecureFunction<T>;
                } else {
                    var newFunc = new SecureFunction<T>(client.GetRemoteFunction<T>(realName));
                    FuncList.Add(realName, newFunc);
                    Console.WriteLine(realName);
                    return newFunc;
                }
                   

            }
        }

    }
}
