using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ZergRush.Alive;
using ZergRush;
#if !INCLUDE_ONLY_CODE_GENERATION

public static partial class SerializationExtensions
{
    public static void UpdateFrom(this System.Collections.Generic.Dictionary<int, ZergRush.CodeGen.Dictionary.Tests.DictionaryValue> self, System.Collections.Generic.Dictionary<int, ZergRush.CodeGen.Dictionary.Tests.DictionaryValue> other, ZRUpdateFromHelper __helper) 
    {
        if (other.Count == 0) { self.Clear(); return; }
        int[] __keysToRemove = null;
        int __removeCount = 0;
        foreach (var __pair in self)
        {
            if (!other.ContainsKey(__pair.Key))
            {
                __keysToRemove ??= new int[self.Count];
                __keysToRemove[__removeCount++] = __pair.Key;
            }
        }
        for (int __i = 0; __i < __removeCount; ++__i)
        {
            self.Remove(__keysToRemove[__i]);
        }
        foreach (var __pair in other)
        {
            if (self.TryGetValue(__pair.Key, out var __value))
            {
                if (__pair.Value == null) {
                    __value = null;
                }
                else { 
                    if (__value == null) {
                        __value = new ZergRush.CodeGen.Dictionary.Tests.DictionaryValue();
                    }
                    __value.UpdateFrom(__pair.Value, __helper);
                }
            }
            else
            {
                if (__pair.Value == null) {
                    __value = null;
                }
                else { 
                    __value = new ZergRush.CodeGen.Dictionary.Tests.DictionaryValue();
                    __value.UpdateFrom(__pair.Value, __helper);
                }
            }
            self[__pair.Key] = __value;
        }
    }
}
#endif
