using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebHelpers.Mvc5.Enum
{
    /// <summary>
    /// ASP.NET handler that renders Enums as a frozen object in JavaScript to promote re-usability
    /// between the server and client.
    /// </summary>
    public class EnumHandler : IHttpHandler
    {
        private static readonly Lazy<string> _handlerUrl = new Lazy<string>(GetHandlerUrl);

        /// <summary>
        /// How long to cache the JavaScript output for. Only used when a unique hash is present in the URL.
        /// </summary>
        private static readonly TimeSpan CacheFor = TimeSpan.FromDays(7);

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="IHttpHandler"/> instance.
        /// </summary>
        public bool IsReusable => true;

        /// <summary>
        /// Gets the URL to the <see cref="EnumHandler"/>, with a unique hash in the URL.
        /// The hash will change every time an Enum changes.
        /// </summary>
        public static string HandlerUrl => _handlerUrl.Value;

        /// <summary>
        /// Handles a HTTP request.
        /// </summary>
        /// <param name="context">
        /// An <see cref="HttpContext" /> object that provides references to the intrinsic server objects
        /// (for example, Request, Response, Session, and Server) used to service HTTP requests.
        /// </param>
        public void ProcessRequest(HttpContext context)
        {

            var javascript = GetJavaScript();

            SetCachingHeaders(context, javascript);

            context.Response.ContentType = "text/javascript";
            context.Response.Write(javascript);
        }

        /// <summary>
        /// Set the HTTP headers to cache the response.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="output">The output of the handler.</param>
        private static void SetCachingHeaders(HttpContext context, string output)
        {
            context.Response.Cache.SetETag(Hash(output));
            context.Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
            context.Response.Cache.SetExpires(DateTime.Now + CacheFor);
            context.Response.Cache.SetMaxAge(CacheFor);
            context.Response.Cache.SetVaryByCustom("*");
        }

        private static string GetJavaScript()
        {
            var sb = new StringBuilder("window.Enums = Object.freeze({");

            var enumsFound = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                enumsFound.AddRange(assembly.GetTypes()
                                            .Where(t => t.IsEnum && t.GetCustomAttributes(typeof(ExposeInJavaScriptAttribute), inherit: true).Any()));
            }

            //var enums = Assembly.GetCallingAssembly()
            //                    .GetTypes()
            //                    .Where(t => t.IsEnum && t.GetCustomAttributes(typeof(ExposeInJavaScriptAttribute), inherit: true).Any());

            for (int i = 0; i < enumsFound.Count; i++)
            {
                AppendEnumJson(sb, enumsFound[i]);

                if (i < enumsFound.Count - 1)
                {
                    sb.Append(",");
                }
            }

            //foreach (var enumFound in enumsFound)
            //{
            //    AppendEnumJson(sb, enumFound);
            //}

            sb.Append("});");

            return sb.ToString();
        }

        private static void AppendEnumJson(StringBuilder sb, Type type)
        {
            sb.Append(type.Name);
            sb.Append(":{");

            var i = 0;
            var values = System.Enum.GetValues(type);

            foreach (var value in values)
            {
                i++;
                sb.Append(System.Enum.GetName(type, value));
                sb.Append(":");
                //sb.Append(value.GetType().Name);

                // We can't assume a cast to an int since an enum can be any integral type and will throw an InvalidCastException.
                sb.Append(Convert.ChangeType(value, Type.GetTypeCode(value.GetType())));

                if (i < values.Length)
                {
                    sb.Append(",");
                }
            }

            sb.Append("}");
        }

        /// <summary>
        /// Calculate the SHA1 hash of the specified content.
        /// </summary>
        /// <param name="content">The content to hash.</param>
        private static string Hash(string content)
        {
            using (var sha1 = SHA1.Create())
            {
                var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(content));
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
                return hash;
            }
        }

        /// <summary>
        /// Gets the URL to the <see cref="EnumHandler"/>, with a unique hash in the URL.
        /// The hash will change every time an Enum changes.
        /// </summary>
        private static string GetHandlerUrl()
        {
            // Hash the file contents so the URL can change whenever a route or the routing JS changes
            // Include version in hash so upgrades invalidate client-side cache
            var javascript = GetJavaScript();
            var hash = Hash(javascript); // TODO: Add file last modified date ticks to make this unique?

            return string.Join("/", VirtualPathUtility.ToAbsolute("~/WebHelpers.axd"), hash);
        }
    }
}
