namespace ECMSS.Utilities
{
    public static class StringHelper
    {
        public static string RemoveSharpCharacter(string fileName)
        {
            if (fileName.Trim()[0] == '#')
            {
                fileName = fileName.Substring(1);
            }
            return fileName;
        }
    }
}