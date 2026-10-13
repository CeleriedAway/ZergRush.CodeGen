using System.Collections.Generic;
using ZergRush.CodeGen;

namespace ZergRush.JsonBenchmarks;

[GenTask(GenTaskFlags.SimpleDataPack)]
public partial class BenchRoom
{
    public List<BenchValue> values;
    public string note;
}

[GenTask(GenTaskFlags.PolymorphicDataPack)]
public partial class BenchValue
{
    public int score;
    public string label;
    [CanBeNull] public BenchValue nested;
}

public partial class BenchChild : BenchValue
{
    public int extra;
}

[GenTask(GenTaskFlags.SimpleDataPack)]
public partial class BenchGraph
{
    public List<BenchReference> values;
}

[GenTask(GenTaskFlags.PolymorphicDataPack), GenMultipleRefs]
public partial class BenchReference
{
    public int score;
    public string label;
}

public partial class BenchReferenceChild : BenchReference
{
    public int extra;
}
