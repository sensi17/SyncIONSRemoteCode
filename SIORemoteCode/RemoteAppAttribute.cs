using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIORemoteCode {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =false, Inherited =false)]
    public class RemoteAppAttribute : Attribute {
        public string AppID { get; }
        public string ApiToken { get; }
        public RemoteAppAttribute(string _app, string remoteApiToken) {
            AppID = _app;
            ApiToken = remoteApiToken;
        }
    }
}
