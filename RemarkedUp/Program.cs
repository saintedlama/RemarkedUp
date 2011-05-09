using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MarkdownSharp;
using NDesk.Options;
using System.Text.RegularExpressions;

namespace RemarkedUp
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 16;
            bool help = false;
            bool init = false;
            bool clean = false;
            string style = null;

            var p = new OptionSet {
   	            { "w:|width:", (int v) => width = v },
                { "i|init", v => init = v != null },
                { "s|style:", v => style = v},
                { "c|clean", v => clean = v != null },
   	            { "h|?|help", v => help = v != null }
            };

            List<string> extra = p.Parse(args);

            if (help)
            {
                ShowHelp(p);
                return;
            }

            if (clean)
            {
                var cleanCmd = new CleanCommand();
                cleanCmd.Execute();
            }

            if (init)
            {
                var initCmd = new InitCommand(style);
                initCmd.Execute();
            }

            if (extra.Count > 0)
            {
                var generateCmd = new GenerateCommand(new GenerateSpecification
                {
                    FileFilter = extra,
                    ColumnWidth = width
                });

                generateCmd.Execute();
            }
            else
            {
                // TODO: In case now command created -> Show help, otherwise process!
                ShowHelp(p);
                return;
            }
        }

        static void ShowHelp(OptionSet p)
        {
            // TODO: Show Help
            Console.WriteLine("Usage: greet [OPTIONS]+ message");
            Console.WriteLine("Greet a list of individuals with an optional message.");
            Console.WriteLine("If no message is specified, a generic greeting is used.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

        private static void PrintErrorAndExit(string p)
        {
            Console.Error.WriteLine("p");
            Environment.Exit(-1);
        }
    }

}
