using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMQSubscriber
    {
        Task<IDisposable> Subscribe(string topic, Func<Message, Task> action);
    }

    /// <summary>
    /// Should be registered with open generics
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMQSubscriber<T>
    {
        Task<IDisposable> Subscribe(string topic, Func<T, Task> action);
    }
}
