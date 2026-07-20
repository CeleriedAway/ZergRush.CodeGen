using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Tests {

    public partial class LivableAddressLeaf
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

        }
        protected void MortifyChildren() 
        {

        }
        public void VisitNode(Action<object> action) 
        {

        }
        public override ZergRush.Alive.ILivable GetLivableChild(int localChildId) 
        {
            return base.GetLivableChild(localChildId);
        }
        public void __PropagateHierarchy() 
        {

        }
        public  LivableAddressLeaf() 
        {

        }
    }
}
#endif
