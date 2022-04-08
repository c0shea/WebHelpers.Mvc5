using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(!httpRequest.Headers.TryGetValue("X-Forwarded-For", out var ipValues) && 
                !httpRequest.Headers.TryGetValue("CF-Connecting-IP", out ipValues))
                httpRequest.Headers.TryGetValue("REMOTE_HOST", out ipValues);

            string ip = ipValues.ToString();

            if (ip.Contains(","))
            {
                ip = ip.Split(',').First().Trim();
            }

            return ip;
        }
    }
}
