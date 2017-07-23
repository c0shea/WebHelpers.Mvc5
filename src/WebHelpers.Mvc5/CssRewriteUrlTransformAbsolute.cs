using System.Web;
using System.Web.Optimization;

namespace WebHelpers.Mvc5
{
    /// <summary>
    /// Rewrites URLs to be absolute so assets will still be found after bundling.
    /// </summary>
    public class CssRewriteUrlTransformAbsolute : IItemTransform
    {
        /// <summary>
        /// Converts any URLs in the input to absolute using the application's base directory.
        /// </summary>
        /// <param name="includedVirtualPath">The virtual path that was included in the bundle for this item that is being transformed.</param>
        /// <param name="input">
        /// The input to be rewritten. For example, url(../fonts/glyphicons.woff) is rewritten as
        /// url(Contoso/Content/fonts/glyphicons.woff) for an application whose base directory is Contoso.
        /// </param>
        public string Process(string includedVirtualPath, string input)
        {
            return new CssRewriteUrlTransform().Process("~" + VirtualPathUtility.ToAbsolute(includedVirtualPath), input);
        }
    }
}
