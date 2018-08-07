using LuaApi.Core.Handlers;
using NLua;

namespace LuaApi.Core.Interfaces
{
    public interface ILuaApi
    {
        void Register(Lua lua);
        event LuaApiBase.LuaCommandEventHandler OnCommandCompleted;
    }
}