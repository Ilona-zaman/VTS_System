using System.IO;
using Core.Business.Interface;
using Core.Data.Helpers;

namespace Tests
{
    public class FileHelper : IFileHelper
    {
        public string GetDBPath(string filename)
        {
            /*string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            //return Path.Combine(libFolder, filename);*/
            return Path.Combine("c:\\Projects\\VTSClient\\Core\\", filename);
        }
    }
}