using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemarkedUp.Html
{
    public class CssInclude
    {
        public String Name { get; set; }
        public String Media { get; set; }
        public String Conditional { get; set; }

        public CssInclude() 
        {
            Media = "screen, projection";
        }

        public CssInclude(String name) : this()
        {
            Name = name;
        }

        public override String ToString()
        {
            var builder = new StringBuilder();

            if (!String.IsNullOrWhiteSpace(Conditional))
            {
                builder.AppendFormat("<!--[{0}]>", Conditional);
            }

            // TODO: Style - this is a depencency we have - make it configurable and check back style too!
            builder.AppendFormat(@"<link rel=""stylesheet"" href=""style/{0}"" type=""text/css"" media=""{1}"">", Name, Media);

            if (!String.IsNullOrWhiteSpace(Conditional))
            {
                builder.Append("<![endif]-->");
            }

            return builder.ToString();
        }
    }
}
