using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SyncIONetSealRemoteCode.Calling {
    class RemoteFunctionProfile {

        public string ApplicationID { get; }

        public string FunctionName { get; }

        public string FunctionDescription { get; }

        public string FunctionSig { get; }

        public RemoteFunctionProfile(string _appId, string _funcName, string _desc, MethodInfo delType) {
            ApplicationID = _appId;
            FunctionName = _funcName;
            FunctionDescription = _desc;

            var sb = new StringBuilder();
            sb.Append(delType.ReturnType.Name);
            sb.Append(" ");
            sb.Append(_funcName);
            sb.Append("(");
            var param = delType.GetParameters();

            if(param.Length > 0) {
                sb.Append(param[0].ParameterType.Name);
                sb.Append(" ");
                sb.Append(param[0].Name);
            }
                
            if(param.Length > 1) {
                for(int i = 1; i < param.Length; i++) {
                    sb.Append(", ");
                    sb.Append(param[i].ParameterType.Name);
                    sb.Append(" ");
                    sb.Append(param[i].Name);
                }
            }
            sb.Append(")");
            FunctionSig = sb.ToString();
        }
    }
}
