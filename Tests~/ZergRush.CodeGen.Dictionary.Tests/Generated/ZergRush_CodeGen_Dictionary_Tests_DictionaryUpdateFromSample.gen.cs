using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Dictionary.Tests {

    public partial class DictionaryUpdateFromSample : IUpdatableFrom<ZergRush.CodeGen.Dictionary.Tests.DictionaryUpdateFromSample>
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Dictionary.Tests.DictionaryUpdateFromSample other, ZRUpdateFromHelper __helper) 
        {
            values.UpdateFrom(other.values, __helper);
        }
    }
}
#endif
