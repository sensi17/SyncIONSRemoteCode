using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIORemoteCode {
    public class RemoteFunctionAttribute : Attribute {

        public string Name { get; }
        public string Description { get; }

        public RemoteFunctionAttribute(string _name, string _description) {
            Name = _name;
            Description = _description;
        }

        public RemoteFunctionAttribute(string _name) : this(_name, string.Empty) {
        }

    }
}
