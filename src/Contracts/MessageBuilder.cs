using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class MessageBuilder
    {
        #region Ctor

        public MessageBuilder(Metadata metadata)
        {
            _metadata = metadata;
            _extensions = new ConcurrentDictionary<string, byte[]>();
        }
        public MessageBuilder(Message message)
            : this(message.Metadata)
        {
            _extensions = new ConcurrentDictionary<string, byte[]>(message._extensions);
        } 

        #endregion // Ctor

        private readonly Metadata _metadata;

        private readonly ConcurrentDictionary<string, byte[]> _extensions = new ConcurrentDictionary<string, byte[]>();

        #region Operations


        public Message ToImmutable() => new Message(_metadata, ImmutableDictionary.CreateRange(_extensions));

        public bool TryAdd(string key, byte[] value) => _extensions.TryAdd(key, value);

        public bool[] AddRange(params IBridge[] bridge) =>
                                    bridge?.Select(m => TryAdd(m.Key, m.Serialize()))
                                           ?.ToArray();
        public Task<bool[]> AddRangeAsync(params IBridge[] bridge)
        {
            var tasks = bridge?.Select(m => Task.Run(() => TryAdd(m.Key, m.Serialize())));
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Add Or Override
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>true when added, false when override</returns>
        public bool AddOrOverride(string key, byte[] value) => 
                            _extensions.AddOrUpdate(key, value, (k, v) => value) == value;

        /// <summary>
        /// Add Or Override
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>For each atempt, returns true when added, false when override</returns>
        public bool[] AddOrOverride(string key, params IBridge[] bridge) => 
                                    bridge?.Select(m => AddOrOverride(m.Key, m.Serialize()))
                                           ?.ToArray();
        /// <summary>
        /// Add Or Override
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>For each atempt, returns true when added, false when override</returns>
        public Task<bool[]> AddOrOverrideAsync(string key, params IBridge[] bridge)
        {
            var tasks = bridge?.Select(m => 
                                Task.Run(() => 
                                            AddOrOverride(m.Key, m.Serialize())));
            return Task.WhenAll(tasks);
        }       

        #endregion // Operations
    }
}
