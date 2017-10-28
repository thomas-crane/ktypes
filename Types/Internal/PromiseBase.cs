using KTypes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTypes.Types.Internal
{
    public abstract class PromiseBase : IPromise
    {
        public virtual Action<object> _thenAction { get; set; }
        public virtual Action<Exception> _catchAction { get; set; }

        /// <summary>
        /// Sets the callback for when the Promise is resolved.
        /// </summary>
        /// <param name="action">The callback to invoke when the Promise is resolved.</param>
        /// <returns>Returns itself for use in method chaining.</returns>
        public IPromise Then(Action<object> action)
        {
            _thenAction = action;
            return this;
        }
        /// <summary>
        /// Sets the callback for when the Promise is rejected.
        /// </summary>
        /// <param name="action">The callback to invoke when the Promise is rejected.</param>
        /// <returns>Returns itself for use in method chaining.</returns>
        public IPromise Catch(Action<Exception> action)
        {
            _catchAction = action;
            return this;
        }

        /// <summary>
        /// Invokes the <see cref="_catchAction"/> of this Promise.
        /// </summary>
        /// <param name="arg">The reason for the Promise rejection.</param>
        public void Reject(Exception arg)
        {
            if (_catchAction == null)
                throw new UnhandledPromiseRejectionException();
            _catchAction.Invoke(arg);
        }
        /// <summary>
        /// Invokes the <see cref="_thenAction"/> of this Promise.
        /// </summary>
        /// <param name="arg">The object to resolve the Promise with.</param>
        public void Resolve(object arg)
        {
            if (_thenAction == null)
                return;
            try
            {
                _thenAction.Invoke(arg);
            } catch (Exception e)
            {
                Reject(e);
            }
        }
    }

    public abstract class PromiseBase<T>: IPromise<T>
    {
        public virtual Action<T> _thenAction { get; set; }
        public virtual Action<Exception> _catchAction { get; set; }

        /// <summary>
        /// Sets the callback for when the Promise is resolved.
        /// </summary>
        /// <param name="action">The callback to invoke when the Promise is resolved.</param>
        /// <returns>Returns itself for use in method chaining.</returns>
        public IPromise<T> Then(Action<T> action)
        {
            _thenAction = action;
            return this;
        }
        /// <summary>
        /// Sets the callback for when the Promise is rejected.
        /// </summary>
        /// <param name="action">The callback to invoke when the Promise is rejected.</param>
        /// <returns>Returns itself for use in method chaining.</returns>
        public IPromise<T> Catch(Action<Exception> action)
        {
            _catchAction = action;
            return this;
        }

        /// <summary>
        /// Invokes the <see cref="_catchAction"/> of this Promise.
        /// </summary>
        /// <param name="arg">The reason for the Promise rejection.</param>
        public void Reject(Exception arg)
        {
            if (_catchAction == null)
                throw new UnhandledPromiseRejectionException();
            _catchAction.Invoke(arg);
        }
        /// <summary>
        /// Invokes the <see cref="_thenAction"/> of this Promise.
        /// </summary>
        /// <param name="arg">The object to resolve the Promise with.</param>
        public void Resolve(T arg)
        {
            if (_thenAction == null)
                return;
            try
            {
                _thenAction.Invoke(arg);
            } catch (Exception e)
            {
                Reject(e);
            }
        }
    }

    public abstract class PromiseBase<T1, T2> : IPromise<T1, T2>
    {
        public virtual Action<T1, T2> _thenAction { get; set; }
        public virtual Action<Exception> _catchAction { get; set; }

        /// <summary>
        /// Sets the callback for when the Promise is resolved.
        /// </summary>
        /// <param name="action">The callback to invoke when the Promise is resolved.</param>
        /// <returns>Returns itself for use in method chaining.</returns>
        public IPromise<T1, T2> Then(Action<T1, T2> action)
        {
            _thenAction = action;
            return this;
        }
        /// <summary>
        /// Sets the callback for when the Promise is rejected.
        /// </summary>
        /// <param name="action">The callback to invoke when the Promise is rejected.</param>
        /// <returns>Returns itself for use in method chaining.</returns>
        public IPromise<T1, T2> Catch(Action<Exception> action)
        {
            _catchAction = action;
            return this;
        }

        /// <summary>
        /// Invokes the <see cref="_catchAction"/> of this Promise.
        /// </summary>
        /// <param name="arg">The reason for the Promise rejection.</param>
        public void Reject(Exception arg)
        {
            if (_catchAction == null)
                throw new UnhandledPromiseRejectionException();
            _catchAction.Invoke(arg);
        }
        /// <summary>
        /// Invokes the <see cref="_thenAction"/> of this Promise.
        /// </summary>
        /// <param name="arg1">One of the objects which the Promise will be resolved with.</param>
        /// <param name="arg2">One of the objects which the Promise will be resolved with.</param>
        public void Resolve(T1 arg1, T2 arg2)
        {
            if (_thenAction == null)
                return;
            try
            {
                _thenAction.Invoke(arg1, arg2);
            } catch (Exception e)
            {
                Reject(e);
            }
        }
    }

    public abstract class PromiseBase<T1, T2, T3> : IPromise<T1, T2, T3>
    {
        public virtual Action<T1, T2, T3> _thenAction { get; set; }
        public virtual Action<Exception> _catchAction { get; set; }

        /// <summary>
        /// Sets the callback for when the Promise is resolved.
        /// </summary>
        /// <param name="action">The callback to invoke when the Promise is resolved.</param>
        /// <returns>Returns itself for use in method chaining.</returns>
        public IPromise<T1, T2, T3> Then(Action<T1, T2, T3> action)
        {
            _thenAction = action;
            return this;
        }
        /// <summary>
        /// Sets the callback for when the Promise is rejected.
        /// </summary>
        /// <param name="action">The callback to invoke when the Promise is rejected.</param>
        /// <returns>Returns itself for use in method chaining.</returns>
        public IPromise<T1, T2, T3> Catch(Action<Exception> action)
        {
            _catchAction = action;
            return this;
        }

        /// <summary>
        /// Invokes the <see cref="_catchAction"/> of this Promise.
        /// </summary>
        /// <param name="arg">The reason for the Promise rejection.</param>
        public void Reject(Exception arg)
        {
            if (_catchAction == null)
                throw new UnhandledPromiseRejectionException();
            _catchAction.Invoke(arg);
        }
        /// <summary>
        /// Invokes the <see cref="_thenAction"/> of this Promise.
        /// </summary>
        /// <param name="arg1">One of the objects which the Promise will be resolved with.</param>
        /// <param name="arg2">One of the objects which the Promise will be resolved with.</param>
        /// <param name="arg3">One of the objects which the Promise will be resolved with.</param>
        public void Resolve(T1 arg1, T2 arg2, T3 arg3)
        {
            if (_thenAction == null)
                return;
            try
            {
                _thenAction.Invoke(arg1, arg2, arg3);
            } catch (Exception e)
            {
                Reject(e);
            }
        }
    }
}
