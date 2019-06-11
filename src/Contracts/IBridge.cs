using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBridge
    {
        string Key { get; }
        byte[] Serialize();
    }

    public interface IBridgeDeserializer<T>
    {
        Task<(T item, bool exists)> DeserializeAsync(Message message);
    }

    ///// <summary>
    ///// Will enable to abstract different extensions execution
    ///// with support for fluent API over the message queue
    ///// </summary>
    //public interface IBridgeBuilder
    //{
    //    IBridgeBuilder Attach<T>(IMQSubscriber meaasgeQueue, IBridgeDeserializer<T> factory, Func<T, Metadata, Task> action);
    //}
}
