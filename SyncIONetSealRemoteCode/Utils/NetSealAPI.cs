using Newtonsoft.Json;
using RestSharp;
using SIORemoteCode.Networking;
using SyncIO.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SyncIONetSealRemoteCode.Utils {
    internal static class NetSealAPI {

        static RestClient ApiClient = new RestClient("http://seal.nimoru.com/Remote2/user.php");

        public static bool IsValidConnection(string token, SyncIOConnectedClient client, string username) {
            try {
                var request = new RestRequest(Method.POST);
                request.AddParameter("token", token);
                request.AddParameter("user", username);
                request.AddParameter("act", "details");
                var jsonResponse = ApiClient.Execute(request);
                var response = JsonConvert.DeserializeObject<Rootobject>(jsonResponse.Content);
                if (response == null || !response.success)
                    return false;

                IPAddress requestedIP;
                if (!IPAddress.TryParse(response.data.ip, out requestedIP))
                    return false;

                return !response.data.banned &&
                       !response.data.expired &&
                        response.data.online &&
                        response.data.user.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
                        client.EndPoint.Address.MapToIPv4().Equals(requestedIP);
            } catch {
                return false;
            }
        }



        public class Rootobject {
            public Data data { get; set; }
            public bool success { get; set; }
            public string error { get; set; }
        }

        public class Data {
            public string user { get; set; }
            public bool online { get; set; }
            public bool expired { get; set; }
            public bool banned { get; set; }
            public string ip { get; set; }
        }


    }
}
