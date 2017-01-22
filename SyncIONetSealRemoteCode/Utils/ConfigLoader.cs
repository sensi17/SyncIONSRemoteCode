using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncIONetSealRemoteCode.Utils {
    internal class ConfigLoader {

        public static int[] GetPorts() {
            if (!File.Exists("ports.txt"))
                return new int[0];

            var lines = File.ReadAllLines("ports.txt");
            var ports = new List<int>();
            foreach(var line in lines) {
                int p;
                if (int.TryParse(line, out p))
                    ports.Add(p);
            }
            return ports.ToArray();
        }

        public static void WritePorts(int[] ports) {
            using(var file = new StreamWriter("ports.txt", false)) {
                foreach (var p in ports)
                    file.WriteLine(p.ToString());
            }
        }


    }
}
