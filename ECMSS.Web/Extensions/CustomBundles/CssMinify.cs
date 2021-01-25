using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Optimization;

namespace ECMSS.Web.Extensions.CustomBundles
{
    public class CssMinify : IBundleTransform
    {
        private const string CssContentType = "text/css";

        static CssMinify()
        {
        }

        public virtual void Process(BundleContext context, BundleResponse response)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }
            if (!context.EnableInstrumentation)
            {
                var minifier = new Minifier();
                FixCustomCssErrors(response);
                string str = minifier.MinifyStyleSheet(response.Content, new CssSettings()
                {
                    CommentMode = CssComment.None
                });
                if (minifier.ErrorList.Count > 0)
                {
                    GenerateErrorResponse(response, minifier.ErrorList);
                }
                else
                {
                    response.Content = str;
                }
            }
            response.ContentType = CssContentType;
        }

        private void FixCustomCssErrors(BundleResponse response)
        {
            response.Content = Regex.Replace(response.Content, @"@import[\s]+([^\r\n]*)[\;]", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }

        private static void GenerateErrorResponse(BundleResponse bundle, IEnumerable<object> errors)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("/* ");
            stringBuilder.Append("CSS Minify Error").Append("\r\n");
            foreach (object obj in errors)
            {
                stringBuilder.Append(obj.ToString()).Append("\r\n");
            }
            stringBuilder.Append(" */\r\n");
            stringBuilder.Append(bundle.Content);
            bundle.Content = stringBuilder.ToString();
        }
    }
}