using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace PipesFlow
{
    public class MessageBuilder
    {
        
        public MessageBuilder(Guid id)
        {
            _id = id;
        }
        public MessageBuilder(Message message)
            : this(message.Id)
        {
            _extensions.AddRange(message.Extensions);
        }

        private readonly Guid _id;

       
        private readonly ImmutableDictionary<string, byte[]>.Builder _extensions = ImmutableDictionary.CreateBuilder<string, byte[]>();

        public Message ToImmutable() => new Message(_id, _extensions.ToImmutable());
    }
}
