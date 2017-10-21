using KTypes.Exceptions;
using KTypes.Types.Internal;
using Lib_K_Relay;
using System;

namespace KTypes.Types
{
    public class CommandListener
    {
        private Proxy _proxy;
        public CommandListener(Proxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        /// <summary>
        /// Listens for any of the commands passed as parameters and
        /// resolves the returned Promise when the command is invoked.
        /// </summary>
        /// <param name="cmd">The commands to listen for.</param>
        /// <returns>A Promise which will be resolved when one of the commands is used.</returns>
        /// <exception cref="UnhandledPromiseRejectionException">Thrown if the promise rejection is not handled.</exception>
        public Promise On(params string[] cmd)
        {
            Promise promise = new Promise();
            if (_proxy == null)
            {
                throw new Exception(nameof(_proxy) + " cannot be null.");
            }
            foreach (string c in cmd)
            {
                _proxy.HookCommand(c, (client, cm, args) =>
                {
                    try
                    {
                        if (args == null)
                            args = new string[0];
                        promise.InvokeThen(client, args);
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            promise.InvokeCatch(e);
                        } catch (UnhandledPromiseRejectionException uprException)
                        {
                            throw uprException;
                        }
                    }
                });
            }
            return promise;
        }
    }
}
