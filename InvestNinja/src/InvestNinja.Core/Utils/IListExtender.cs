using System;
using System.Collections.Generic;

namespace InvestNinja.Core.Utils
{
    public static class IListExtender
    {
        public static void ForEach<T>(this IList<T> list, Action<T> action)
        {
            foreach(var item in list)
                action.Invoke(item);
        }
    }
}
