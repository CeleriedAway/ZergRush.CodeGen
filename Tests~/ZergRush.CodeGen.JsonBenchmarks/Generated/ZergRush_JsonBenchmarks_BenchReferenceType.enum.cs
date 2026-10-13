// ZergRush serialization schema: 3 (array JSON envelopes, logical collections, replacement reads)
using System;
using System.Collections.Generic;
using System.Text;

namespace ZergRush.JsonBenchmarks {
    public enum BenchReferenceType : ushort
    {
        BenchReference = 1,
        BenchReferenceChild = 2,
    }
}

