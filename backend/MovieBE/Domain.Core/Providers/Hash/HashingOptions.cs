using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Providers.Hash
{
    public sealed class HashingOptions
    {
        public const string Hashing = "Hashing";

        public int Iterations { get; set; } = 10000;
    }
}
