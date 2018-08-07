using System;
using LuaApi.Core.Attributes;
using NLua;

namespace LuaApi.Core.Handlers
{
    public class ConsoleHandler : LuaApiBase {
        private readonly string _filename;

        public ConsoleHandler(string scriptName) {
            _filename = scriptName;
        }

        public override string Filename {
            get { return string.Format(@"scripts\{0}", _filename); }
        }

        public override void Register(Lua lua)
        {
            lua["console"] = this;
        }

        public override event LuaCommandEventHandler OnCommandCompleted;

        [LuaApiMethod]
        public void APIWrite(string setValue)
        {
            Console.WriteLine(setValue);
        }
    }
}