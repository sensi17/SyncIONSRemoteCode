using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncIONetSealRemoteCode.Client {
    class FunctionInfo {

        public string ID { get; }
        public string ApiCode { get; }

        public FunctionInfo(string _id, string _apiCode) {
            ID = _id;
            ApiCode = _apiCode;
        }

    }
}
