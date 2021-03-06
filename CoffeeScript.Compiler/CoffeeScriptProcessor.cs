﻿using System;
using System.Diagnostics;
using Jurassic;

namespace CoffeeScript.Compiler
{
    /// <summary>
    /// Processes CoffeeScript files into javascript
    /// </summary>
    public class CoffeeScriptProcessor
    {
        private const string COMPILE_TASK = "CoffeeScript.compile(Source, {{bare: {0}}})";

        private static readonly ScriptEngine _engine;
        private static readonly object _o = new object();

        static CoffeeScriptProcessor()
        {
            _engine = new ScriptEngine();
            var js = ResourceReader.ReadString("CoffeeScript.Compiler.coffeescript.js");
            //FIXME-WEIRD: The following line take ages when ncoffee is compiled in Debug and 
            //a lot less when it's in release. It's not related to Jurassic...
            _engine.Execute(js);
        }


        private static ScriptEngine Engine
        {
            get
            {
                return _engine;
            }
        }

        public static string Process(string contents)
        {
            return Process(contents, false);
        }


        /// <summary>
        /// Processes contents as a coffeescript file
        /// </summary>
        /// <param name="contents">The javascript contents</param>
        /// <returns></returns>
        public static string Process(string contents, bool bare)
        {
            lock (_o)
            {
                Engine.SetGlobalValue("Source", contents);
                var bareArg = bare ? "true" : "false";
                return Engine.Evaluate<string>(String.Format(COMPILE_TASK,bareArg));
            }
        }
    }
}