using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build.Models
{
    internal class CommandLineArguments
    {
        [Option('t', "targets")]
        public IEnumerable<string> Targets { get; set; } = new List<string>();

        [Option("clear")]
        public bool Clear { get; set; }

        [Option("dry-run")]
        public bool DryRun { get; set; }

        [Option("no-color")]
        public bool NoColor { get; set; }

        [Option("skip-dependencies")]
        public bool SkipDependencies { get; set; }

        [Option("verbose")]
        public bool Verbose { get; set; }

        [Option("list-dependencies")]
        public bool ListDependencies { get; set; }

        [Option("list-inputs")]
        public bool ListInputs { get; set; }

        [Option("list-targets")]
        public bool ListTargets { get; set; }

        [Option("list-tree")]
        public bool ListTree { get; set; }

        [Option("verbosity")]
        public string Verbosity { get; set; } = "quiet";
    }
}
