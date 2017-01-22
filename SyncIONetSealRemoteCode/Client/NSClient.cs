using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncIONetSealRemoteCode.Client {
    internal class NSClient {

        public string Username { get; }
        public string AppID { get; }

        public NSClient(string _username, string _appID) {
            Username = _username;
            AppID = _appID;
        }

    }
}
