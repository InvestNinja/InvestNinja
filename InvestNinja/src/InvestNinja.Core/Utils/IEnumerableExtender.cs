using System;
using System.Collections.Generic;

namespace InvestNinja.Core.Utils
{
    public static class IEnumerableExtender
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (list != null)
                foreach (var item in list)
                    action.Invoke(item);
        }
    }
}
