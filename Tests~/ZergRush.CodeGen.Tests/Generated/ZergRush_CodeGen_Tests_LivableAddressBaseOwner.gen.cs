using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class LivableAddressBaseOwner
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
            alphaBaseChild.Enlive();
        }
        protected override void MortifyChildren() 
        {
            base.MortifyChildren();
            alphaBaseChild.Mortify();
        }
        public override void VisitNode(Action<object> action) 
        {
            base.VisitNode(action);
            alphaBaseChild.VisitNode(action);
        }
        public override ZergRush.Alive.ILivable GetLivableChild(int localChildId) 
        {
            switch (localChildId)
            {
                case 1: return alphaBaseChild;
            }
            return base.GetLivableChild(localChildId);
        }
        public override void __PropagateHierarchy() 
        {
            base.__PropagateHierarchy();
            alphaBaseChild.SetRootAndCarrier(root, this);
            alphaBaseChild.__PropagateHierarchy();
        }
        public  LivableAddressBaseOwner() 
        {
            alphaBaseChild = new ZergRush.CodeGen.Tests.LivableAddressLeaf();
            alphaBaseChild.livableAddressId = 1;
        }
    }
}
#endif
