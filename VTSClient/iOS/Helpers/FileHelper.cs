using System;
using System.IO;
using Core.Data.Helpers;

namespace iOS.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string GetDBPath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}