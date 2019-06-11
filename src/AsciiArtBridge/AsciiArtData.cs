using Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsciiArtBridge
{
    public class AsciiArtData : IBridge
    {
        public const string KEY = nameof(AsciiArtData);

        private readonly string _data;

        public AsciiArtData(string data)
        {
            _data = data;
        }

        public string Key { get; } = KEY;

        public byte[] Serialize() => Encoding.UTF8.GetBytes(_data);

        public void Draw() => Console.WriteLine(_data);
    }
}
