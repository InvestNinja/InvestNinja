using System;
using System.Collections.Generic;

namespace InvestNinja.Core.Utils
{
    public static class IListExtender
    {
        public static void ForEach<T>(this IList<T> list, Action<T> action)
        {
            if (list != null)
                foreach(var item in list)
                    action.Invoke(item);
        }

        public static void AddRange<T>(this IList<T> list, IList<T> listAdd) => listAdd?.ForEach<T>(item => list.Add(item));
    }
}
