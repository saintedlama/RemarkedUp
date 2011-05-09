using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RemarkedUp.Html
{
    public class HtmlWriter
    {
        private string _path;
        private StringBuilder _builder;
        private List<CssInclude> _cssIncludes;
        private int _width;

        public HtmlWriter(string path, int width)
        {
            _path = path;
            _builder = new StringBuilder();

            _cssIncludes = new List<CssInclude>();

            if (width <= 0 || width > 24)
            {
                throw new ArgumentException("Width in columns must be greater than 0 and less or equal to 24");
            }

            _width = width;
        }

        public void AddCssIncludes(params CssInclude[] cssIncludes)
        {
            _cssIncludes.AddRange(cssIncludes);
        }

        public void Append(String htmlContent)
        {
            if (_builder.Length == 0)
            {
                WriteHtmlStart();
            }

            _builder.Append(htmlContent);
        }

        protected void WriteHtmlStart()
        {
            // TODO: Title!

            _builder.AppendLine(@"<!DOCTYPE html>");
            _builder.AppendLine(@"<html>");
            _builder.AppendLine(@"<meta charset='utf-8'>");
            _builder.AppendLine(@"<title></title>");

            foreach (CssInclude cssInclude in _cssIncludes)
            {
                _builder.AppendLine(cssInclude.ToString());
            }

            _builder.AppendLine(@"<body>");
            _builder.AppendLine(@"<div class=""container"">");

            int firstColumnWidth = (24 - _width) / 2;
            int lastColumnWidth = 24 - (firstColumnWidth + _width);

            _builder.AppendLine(String.Format(@"<div class=""span-{1} prepend-{0} append-{2} last"">", firstColumnWidth, _width, lastColumnWidth));
        }

        protected void WriteHtmlEnd()
        {
            _builder.AppendLine(@"</div>"); // Grid column
            _builder.AppendLine(@"</div>"); // Container
            _builder.AppendLine(@"</body>");
            _builder.AppendLine(@"</html>");
        }

        public void Write()
        {
            WriteHtmlEnd();

            File.WriteAllText(_path, _builder.ToString(), Encoding.UTF8);

            _builder = new StringBuilder();
            _cssIncludes = new List<CssInclude>();
        }
    }
}
