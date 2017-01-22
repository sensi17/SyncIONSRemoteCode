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
    /// Interaction logic for winAddPort.xaml
    /// </summary>
    public partial class winAddPort : Window {

        public int Port { get; private set; }

        public winAddPort() {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e) {
            int temp;
            if(!int.TryParse(tbPort.Text, out temp)) {
                MessageBox.Show("Invalid port.");
                return;
            }

            Port = temp;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
