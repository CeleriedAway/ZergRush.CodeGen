using ZergRush;
using ZergRush.Alive;

namespace ZergRush.CodeGen.Tests;

public sealed class LivableAddressTests
{
    [Fact]
    public void Generated_ids_and_lookup_round_trip_direct_slot_list_and_inherited_children()
    {
        var root = new LivableAddressRoot();
        var owner = root.owner;

        Assert.Equal(1, owner.livableAddressId);
        Assert.Equal(1, owner.alphaBaseChild.livableAddressId);
        Assert.Equal(2, owner.abilityList.livableAddressId);
        Assert.Equal(3, owner.directChild.livableAddressId);
        Assert.Equal(4, owner.slotChild.livableAddressId);

        Assert.Same(owner, root.GetLivableChild(1));
        Assert.Same(owner.alphaBaseChild, owner.GetLivableChild(1));
        Assert.Same(owner.abilityList, owner.GetLivableChild(2));
        Assert.Same(owner.directChild, owner.GetLivableChild(3));
        Assert.Null(owner.GetLivableChild(4));

        var slotValue = new LivableAddressLeaf();
        owner.slotChild.value = slotValue;
        Assert.Equal(4, slotValue.livableAddressId);
        Assert.Same(owner, slotValue.carrier);
        Assert.Null(slotValue.intermediateContainer);
        Assert.Same(slotValue, owner.GetLivableChild(4));

        var listValue = new LivableAddressLeaf();
        owner.abilityList.Add(listValue);
        Assert.Equal(0, listValue.livableAddressId);
        Assert.Same(owner, listValue.carrier);
        Assert.Same(owner.abilityList, listValue.intermediateContainer);

        Assert.Empty(root.GetLivableAddress());
        Assert.Equal(new[] { 1, 1 }, owner.alphaBaseChild.GetLivableAddress());
        Assert.Equal(new[] { 1, 3 }, owner.directChild.GetLivableAddress());
        Assert.Equal(new[] { 1, 4 }, slotValue.GetLivableAddress());
        Assert.Equal(new[] { 1, 2, 0 }, listValue.GetLivableAddress());

        Assert.Same(owner.alphaBaseChild, root.GetLivableChild(owner.alphaBaseChild.GetLivableAddress()));
        Assert.Same(owner.directChild, root.GetLivableChild(owner.directChild.GetLivableAddress()));
        Assert.Same(slotValue, root.GetLivableChild(slotValue.GetLivableAddress()));
        Assert.Same(listValue, root.GetLivableChild(listValue.GetLivableAddress()));
    }

    [Fact]
    public void Livable_list_keeps_index_ids_correct_for_every_mutation_path()
    {
        var root = new LivableAddressRoot();
        var list = root.owner.abilityList;
        var first = new LivableAddressLeaf { marker = 1 };
        var second = new LivableAddressLeaf { marker = 2 };
        var third = new LivableAddressLeaf { marker = 3 };

        list.Add(first);
        list.Add(second);
        list.Add(third);
        AssertListIds(list);

        var inserted = new LivableAddressLeaf { marker = 4 };
        list.Insert(1, inserted);
        AssertListIds(list);

        list.RemoveAt(0);
        AssertListIds(list);
        Assert.False(first.TryGetLivableAddress(out _));

        var replacement = new LivableAddressLeaf { marker = 5 };
        list[1] = replacement;
        AssertListIds(list);
        Assert.Equal(1, replacement.livableAddressId);

        list.SwapItem(0, 2);
        AssertListIds(list);

        var copied = new LivableAddressLeaf();
        list.InsertCopy(copied, new LivableAddressLeaf { marker = 6 }, new ZRUpdateFromHelper(), 1);
        AssertListIds(list);

        var addedCopy = new LivableAddressLeaf();
        list.AddCopy(addedCopy, new LivableAddressLeaf { marker = 7 }, new ZRUpdateFromHelper());
        AssertListIds(list);

        list.Insert(0, null!);
        Assert.Null(list.GetLivableChild(0));
        AssertListIds(list);

        for (var i = 0; i < list.Count; i++)
        {
            if (list[i] != null) list[i].livableAddressId = 99;
        }
        list.__PropagateHierarchy();
        AssertListIds(list);

        var survivor = list.First(item => item != null);
        list.Clear();
        Assert.Empty(list);
        Assert.False(survivor.TryGetLivableAddress(out _));
    }

    [Fact]
    public void Address_helpers_reject_invalid_detached_stale_and_cyclic_paths()
    {
        var root = new LivableAddressRoot();
        Assert.Same(root, root.GetLivableChild(Array.Empty<int>()));
        Assert.Null(root.GetLivableChild(-1));
        Assert.Null(root.GetLivableChild(99));
        Assert.False(root.TryGetLivableChild(new[] { 99 }, out _));
        Assert.Throws<InvalidOperationException>(() => root.GetLivableChild(new[] { 99 }));

        Assert.False(root.TryGetLivableChild(new[] { 1, 4 }, out _));
        Assert.Throws<InvalidOperationException>(() => root.GetLivableChild(new[] { 1, 4 }));

        var detached = new LivableAddressLeaf();
        Assert.False(detached.TryGetLivableAddress(out _));
        Assert.Throws<InvalidOperationException>(() => detached.GetLivableAddress());

        detached.carrier = detached;
        Assert.False(detached.TryGetLivableAddress(out _));

        var stale = new LivableAddressLeaf();
        root.owner.abilityList.Add(stale);
        root.owner.abilityList.Remove(stale);
        Assert.False(stale.TryGetLivableAddress(out _));
    }

    [Fact]
    public void Livable_address_array_round_trips_through_binary_and_json_serialization()
    {
        var root = new LivableAddressRoot();
        var item = new LivableAddressLeaf();
        root.owner.abilityList.Add(item);
        var address = item.GetLivableAddress();
        var holder = new CodeGenCoverageSamples { primitiveArray = address };

        var binary = holder.WriteToByteArray().Read<CodeGenCoverageSamples>();
        var json = holder.WriteToJsonString().ReadFromJson<CodeGenCoverageSamples>();

        Assert.Equal(address, binary.primitiveArray);
        Assert.Equal(address, json.primitiveArray);
        Assert.Same(item, root.GetLivableChild(binary.primitiveArray));
        Assert.Same(item, root.GetLivableChild(json.primitiveArray));
    }

    static void AssertListIds(LivableList<LivableAddressLeaf> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            var item = list[i];
            if (item == null) continue;
            Assert.Equal(i, item.livableAddressId);
            Assert.Same(item, list.GetLivableChild(i));
        }

        Assert.Null(list.GetLivableChild(-1));
        Assert.Null(list.GetLivableChild(list.Count));
    }
}
