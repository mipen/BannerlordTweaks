using System.Collections.Generic;
using System.Linq;

namespace ModLib
{
    public static class StackExtension
    {
        public static void AppendToTop<T>(this Stack<T> baseStack, Stack<T> toAppend)
        {
            if (toAppend.Count == 0)
                return;

            T[] array = toAppend.ToArray();
            for (int i = array.Count() - 1; i >= 0; i--)
            {
                baseStack.Push(array[i]);
            }
        }
    }
}
