using System.IO;
using System.Web.Hosting;
using System.Web.Optimization;

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

        /// <summary>
        /// Adds a cache-busting query parameter to the URL of the bundle.
        /// The version is the number of ticks since the last write time of the file.
        /// </summary>
        /// <param name="bundle">The bundle (e.g. StyleBundle) to add the version to.</param>
        public static Bundle AddVersion(this Bundle bundle)
        {
            bundle.Transforms.Add(new VersionBundleTransform());

            return bundle;
        }

        private class VersionBundleTransform : IBundleTransform
        {
            public void Process(BundleContext context, BundleResponse response)
            {
                foreach (var file in response.Files)
                {
                    file.IncludedVirtualPath = $"{file.IncludedVirtualPath}?v={File.GetLastWriteTime(HostingEnvironment.MapPath(file.IncludedVirtualPath)).Ticks}";
                }
            }
        }
    }
}
