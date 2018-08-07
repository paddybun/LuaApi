using System.Linq;

namespace LuaApi.Core.Extensions {
    public static class StringExtensions {
        public static bool AreNullOrEmpty(this string me, params string[] literals) {

            if (string.IsNullOrEmpty(me))
                return false;

            return literals.All(x => !string.IsNullOrEmpty(x));
        }
    }
}