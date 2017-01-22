using SyncIO.Network;
using SyncIO.Server;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for winNetworkSettings.xaml
    /// </summary>
    public partial class winNetworkSettings : Window {

        private SyncIOServer server;

        public winNetworkSettings(SyncIOServer _server) {
            InitializeComponent();

            server = _server;
            foreach(var port in server)
                lbPortList.Items.Add(port);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            var addPort = new winAddPort();
            if(addPort.ShowDialog() ?? false) {
                var newPort = server.ListenTCP(addPort.Port);
                if (newPort != null)
                    lbPortList.Items.Add(newPort);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e) {
            foreach(var port in lbPortList.SelectedItems.Cast<SyncIOSocket>()) {
                lbPortList.Items.Remove(port);
                port.Dispose();
            }
        }
    }
}
