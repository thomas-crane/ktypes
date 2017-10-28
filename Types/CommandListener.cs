using KTypes.Exceptions;
using KTypes.Types.Internal;
using Lib_K_Relay;
using Lib_K_Relay.Networking;
using System;

namespace KTypes.Types
{
    public class CommandListener
    {
        private Proxy _proxy;
        public CommandListener(Proxy proxy)
        {
            if (proxy == null)
                throw new ArgumentNullException(nameof(proxy));
            _proxy = proxy;
        }

        /// <summary>
        /// Listens for any of the commands passed as parameters and
        /// resolves the returned Promise when the command is invoked.
        /// </summary>
        /// <param name="cmd">The commands to listen for.</param>
        /// <returns>A Promise which will be resolved when one of the commands is used.</returns>
        /// <exception cref="UnhandledPromiseRejectionException">Thrown if the promise rejection is not handled.</exception>
        public CommandPromise On(params string[] cmd)
        {
            CommandPromise promise = new CommandPromise();
            if (_proxy == null)
            {
                throw new Exception(nameof(_proxy) + " cannot be null.");
            }
            foreach (string c in cmd)
            {
                _proxy.HookCommand(c, (client, cm, args) =>
                {
                    if (args == null)
                        args = new string[0];
                    promise.Resolve(client, args);
                });
            }
            return promise;
        }
    }

    public sealed class CommandPromise : PromiseBase<Client, string[]>
    {
    }
}
