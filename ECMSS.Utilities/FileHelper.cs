using System.IO;

namespace ECMSS.Utilities
{
    public class FileHelper
    {
        public static void SaveFile(string filePath, byte[] fileData)
        {
            string path = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fs.Write(fileData, 0, fileData.Length);
            }
        }
    }
}