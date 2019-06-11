using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Contracts
{
    public class Metadata
    {
        public Metadata(Guid id)
        {
            Id = id;
        }

        public readonly Guid Id;
    }
}
