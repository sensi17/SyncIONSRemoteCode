using SIORemoteCode;
using SIORemoteCode.Networking;
using SyncIO.Network;
using SyncIO.Server;
using SyncIO.Transport.Packets;
using SyncIONetSealRemoteCode.Calling;
using SyncIONetSealRemoteCode.Client;
using SyncIONetSealRemoteCode.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SyncIONetSealRemoteCode.Windows {
    /// <summary>
    /// Interaction logic for winMain.xaml
    /// </summary>
    public partial class winMain : Window {

        private SyncIOServer server;

        public winMain() {
            InitializeComponent();

           

            server = new SyncIOServer(SyncIO.Transport.TransportProtocal.IPv4, RemoteConfig.GetPackager());

            server.SetHandler<HandshakePacket>((client, packet) => {
                var nsClientInfo = new NSClient(packet.Username, packet.AppID);
                client.Tag = nsClientInfo;
                client.Send(new HandshakeResponsePacket(true));
            });

            server.SetHandler((SyncIOConnectedClient client, object[] p) => {
                client.Send(p);
            });

            server.SetDefaultRemoteFunctionAuthCallback((client, func) => {
                var FuncInfo = func.Tag as FunctionInfo;
                if (FuncInfo == null)
                    return false;

                var nsClientInfo = client.Tag as NSClient;
                if (nsClientInfo == null)
                    return false;

                var succ = nsClientInfo.AppID.Equals(FuncInfo.ID) &&
                    NetSealAPI.IsValidConnection(FuncInfo.ApiCode, client, nsClientInfo.Username);

                if (!succ)
                    client.Disconnect(new AccessViolationException());

                return succ;
            });
            
            LoadFunctions();

            foreach (var port in ConfigLoader.GetPorts())
                server.ListenTCP(port);

            var listeningPorts = server.Count();
            if(listeningPorts < 1) {
                lblStatus.Content = $"Idle. (listening on {listeningPorts} ports).";
            }else {
                lblStatus.Content = $"listening on {listeningPorts} ports.";
            }
        }


        private void LoadFunctions() {
            var dirInfo = new DirectoryInfo("Remote");
            if (!dirInfo.Exists)
                dirInfo.Create();

            foreach(var file in dirInfo.GetFiles("*.dll")) {
                try {
                    var asm = Assembly.LoadFile(file.FullName);
                    foreach(var t in asm.GetTypes()) {
                        var typeAtt = t.GetCustomAttribute<RemoteAppAttribute>();
                        if (typeAtt == null)
                            continue;

                        foreach(var m in t.GetMethods(BindingFlags.Public | BindingFlags.Static)) {
                            var methodAtt = m.GetCustomAttribute<RemoteFunctionAttribute>();
                            if (methodAtt == null)
                                continue;
                            var del = System.Linq.Expressions.Expression.GetDelegateType(m.GetParameters()
                                .Select(x => x.ParameterType)
                                .Concat(new[] { m.ReturnType })
                                .ToArray());

                            var profile = new RemoteFunctionProfile(typeAtt.AppID, methodAtt.Name, methodAtt.Description, m);
                            var funcName = string.Format("{0}.{1}", profile.ApplicationID, profile.FunctionName);
                            var newRemoteFunc = server.RegisterRemoteFunction(funcName, m.CreateDelegate(del));
                            newRemoteFunc.Tag = new FunctionInfo(typeAtt.AppID, typeAtt.ApiToken);
                            lvFunctions.Items.Add(profile);
                        }

                    }
               }catch {
                   MessageBox.Show("Failed to load " + file.Name);
               }
            }
        }

        private void miNetworking_Click(object sender, RoutedEventArgs e) {
            var nwwin = new winNetworkSettings(server);
            nwwin.ShowDialog();
            ConfigLoader.WritePorts(server.Select(x => x.EndPoint.Port).ToArray());
            var listeningPorts = server.Count();
            if (listeningPorts < 1) {
                lblStatus.Content = $"Idle. (listening on {listeningPorts} ports).";
            } else {
                lblStatus.Content = $"listening on {listeningPorts} ports.";
            }
        }
    }
}
