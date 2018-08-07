using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LuaApi.Core.Events;
using LuaApi.Core.Handlers;
using NLua;

namespace LuaApi
{
    internal class Program
    {
        private static readonly Dictionary<string, Action> KnownFunctions = new Dictionary<string, Action>
        {
            {"exit", () => Environment.Exit(2)}
        };

        private static void Main(string[] args) {
            RunLuaEngine();
            //RunLuaCall();
        }

        private static void RunLuaEngine() {
            var handler = new LuaScriptHandler();
            handler.Initialize(new ConsoleHandler("console.lua"));
            var res = handler.Apis.First();
            res.OnCommandCompleted += res_OnCommandCompleted;

            while (true) {
                var command = Console.ReadLine();
                if (command == null)
                    continue;

                try {
                    if (KnownFunctions.ContainsKey(command)) {
                        KnownFunctions[command].Invoke();
                        continue;
                    }
                    handler.ExecuteCommand(command);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void res_OnCommandCompleted(object sender, LuaCommandEventArgs eventArgs)
        {
            Console.WriteLine("Command: {0} executed", eventArgs.CalledCommand);
        }

        private static void RunLuaCall() {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "scripts", "console.lua");
            var state = new Lua();
            state.DoString(File.ReadAllText(file));
            var write = state["getOutput"] as LuaFunction;

            if (write != null) {
                var output = write.Call("Patrick");
                var res = (string) output.First();
            }
        }
    }
}