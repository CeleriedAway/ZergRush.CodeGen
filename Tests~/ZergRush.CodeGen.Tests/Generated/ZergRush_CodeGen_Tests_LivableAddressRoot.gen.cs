using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class LivableAddressRoot
    {
        public void Enlive() 
        {
            EnliveSelf();
            EnliveChildren();
        }
        public void Mortify() 
        {
            MortifySelf();
            MortifyChildren();
        }
        protected void EnliveChildren() 
        {
            owner.Enlive();
        }
        protected void MortifyChildren() 
        {
            owner.Mortify();
        }
        public void VisitNode(Action<object> action) 
        {
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
        public void __PropagateHierarchy() 
        {
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
