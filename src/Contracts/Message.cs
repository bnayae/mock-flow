using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Contracts
{
    public class Message
    {
        #region Ctor

        public Message(Metadata metadata)
        {
            Metadata = metadata;
        }

        public Message(Metadata metadata, ImmutableDictionary<string, byte[]> extensions)
        {
            Metadata = metadata;
            _extensions = extensions;
        }

        public Message(Metadata metadata, IEnumerable<IBridge> extensions)
        {
            Metadata = metadata;
            var pairs = extensions.Select(m => new KeyValuePair<string, byte[]>(m?.Key, m?.Serialize()));
            _extensions.AddRange(pairs);
        }
        
        #endregion // Ctor

        public readonly Metadata Metadata;

        #region Operations

        internal readonly ImmutableDictionary<string, byte[]> _extensions = ImmutableDictionary<string, byte[]>.Empty;

        public (byte[] value, bool hasValue) this[string key]
        {
            get
            {
                bool hasValue = _extensions.TryGetValue(key, out byte[] value);
                return (value, hasValue);
            }
        }

        public (Message message, bool added) Add(string key, byte[] value)
        {
            if (_extensions.ContainsKey(key))
            {
                value = default;
                return (this, false);
            }
            var extensions = _extensions.Add(key, value);
            var message = new Message(Metadata, extensions);
            return (message, true);
        }

        public (Message message, bool added) Add(IBridge bridge)
        {
            byte[] value = bridge?.Serialize();
            if (_extensions.ContainsKey(bridge.Key))
            {
                value = default;
                return (this, false);
            }
            var extensions = _extensions.Add(bridge.Key, value);
            var message = new Message(Metadata, extensions);
            return (message, true);
        }

        public Message AddOrUpdate(string key, byte[] value)
        {
            ImmutableDictionary<string, byte[]> extensions;
            extensions = _extensions.Remove(key)
                                     .Add(key, value);
            var message = new Message(Metadata, extensions);
            return message;
        }

        public Message AddOrUpdate(string key, IBridge bridge)
        {
            byte[] value = bridge?.Serialize();
            ImmutableDictionary<string, byte[]> extensions;
            extensions = _extensions.Remove(key)
                                     .Add(key, value);
            var message = new Message(Metadata, extensions);
            return message;
        }

        public MessageBuilder ToBuilder() => new MessageBuilder(this);

        #endregion // Operations
    }
}
