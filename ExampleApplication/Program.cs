using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIORemoteCode;
using NetSeal;
using System.Windows.Forms;

namespace ExampleApplication {
    class Program {

        static RemoteCode Remote;

        static SecureFunction<string> Reverse;

        static void Main(string[] args) {

            var appID = "D6780000";

            var netseal = new Broker();
            netseal.Initialize(appID);
           
            Remote = new RemoteCode(appID, netseal.UserName);
            Remote.OnConnect += Remote_OnConnect;
            Remote.OnDisconnect += Remote_OnDisconnect;
            Remote.Init("127.0.0.1", 12345);
            Application.Run();
        }

        private static void Remote_OnDisconnect() {
            Console.WriteLine("Connection lost.");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private static void Remote_OnConnect() {
            Reverse = Remote.GetFunction<string>("Reverse");
            RealMain();
        }

        private static void RealMain() {

            Console.WriteLine("Enter any string to reverse it.");
            while (true) {
                var s = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(s))
                    continue;

                Console.WriteLine("Reversed: {0}", Reverse.CallWait(s));
            }


        }


    }
}
