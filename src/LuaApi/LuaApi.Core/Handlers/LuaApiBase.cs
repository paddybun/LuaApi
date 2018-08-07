using System.IO;
using System.Threading;
using LuaApi.Core.Events;
using LuaApi.Core.Interfaces;
using NLua;

namespace LuaApi.Core.Handlers {
    public abstract class LuaApiBase : ILuaApi {
        public abstract void Register(Lua lua);
        public abstract string Filename { get;  }

        public delegate void LuaCommandEventHandler(object sender, LuaCommandEventArgs eventArgs);
        public abstract event LuaCommandEventHandler OnCommandCompleted;

        public override string ToString()
        {
            Thread.Sleep(500);
            return File.ReadAllText(Filename);
        }
    }
}