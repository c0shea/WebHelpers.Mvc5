using System.Linq;
using System.Web;

namespace WebHelpers.Mvc5
{
    /// <summary>
    /// Extensions for <see cref="HttpRequest"/>.
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Gets the IP address of the client sending the request. This method will return the originating
        /// IP if specified by a proxy but makes no guarantee that this is the client's true IP address.
        /// Since these headers can be spoofed, you are encouraged to perform additional validation if
        /// you are using the IP in a sensitive context.
        /// </summary>
        /// <param name="httpRequest">
        /// The incoming request to get the client's IP address from.
        /// This is typically from HttpContext.Current.Request or similar.
        /// </param>
        public static string ClientIP(this HttpRequest httpRequest)
        {
            var ip = httpRequest.Headers["X-Forwarded-For"] ??
                     httpRequest.Headers["CF-Connecting-IP"] ??
                     httpRequest.ServerVariables["REMOTE_HOST"];

            if (ip.Contains(","))
            {
                ip = ip.Split(',').First().Trim();
            }

            return ip;
        }

        /// <summary>
        /// Gets the IP address of the client sending the request. This method will return the originating
        /// IP if specified by a proxy but makes no guarantee that this is the client's true IP address.
        /// Since these headers can be spoofed, you are encouraged to perform additional validation if
        /// you are using the IP in a sensitive context.
        /// </summary>
        /// <param name="httpRequest">
        /// The incoming request to get the client's IP address from.
        /// This is typically from HttpContext.Current.Request or similar.
        /// </param>
        public static string ClientIP(this HttpRequestBase httpRequest)
        {
            var ip = httpRequest.Headers["X-Forwarded-For"] ??
                     httpRequest.Headers["CF-Connecting-IP"] ??
                     httpRequest.ServerVariables["REMOTE_HOST"];

            if (ip.Contains(","))
            {
                ip = ip.Split(',').First().Trim();
            }

            return ip;
        }
    }
}
