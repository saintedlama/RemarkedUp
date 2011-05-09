using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MarkdownSharp;
using RemarkedUp.Html;

namespace RemarkedUp
{
    public class GenerateCommand
    {
        private GenerateSpecification _howToGenerate;

        public GenerateCommand(GenerateSpecification howToGenerate)
        {
            if (howToGenerate == null)
            {
                throw new ArgumentNullException("howToGenerate");
            }

            // TODO: Exceptions - checking in specification object
            if (howToGenerate.FileFilter == null || howToGenerate.FileFilter.Count() == 0)
            {
                throw new ArgumentNullException("howToGenerate");
            }

            if (howToGenerate.ColumnWidth < 1)
            {
                throw new ArgumentNullException("howToGenerate");
            }

            _howToGenerate = howToGenerate;
        }

        public void Execute()
        {
            List<FileInfo> files = new List<FileInfo>();

            foreach (String fileFilter in _howToGenerate.FileFilter)
            {
                files.AddRange(GetFiles(fileFilter));
            }

            // TODO: ReferenceTable

            var writer = new HtmlWriter("out.html", _howToGenerate.ColumnWidth);

            writer.AddCssIncludes(
                new CssInclude("screen.css"),
                new CssInclude("print.css") { Media = "print" },
                new CssInclude("ie.css") { Conditional = "if lt IE 8" },
                new CssInclude("custom.css"),
                new CssInclude("fancy-type-screen.css")
            );

            foreach (FileInfo file in files)
            {
                Console.WriteLine("Processing file " + file.FullName);

                var content = File.ReadAllText(file.FullName);
                var markdown = new Markdown();
                var html = markdown.Transform(content);

                writer.Append(html);
            }

            writer.Write();
        }

        private IEnumerable<FileInfo> GetFiles(string fileFilter)
        {
            var path = Path.GetDirectoryName(fileFilter);

            if (String.IsNullOrEmpty(path))
            {
                path = Directory.GetCurrentDirectory();
            }
            else
            {
                fileFilter = Path.GetFileName(fileFilter);
            }

            var dir = new DirectoryInfo(path);

            return dir.GetFiles(fileFilter).OrderBy(f => f.FullName);
        }
    }
}
