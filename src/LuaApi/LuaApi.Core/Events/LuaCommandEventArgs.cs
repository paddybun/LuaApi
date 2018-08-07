using System;

namespace LuaApi.Core.Events {
    public class LuaCommandEventArgs : EventArgs {
        public Type HandlerType { get; set; }
        public string CalledCommand { get; set; }
    }
}