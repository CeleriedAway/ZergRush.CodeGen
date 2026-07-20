using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using ZergRush;
#if !INCLUDE_ONLY_CODE_GENERATION
namespace ZergRush.CodeGen.Dictionary.Tests {

    public partial class DictionaryValue : IUpdatableFrom<ZergRush.CodeGen.Dictionary.Tests.DictionaryValue>
    {
        public virtual void UpdateFrom(ZergRush.CodeGen.Dictionary.Tests.DictionaryValue other, ZRUpdateFromHelper __helper) 
        {
            number = other.number;
        }
    }
}
#endif
