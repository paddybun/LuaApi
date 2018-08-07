using LuaApi.Core.Attributes;
using LuaApi.Core.Events;
using NLua;

namespace LuaApi.Core.Handlers
{
    public class BaseHandler : LuaApiBase {
        private Lua _lua;
        private readonly string _filename;

        public BaseHandler(string scriptName) {
            _filename = scriptName;
        }

        public override event LuaCommandEventHandler OnCommandCompleted;

        public override string Filename {
            get { return string.Format(@"scripts\{0}", _filename); }
        }

        public override void Register(Lua lua) {
            _lua = lua;
            lua["lua"] = this;
        }

        [LuaApiMethod]
        public void APIDoString(string toExecute)
        {
            _lua.DoString(toExecute);

            if (OnCommandCompleted != null) {
                OnCommandCompleted(this, new LuaCommandEventArgs{CalledCommand = "DoString", HandlerType = GetType()});
            }
        }
    }
}