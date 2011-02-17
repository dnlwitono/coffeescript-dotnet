#region usings

using System;
using System.IO;
using Composable.System.IO;
using Composable.System.Linq;
using NUnit.Framework;

#endregion

namespace CoffeeScript.Compiler.Tests
{
    public class CompilerTestBase
    {
        public static readonly DirectoryInfo BinDir = Environment.CurrentDirectory.AsDirectory();
        public static readonly DirectoryInfo BaseDir = BinDir.Parent.Parent;
        public static readonly DirectoryInfo OutputDir = BaseDir.SubDir("OutputDir");

        public static readonly string NonExistingPath = @"Q:\nonsense\nonsense\nonsense\nonsense";

        [SetUp]
        public void CleanOutputDir()
        {
            OutputDir.GetFilesResursive().ForEach(f => f.Delete());
        }
    }
}