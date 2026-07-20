using ZergRush.ReactiveCore;

namespace ZergRush.Alive
{
    public interface ILivable
    {
        ILivable? GetLivableChild(int localChildId);
        ILivable? GetLivableAddressParent();
        void SetRootAndCarrier(LivableRoot root, Livable carrier, ILivable? intermediateContainer = null);
        int livableAddressId { get; set; }
        bool IsInHierarchy { get; }
        IEventStream destroyEvent { get; }
    }

    // StableUpdateFrom needs to distinguish lists that own livable hierarchy items.
    public interface ILivableContainer
    {
    }
}
