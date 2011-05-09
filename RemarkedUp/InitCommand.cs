using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace RemarkedUp
{
    public class InitCommand
    {
        private string _style;
        
        public InitCommand(String style)
        {
            _style = String.IsNullOrWhiteSpace(style) ? "default" : style;
        }

        // TODO: Print progress via some passed object?
        public void Execute()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(location);
            var stylePath = Path.Combine(path, _style);

            if (!Directory.Exists(stylePath))
            {
                // Try to interpret style as path
                if (!Directory.Exists(_style))
                {
                    // TODO: No - even no path -> Throw ex!
                }

                stylePath = _style;
            }

            // TODO: And again a dependency to style directory :)
            var destinationPath = Path.Combine(Directory.GetCurrentDirectory(), "style");

            // TODO: Check if file exists - in case it exists, don't do anything!
            CopyStyle(stylePath, destinationPath);
        }

        private void CopyStyle(string sourcePath, string destinationPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));

            //Copy all the files
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, destinationPath));
        }
    }
}
