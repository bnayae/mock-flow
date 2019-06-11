using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMQPublisher
    {
        Task Enqueue(string topic, Message message);
    }
}
