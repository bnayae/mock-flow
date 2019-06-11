using Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArtBridge
{
    public class AsciiArtFactory : IBridgeDeserializer<AsciiArtData>
    {
        private readonly static Task<(AsciiArtData item, bool exists)> EmptyResult = Task.FromResult((default(AsciiArtData), false));
        public Task<(AsciiArtData item, bool exists)> DeserializeAsync(Message message)
        {
            (byte[] value, bool hasValue) = message[AsciiArtData.KEY];
            if (!hasValue)
                return EmptyResult;
            var ascii = Encoding.UTF8.GetString(value);
            var item = new AsciiArtData(ascii);
            return Task.FromResult((item, true));
        }
    }
}
