// ZergRush serialization schema: 3 (array JSON envelopes, logical collections, replacement reads)
using System;
using System.Collections.Generic;
using System.Text;

namespace ZergRush.CodeGen.Tests {
    public enum JsonArrayNodeType : ushort
    {
        JsonArrayNode = 1,
        JsonArrayChild = 2,
    }
}

