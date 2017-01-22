using SIORemoteCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleRemoteCode {
    [RemoteApp("App ID", "Remote API Token")]
    public class RemoteCodeLub {

        [RemoteFunction("Reverse", "Reverses a given string")]
        public static string reverseString(string s) {
            return string.Concat(s.Reverse());
        }


    }
}
