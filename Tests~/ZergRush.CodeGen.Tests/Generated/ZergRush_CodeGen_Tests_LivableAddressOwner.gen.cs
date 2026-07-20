using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class LivableAddressOwner
    {
        public override void Enlive() 
        {
            EnliveSelf();
            EnliveChildren();
        }
        public override void Mortify() 
        {
            MortifySelf();
            MortifyChildren();
        }
        protected override void EnliveChildren() 
        {
            base.EnliveChildren();
            abilityList.Enlive();
            directChild.Enlive();
            slotChild.Enlive();
        }
        protected override void MortifyChildren() 
        {
            base.MortifyChildren();
            abilityList.Mortify();
            directChild.Mortify();
            slotChild.Mortify();
        }
        public override void VisitNode(Action<object> action) 
        {
            base.VisitNode(action);
            abilityList.VisitNode(action);
            directChild.VisitNode(action);
            slotChild.VisitNode(action);
        }
        public override ZergRush.Alive.ILivable GetLivableChild(int localChildId) 
        {
            switch (localChildId)
            {
                case 2: return abilityList;
                case 3: return directChild;
                case 4: return slotChild.value;
            }
            return base.GetLivableChild(localChildId);
        }
        public override void __PropagateHierarchy() 
        {
            base.__PropagateHierarchy();
            abilityList.SetRootAndCarrier(root, this);
            abilityList.__PropagateHierarchy();
            directChild.SetRootAndCarrier(root, this);
            directChild.__PropagateHierarchy();
            slotChild.SetRootAndCarrier(root, this);
            slotChild.__PropagateHierarchy();
        }
        public  LivableAddressOwner() 
        {
            abilityList = new ZergRush.Alive.LivableList<ZergRush.CodeGen.Tests.LivableAddressLeaf>();
            directChild = new ZergRush.CodeGen.Tests.LivableAddressLeaf();
            slotChild = new ZergRush.Alive.LivableSlot<ZergRush.CodeGen.Tests.LivableAddressLeaf>();
            abilityList.livableAddressId = 2;
            directChild.livableAddressId = 3;
            slotChild.livableAddressId = 4;
        }
    }
}
#endif
