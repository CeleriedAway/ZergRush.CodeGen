using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class LivableAddressRoot
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
            owner.Enlive();
        }
        protected override void MortifyChildren() 
        {
            base.MortifyChildren();
            owner.Mortify();
        }
        public override void VisitNode(Action<object> action) 
        {
            base.VisitNode(action);
            owner.VisitNode(action);
        }
        public override ZergRush.Alive.ILivable GetLivableChild(int localChildId) 
        {
            switch (localChildId)
            {
                case 1: return owner;
            }
            return base.GetLivableChild(localChildId);
        }
        public override void __PropagateHierarchy() 
        {
            base.__PropagateHierarchy();
            owner.SetRootAndCarrier(root, this);
            owner.__PropagateHierarchy();
        }
        public  LivableAddressRoot() 
        {
            owner = new ZergRush.CodeGen.Tests.LivableAddressOwner();
            owner.livableAddressId = 1;
            root = this;
            __PropagateHierarchy();
        }
    }
}
#endif
