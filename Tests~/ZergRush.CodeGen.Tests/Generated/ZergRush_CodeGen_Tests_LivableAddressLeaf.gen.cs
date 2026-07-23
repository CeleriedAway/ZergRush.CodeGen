using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class LivableAddressLeaf
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

        }
        protected override void MortifyChildren() 
        {
            base.MortifyChildren();

        }
        public override void VisitNode(Action<object> action) 
        {
            base.VisitNode(action);

        }
        public override ZergRush.Alive.ILivable GetLivableChild(int localChildId) 
        {
            return base.GetLivableChild(localChildId);
        }
        public override void __PropagateHierarchy() 
        {
            base.__PropagateHierarchy();

        }
        public  LivableAddressLeaf() 
        {

        }
    }
}
#endif
