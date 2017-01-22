using SyncIO.Client.RemoteCalls;
using SyncIO.Network;
using SyncIO.Transport.RemoteCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SIORemoteCode {
    public delegate void SecureFunctionCallback<T>(SecureFunction<T> function, T returnValue, Guid CallID);


    public abstract class SecureFunctionGeneric {

    }

    public class SecureFunction<T> : SecureFunctionGeneric {


        private RemoteFunction<T> func;

        public SecureFunction(RemoteFunction<T> _func) {
            func = _func;
            func.ReturnCallback += (f, r, i) => ReturnCallback?.Invoke(this, r, i);
        }

        /// <summary>
        /// Last value returned from the server
        /// </summary>
        public T LastValue { get; internal set; }


        /// <summary>
        /// Called every time a value s receved for this function
        /// </summary>
        public event SecureFunctionCallback<T> ReturnCallback;

        /// <summary>
        /// Calls function without blocking the current thread
        /// </summary>
        /// <param name="param">Call ID of current call</param>
        /// <returns></returns>
        public Guid Call(params object[] args) => func.Call(args);

        /// <summary>
        /// Calls the function and blocks the current thread untill a return value is receved
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public T CallWait(params object[] args) => func.CallWait(args);
    }

}
