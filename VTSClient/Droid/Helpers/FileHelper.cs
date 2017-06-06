
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Core.Data.Helpers;

namespace Droid.Helpers
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
            //return Path.Combine("c:\\Projects\\VTSClient\\Core\\", filename);
        }
    }
}