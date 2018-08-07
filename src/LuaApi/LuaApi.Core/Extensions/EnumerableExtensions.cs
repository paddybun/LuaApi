using System;
using System.Collections.Generic;

namespace LuaApi.Core.Extensions {
    public static class EnumerableExtensions {
        public static void ForEach<T>(this IEnumerable<T> me, Action<T> action) {
            foreach (var item in me) {
                action.Invoke(item);
            }
        }
    }
}