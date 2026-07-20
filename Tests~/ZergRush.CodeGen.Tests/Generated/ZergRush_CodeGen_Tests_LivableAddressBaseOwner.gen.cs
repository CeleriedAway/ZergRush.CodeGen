using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class LivableAddressBaseOwner
    {
        public virtual void Enlive() 
        {
            EnliveSelf();
            EnliveChildren();
        }
        public virtual void Mortify() 
        {
            MortifySelf();
            MortifyChildren();
        }
        protected virtual void EnliveChildren() 
        {
            alphaBaseChild.Enlive();
        }
        protected virtual void MortifyChildren() 
        {
            alphaBaseChild.Mortify();
        }
        public virtual void VisitNode(Action<object> action) 
        {
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
        public virtual void __PropagateHierarchy() 
        {
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
