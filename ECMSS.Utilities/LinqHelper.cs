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
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string columnName, bool isAscending = true)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                return source;
            }

            ParameterExpression parameter = Expression.Parameter(source.ElementType, "");

            MemberExpression property = Expression.Property(parameter, columnName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = isAscending ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, property.Type },
                                  source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<TEntity>(methodCallExpression);
        }
    }
}