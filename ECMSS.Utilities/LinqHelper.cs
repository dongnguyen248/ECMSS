using ECMSS.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ECMSS.Utilities
{
    public static class LinqHelper
    {
        private static readonly string[] powerPointTypes = new string[] { "ppt", "pptx" };
        private static readonly string[] excelTypes = new string[] { "xls", "xlt", "xlsx", "xlsm", "xlsb", "xltx", "xltm", "csv" };
        private static readonly string[] wordTypes = new string[] { "doc", "docx" };
        private static readonly string[] pdfTypes = new string[] { "pdf" };
        private static readonly string[] imageTypes = new string[] { "jpg", "gif", "png", "jpeg" };
        private static readonly string[] cadTypes = new string[] { "dwg", "dwt", "dxf" };
        private static readonly string[] videoTypes = new string[] { "webm", "mpeg", "ogg", "mp4", "mpg", "mpv", "m4p", "m4v", "avi", "wmv", "flv", "swf" };

        public static Expression<Func<FileInfo, bool>> GetFromType(string type)
        {
            switch (type.ToUpper())
            {
                case "POWERPOINT":
                    return f => powerPointTypes.Contains(f.Extension);

                case "EXCEL":
                    return f => excelTypes.Contains(f.Extension);

                case "WORD":
                    return f => wordTypes.Contains(f.Extension);

                case "PDF":
                    return f => pdfTypes.Contains(f.Extension);

                case "IMAGE":
                    return f => imageTypes.Contains(f.Extension);

                case "CAD":
                    return f => cadTypes.Contains(f.Extension);

                case "VIDEO":
                    return f => videoTypes.Contains(f.Extension);

                default:
                    return f => true;
            }
        }
    }
}