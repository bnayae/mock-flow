using System;
using System.Collections.Generic;
using System.Text;

namespace PipesFlow
{
    public class Extendable
    {

        public Extendable(string key, byte[] data)
        {
            Key = key;
            Data = data;
        }

        public readonly string Key;

        public readonly byte[] Data;
    }
}
