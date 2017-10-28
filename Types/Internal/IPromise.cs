using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTypes.Types.Internal
{
    public interface IPromise
    {
        Action<object> _thenAction { get; set; }
        Action<Exception> _catchAction { get; set; }

        IPromise Then(Action<object> action);
        IPromise Catch(Action<Exception> action);

        void Resolve(object arg);
        void Reject(Exception arg);
    }

    public interface IPromise<T>
    {
        Action<T> _thenAction { get; set; }
        Action<Exception> _catchAction { get; set; }

        IPromise<T> Then(Action<T> action);
        IPromise<T> Catch(Action<Exception> action);

        void Resolve(T arg);
        void Reject(Exception arg);
    }


    public interface IPromise<T1, T2>
    {
        Action<T1, T2> _thenAction { get; set; }
        Action<Exception> _catchAction { get; set; }

        IPromise<T1, T2> Then(Action<T1, T2> action);
        IPromise<T1, T2> Catch(Action<Exception> action);

        void Resolve(T1 arg1, T2 arg2);
        void Reject(Exception arg);
    }

    public interface IPromise<T1, T2, T3>
    {
        Action<T1, T2, T3> _thenAction { get; set; }
        Action<Exception> _catchAction { get; set; }

        IPromise<T1, T2, T3> Then(Action<T1, T2, T3> action);
        IPromise<T1, T2, T3> Catch(Action<Exception> action);

        void Resolve(T1 arg1, T2 arg2, T3 arg3);
        void Reject(Exception arg);
    }
}
