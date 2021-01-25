using System.Web.Optimization;

namespace ECMSS.Web.Extensions.CustomBundles
{
    public class StyleBundle : Bundle
    {
        public StyleBundle(string virtualPath, IBundleTransform bundleTransform = null)
           : base(virtualPath, new IBundleTransform[1] { bundleTransform ?? new CssMinify() })
        {
        }

        public StyleBundle(string virtualPath, string cdnPath, IBundleTransform bundleTransform = null)
            : base(virtualPath, cdnPath, new IBundleTransform[1] { bundleTransform ?? new CssMinify() })
        {
        }
    }
}