using Contracts;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Channels
{
    public class TdfChannel : IMQPublisher, IMQSubscriber
    {
        private readonly ConcurrentDictionary<string, BroadcastBlock<Message>> _channels = new ConcurrentDictionary<string, BroadcastBlock<Message>>();
        private static readonly DataflowLinkOptions LINK_OPTIONS = new DataflowLinkOptions { PropagateCompletion = true };

        public Task Enqueue(string topic, Message message)
        {
            BroadcastBlock<Message> channel = _channels.GetOrAdd(topic, new BroadcastBlock<Message>(m => m));

            return channel.SendAsync(message);
        }

        public Task<IDisposable> Subscribe(string topic, Func<Message, Task> action)
        {
            BroadcastBlock<Message> channel = _channels.GetOrAdd(topic, new BroadcastBlock<Message>(m => m));
            var executer = new ActionBlock<Message>(action);
            var subscriptionHandle = channel.LinkTo(executer, LINK_OPTIONS);
            return Task.FromResult(subscriptionHandle);
        }
    }
}
