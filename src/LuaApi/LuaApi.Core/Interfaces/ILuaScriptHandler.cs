using System.Collections.Generic;
using NLua;

namespace LuaApi.Core.Interfaces {
    public interface ILuaScriptHandler {
        void Initialize(params ILuaApi[] apis);
        Lua Lua { get; }
        void ExecuteCommand(string commandName);
        void AddApi(ILuaApi api);
        IEnumerable<ILuaApi> Apis { get;}
    }
}