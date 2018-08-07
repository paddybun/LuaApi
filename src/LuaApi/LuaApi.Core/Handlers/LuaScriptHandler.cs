using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LuaApi.Core.Extensions;
using LuaApi.Core.Interfaces;
using NLua;

namespace LuaApi.Core.Handlers {
    public class LuaScriptHandler : ILuaScriptHandler {

        private FileSystemWatcher _watcher;
        private readonly List<ILuaApi> _apis;
        private Lua _lua;

        public Lua Lua {
            get { return _lua ?? (_lua = new Lua()); }
            private set { _lua = value; }
        }

        public IEnumerable<ILuaApi> Apis { get { return _apis; } }

        public LuaScriptHandler() {
            _apis = new List<ILuaApi> {new BaseHandler("base.lua")};
        }

        public void Initialize(params ILuaApi[] apis) {
            _watcher = CreateWatcher();

            _apis.AddRange(apis);

            lock (Lua)
            {
                InitializeScripts(_apis);
                RegisterValiables(_apis);
            }
        }

        public void ExecuteCommand(string commandName) {
            Lua.DoString(commandName);
        }

        public void AddApi(ILuaApi api) {
            _apis.Add(api);
        }

        private FileSystemWatcher CreateWatcher() {
            var watcher = new FileSystemWatcher {
                Path = GetFolderToWatch(),
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite,
                Filter = "*.lua",
                EnableRaisingEvents = true
            };

            watcher.Changed += ScriptUpdated;
            return watcher;
        }

        private string GetFolderToWatch() {
            return Path.Combine(Directory.GetCurrentDirectory(), "scripts");
        }

        private void ScriptUpdated(object sender, FileSystemEventArgs e)
        {
            lock (Lua) {
                ResetLuaHandler();
                InitializeScripts(_apis);
                RegisterValiables(_apis);
            }
        }

        private void ResetLuaHandler() {
            Lua.Dispose();
            Lua = null;
        }

        private void InitializeScripts(IEnumerable<ILuaApi> apis) {
            var mergedScriptFile = string.Join(Environment.NewLine, apis.Select(x => x.ToString()));
            Lua.DoString(mergedScriptFile);
        }

        private void RegisterValiables(IEnumerable<ILuaApi> apis)
        {
            apis.ForEach(x => x.Register(Lua));
        }
    }
}