using System.IO;
using System.Web.Hosting;

namespace WebHelpers.Mvc5
{
    /// <summary>
    /// Extensions for generating a version query parameter for content.
    /// </summary>
    public static class VersionExtensions
    {
        /// <summary>
        /// Adds a cache-busting query parameter to the URL.
        /// The version is the number of ticks since the last write time of the file.
        /// </summary>
        /// <param name="contentPath">The application absolute path to the file.</param>
        public static string AddVersion(this string contentPath)
        {
            return $"{contentPath}?v={File.GetLastWriteTime(HostingEnvironment.MapPath(contentPath)).Ticks}";
        }
    }
}
