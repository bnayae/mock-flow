using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class Topiced<T>
    {
        private readonly string Topic;
        private readonly T Item;

        public Topiced(string topic, T item)
        {
            Topic = topic;
            Item = item;
        }
    }
}
