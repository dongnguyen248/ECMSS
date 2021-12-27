using ECMSS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ECMSS.Utilities
{
    public static class LinqHelper
    {
        public static Expression<Func<FileInfo, bool>> GetFromType(string type)
        {
            Dictionary<string, string[]> fileTypes = new Dictionary<string, string[]>
            {
                { "POWERPOINT", new string[] { "ppt", "pptx" } },
                { "EXCEL", new string[] { "xls", "xlt", "xlsx", "xlsm", "xlsb", "xltx", "xltm", "csv" } },
                { "WORD", new string[] { "doc", "docx" } },
                { "PDF", new string[] { "pdf" } },
                { "IMAGE", new string[] { "jpg", "gif", "png", "jpeg" } },
                { "CAD", new string[] { "dwg", "dwt", "dxf" } },
                { "VIDEO", new string[] { "webm", "mpeg", "ogg", "mp4", "mpg", "mpv", "m4p", "m4v", "avi", "wmv", "flv", "swf" } }
            };
            string[] types = fileTypes.Where(x => x.Key == type.ToUpper()).Select(x => x.Value).FirstOrDefault();
            if (types != null)
            {
                return f => types.Contains(f.Extension);
            }
            return f => true;
        }
    }
}