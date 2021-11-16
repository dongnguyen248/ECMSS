using System;
using System.IO;

namespace ECMSS.Utilities
{
    public class FileHelper
    {
        public static void SaveFile(string filePath, byte[] fileData, bool isOverride = false)
        {
            if (!isOverride)
            {
                if (File.Exists(filePath))
                {
                    throw new Exception("There is a file with the same name in the same directory");
                }
            }
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fs.Write(fileData, 0, fileData.Length);
            }
        }

        public static void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }
        }

        public static string CreateFolder(string path, string subPath)
        {
            string fullPath = path.Trim() + subPath.Trim();
            if (Directory.Exists(fullPath))
            {
                throw new Exception("This directory is already available");
            }
            return Directory.CreateDirectory(fullPath).Name;
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void Move(string srcPath, string desPath)
        {
            if (File.Exists(desPath))
            {
                throw new Exception("There is a file with the same name in the destination directory");
            }
            File.Move(srcPath, desPath);
        }

        public static void Empty(string path, bool deleteRoot = false)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            foreach (FileInfo file in directory.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in directory.EnumerateDirectories())
            {
                dir.Delete(true);
            }
            if (deleteRoot)
            {
                Directory.Delete(path);
            }
        }

        public static string GetFileExtension(string fileName)
        {
            int lastIndex = fileName.LastIndexOf('.');
            return fileName.Substring(lastIndex + 1);
        }
    }
}