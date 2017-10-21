using KTypes.Exceptions;
using Lib_K_Relay.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTypes.Types.Internal
{
    public sealed class Promise
    {
        private Action<object[]> _thenAction;
        private Action<object[]> _catchAction;
        public Promise()
        {

        }

        public void InvokeThen(params object[] args)
        {
            if (_thenAction == null)
                throw new Exception(nameof(_thenAction) + " cannot be null.");
            if (args == null)
                args = new object[0];
            try
            {
                _thenAction.Invoke(args);
            } catch (Exception e)
            {
                try
                {
                    _catchAction.Invoke(new object[1] { e });
                } catch
                {
                    throw new UnhandledPromiseRejectionException();
                }
            }
        }

        /// <summary>
        /// Invokes the <see cref="_catchAction"/> of this Promise.
        /// </summary>
        /// <param name="args">The arguments to pass to the method being invoked.</param>
        /// <exception cref="UnhandledPromiseRejectionException">Thrown if <see cref="_catchAction"/> is null</exception>
        public void InvokeCatch(params object[] args)
        {
            if (_catchAction == null)
                throw new UnhandledPromiseRejectionException();
            if (args == null)
                args = new object[0];
            _catchAction.Invoke(args);
        }

        /// <summary>
        /// Sets the action to be invoked when the Promise is resolved.
        /// </summary>
        /// <param name="action">The method to invoke</param>
        /// <returns>Returns itself for use in method chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null</exception>
        public Promise Then(Action<object[]> action)
        {
            _thenAction = action ?? throw new ArgumentNullException(nameof(action));
            return this;
        }

        /// <summary>
        /// Sets the action to be invoked if the Promise is rejected.
        /// </summary>
        /// <param name="action">The method to invoke</param>
        /// <returns>Returns itself for use in method chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null</exception>
        public Promise Catch(Action<object[]> action)
        {
            _catchAction = action ?? throw new ArgumentNullException(nameof(action));
            return this;
        }
    }
}
