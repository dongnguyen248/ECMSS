using System.IO;

namespace ECMSS.Utilities
{
    public class FileHelper
    {
        public static void SaveFile(string filePath, byte[] fileData)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fs.Write(fileData, 0, fileData.Length);
            }
        }

        public static string CreatePath(string path, string subPath)
        {
            string fullPath = path + subPath;
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            return fullPath;
        }

        public static void CreatePath(string path, params string[] subPaths)
        {
            if (subPaths is null)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            else
            {
                for (int i = 0; i < subPaths.Length; i++)
                {
                    string fullPath = path + subPaths[i];
                    if (!Directory.Exists(fullPath))
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                }
            }
        }
    }
}