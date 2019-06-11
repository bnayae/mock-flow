using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace PipesFlow
{
    public class Message
    {
        public Message(Guid id)
        {
            Id = id;
        }

        public Message(Guid id, ImmutableDictionary<string, byte[]> extensions)
        {
            Id = id;
            Extensions = extensions;
        }

        public readonly Guid Id;

        public readonly ImmutableDictionary<string, byte[]> Extensions = ImmutableDictionary<string, byte[]>.Empty;

        public MessageBuilder ToBuilder() => new MessageBuilder(this);
    }
}
